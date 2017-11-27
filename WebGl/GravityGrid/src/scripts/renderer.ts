import { Press } from "./Press"

declare function require(name: string): string;
const vertexShaderSource: string = (() =>
    require('./glsl/2d-vertex-shader.glsl'))();
const fragmentShaderSource: string = (() =>
    require('./glsl/2d-fragment-shader.glsl'))();

export interface IRenderable {
    renderGravityGrid(div: HTMLElement): void;
}

class Renderer implements IRenderable {
    private gl: WebGLRenderingContext | null;
    private program: WebGLProgram | null;
    private canvas: HTMLCanvasElement;
    private start: number = new Date().getTime();
    
    private idMapper: number[] = Array(0);
    private presses : Press[] = Array(6); // mouse + 5 touch points.

    public renderGravityGrid(div: HTMLElement): void {
        this.canvas = document.createElement('canvas');
        this.canvas.style.width = '100%';
        this.canvas.style.height = '100%';
        div.appendChild(this.canvas);
        this.gl = this.canvas.getContext('experimental-webgl');

        window.onload = () => {
            this.init();
            this.render();
        }
    }

    private takeFirstFreeIndex(id:number) : number {
        for (let i = 0; i < this.presses.length; i++) {
            if (!this.presses[i] || this.presses[i].id === -1) {
                this.idMapper[id] = i;
                return i;
            }
        }
        return -1;
    }

    private takeFirstFreeIndexMouse() : number {
        for (let i = this.presses.length-1; i >= 0; i--) {
            if (!this.presses[i] || this.presses[i].id === -1) { // no press
                this.idMapper[i] = i;
                return i;
            } else if (this.presses[i].isMouse && this.presses[i].isDead && this.presses[i].power <= 2) { // dead press
                this.idMapper[i] = i;
                return i;
            }
        }
        return -1;
    }

    private findActiveMouse() : Press | null {
        for (let i = this.presses.length-1; i >= 0; i--) {
            if (this.presses[i] && this.presses[i].id !== -1 && !this.presses[i].isDead && this.presses[i].isMouse) {
                return this.presses[i];
            }
        }
        return null;
    }

    private getPos(canvas: HTMLCanvasElement, x: number, y:number) : {x:number, y:number} {
        const rect: ClientRect = canvas.getBoundingClientRect();        
        return {
            x:
                (x - rect.left) /
                (rect.right - rect.left) *
                canvas.width,
            y:
                (y - rect.bottom) /
                (rect.top - rect.bottom) *
                canvas.height,
        };
    }
    private getMousePos(canvas: HTMLCanvasElement, evt: Event): { x: number; y: number } {
        const mouseEvt: MouseEvent = evt as MouseEvent;
        return this.getPos(canvas, mouseEvt.clientX, mouseEvt.clientY);
    }

    private getTouchPos(canvas: HTMLCanvasElement, touch: Touch): { x: number; y: number } {
        const rect: ClientRect = canvas.getBoundingClientRect();
        return this.getPos(canvas, touch.clientX, touch.clientY);
    }

    private getSize(): void {
        this.canvas.width = this.canvas.clientWidth;
        this.canvas.height = this.canvas.clientHeight;
    }

    private setTimedInterval(callbackInterval: () => void, callbackTimeout: () => void, delay: number, timeout: number): void {
        const id: number = window.setInterval(callbackInterval, delay);
        window.setTimeout(() => {
            window.clearInterval(id);
            callbackTimeout();
        }, timeout);
    }

    private addListeners(): void {
        this.canvas.addEventListener('mouseenter', evt => {
            const id: number = this.takeFirstFreeIndexMouse();
            if (id > 0) {
                const press: Press = new Press(id);
                press.isMouse = true;
                press.current = this.getMousePos(this.canvas, evt);
                press.old = press.current;
                this.presses[id] = press;
            }
        }, false);

        this.canvas.addEventListener('mousemove', evt => {
            const press: Press | null = this.findActiveMouse();
            if (press) {
                press.old = press.current;
                press.current = this.getMousePos(this.canvas, evt);
            }
        }, false);

        this.canvas.addEventListener('mouseleave', evt => {
            const press: Press | null = this.findActiveMouse();
            if (press) {
                press.isDead = true;
            }
        }, false);

        this.canvas.addEventListener('touchstart', evt => {
            evt.preventDefault();
            // tslint:disable-next-line:prefer-for-of
            for (let i = 0; i<evt.changedTouches.length; i++) {
                const touch: Touch = evt.changedTouches[i];
                const id = this.takeFirstFreeIndex(touch.identifier);
                if (id >= 0) {
                    const press: Press = new Press(id);
                    press.current = this.getTouchPos(this.canvas, touch);
                    press.old = press.current;

                    this.presses[press.id] = press;
                }
            }
        }, false);

        this.canvas.addEventListener('touchmove', evt => {
            evt.preventDefault();            
            // tslint:disable-next-line:prefer-for-of
            for (let i = 0; i<evt.changedTouches.length; i++) {
                const touch: Touch = evt.changedTouches[i];
                const id = this.idMapper[touch.identifier];
                if (id>=0) {
                    const press: Press = this.presses[id];
                    press.old = press.current;
                    press.current = this.getTouchPos(this.canvas, touch);
                }
            }
        }, false);

        this.canvas.addEventListener('touchend', evt => {
            evt.preventDefault();            
            const ids: number[] = Array();
            // tslint:disable-next-line:prefer-for-of
            for (let i = 0; i<evt.changedTouches.length; i++) {
                const touch: Touch = evt.changedTouches[i];
                const id = this.idMapper[touch.identifier];
                if (id>=0) {
                    const press: Press = this.presses[id];
                    press.delta = { x: press.current.x - press.old.x, y: press.current.y - press.old.y };
                    press.isDead = true;
                    ids.push(id);
                }
            }
            
            this.setTimedInterval(() => { // interval
                // tslint:disable-next-line:prefer-for-of
                for (let i = 0; i<ids.length; i++) {
                    const id = ids[i];
                    if (id>=0) {
                        const press: Press = this.presses[id];
                        press.current = { 
                            x: press.current.x + press.delta.x * press.power / 250, 
                            y: press.current.y + press.delta.y * press.power / 250
                        };
                    }
                }
            }, ()=> { // timeout
                // tslint:disable-next-line:prefer-for-of
                for (let i = 0; i<ids.length; i++) {
                    const id = ids[i];
                    this.presses[id] = new Press(-1);
                }
            }, 18, 2000);
        }, false);

        window.onresize = () => setTimeout(() => this.getSize(), 1);
    }
    
    private init(): void {
        let vertexShader: WebGLShader | null;
        let fragmentShader: WebGLShader | null;
        this.addListeners();
        this.getSize();
        if (this.gl) {
            const buffer: WebGLBuffer | null = this.gl.createBuffer();
            this.gl.bindBuffer(this.gl.ARRAY_BUFFER, buffer);
            this.gl.bufferData(
                this.gl.ARRAY_BUFFER,
                new Float32Array([-1.0, -1.0, 1.0, -1.0, -1.0, 1.0, -1.0, 1.0, 1.0, -1.0, 1.0, 1.0,]),
                this.gl.STATIC_DRAW
            );
            this.gl.viewport(0, 0,
                this.gl.drawingBufferWidth,
                this.gl.drawingBufferHeight
            );
            vertexShader = this.gl.createShader(this.gl.VERTEX_SHADER);
            this.gl.shaderSource(vertexShader, vertexShaderSource);
            this.gl.compileShader(vertexShader);
            fragmentShader = this.gl.createShader(this.gl.FRAGMENT_SHADER);
            this.gl.shaderSource(fragmentShader, fragmentShaderSource);
            this.gl.compileShader(fragmentShader);
            this.program = this.gl.createProgram();
            this.gl.attachShader(this.program, vertexShader);
            this.gl.attachShader(this.program, fragmentShader);
            this.gl.linkProgram(this.program);
            this.gl.useProgram(this.program);
        }
    }

    private addGLProperties(): void {
        if (this.gl) {
            const positionLocation: number = this.gl.getAttribLocation(this.program, 'position');
            this.gl.enableVertexAttribArray(positionLocation);
            this.gl.vertexAttribPointer(positionLocation, 2, this.gl.FLOAT, true, 0, 0);
            
            const resolutionPosition: WebGLUniformLocation | null = this.gl.getUniformLocation(this.program, 'resolution');
            this.gl.uniform2f(resolutionPosition, this.canvas.width, this.canvas.height);
            
            const rotationPosition: WebGLUniformLocation | null = this.gl.getUniformLocation(this.program, 'rotation');
            this.gl.uniform2f(rotationPosition, 0.5, 0.8);
            
            const timePosition: WebGLUniformLocation | null = this.gl.getUniformLocation(this.program, 'time');
            this.gl.uniform1f(timePosition, (new Date().getTime() - this.start) / 1000);

            const gravityPosition: WebGLUniformLocation | null = this.gl.getUniformLocation(this.program, 'gravity');
            this.gl.uniform1f(gravityPosition, 70);

            const reachPosition: WebGLUniformLocation | null = this.gl.getUniformLocation(this.program, 'reach');
            this.gl.uniform1f(reachPosition, 10000);

            const offsetPosition: WebGLUniformLocation | null = this.gl.getUniformLocation(this.program, 'offset');
            this.gl.uniform2f(offsetPosition, 0, 0);
            
            const pitchPosition: WebGLUniformLocation | null = this.gl.getUniformLocation(this.program, 'pitch');
            this.gl.uniform2f(pitchPosition, 80, 80);

            let presses: number = 0;
            for (let i: number = 0; i<7; i++) {
                if (this.presses[i] && this.presses[i].id !== -1) {
                    const pressPosition: WebGLUniformLocation | null = this.gl.getUniformLocation(this.program, "presses["+presses+"]");
                    this.gl.uniform3f(pressPosition, this.presses[i].current.x, this.presses[i].current.y, this.presses[i].power/250);
                    presses++;
                }
            }

            const amtPressesPosition: WebGLUniformLocation | null = this.gl.getUniformLocation(this.program, 'amtPresses');
            this.gl.uniform1i(amtPressesPosition, presses);
        }
    }

    private render(): void {
        if (this.gl) {
            this.addGLProperties();
            this.gl.drawArrays(this.gl.TRIANGLES, 0, 6);
            requestAnimationFrame(() => this.render());

            for(const press of this.presses) {
                if (press && press.isDead && press.power > 0) {
                    press.power -= 2;
                } else if (press && !press.isDead && press.power < 200) {
                    press.power += 7;
                }
            }
        }
    }
}

export { Renderer };
