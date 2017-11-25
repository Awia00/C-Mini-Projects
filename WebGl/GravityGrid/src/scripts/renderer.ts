declare function require(name: string): string;
const vertexShaderSource: string = (() =>
    require('./glsl/2d-vertex-shader.glsl'))();
const fragmentShaderSource: string = (() =>
    require('./glsl/2d-fragment-shader.glsl'))();

export interface IRenderable {
    renderOnCanvas(canvas: HTMLElement): void;
}

class Renderer implements IRenderable {
    private gl: WebGLRenderingContext | null;
    private program: WebGLProgram | null;
    private canvas: HTMLCanvasElement;
    private mousePos = { x: 0.0, y: 0.0 };
    private oldMousePos = { x: 0.0, y: 0.0 };
    private delta: { x: number; y: number } | undefined;
    private start: number = new Date().getTime();
    private lastInput: number = 3;
    private isInput: boolean = false;

    public renderOnCanvas(div: HTMLElement): void {
        this.canvas = document.createElement('canvas');
        this.canvas.style.width = '100%';
        this.canvas.style.height = '100%';
        div.appendChild(this.canvas);
        this.gl = this.canvas.getContext('experimental-webgl');
        window.onload = () => this.init();
    }

    private getMousePos(canvas: HTMLCanvasElement, evt: Event): { x: number; y: number } {
        const rect: ClientRect = canvas.getBoundingClientRect();
        const mouseEvt: MouseEvent = evt as MouseEvent;
        return {
            x:
                (mouseEvt.clientX - rect.left) /
                (rect.right - rect.left) *
                canvas.width,
            y:
                (mouseEvt.clientY - rect.bottom) /
                (rect.top - rect.bottom) *
                canvas.height,
        };
    }

    private getTouchPos(canvas: HTMLCanvasElement, evt: Event): { x: number; y: number } {
        const rect: ClientRect = canvas.getBoundingClientRect();
        const touchEvt: TouchEvent = evt as TouchEvent;
        return {
            x:
                (touchEvt.touches[0].clientX - rect.left) /
                (rect.right - rect.left) *
                canvas.width,
            y:
                (touchEvt.touches[0].clientY - rect.bottom) /
                (rect.top - rect.bottom) *
                canvas.height,
        };
    }

    private viewportToPixels(value: string, isHeight: boolean): number {
        const parts: RegExpMatchArray | null = value.match(/([0-9\.]+)(vh|vw)/);
        if (parts) {
            const q: number = Number(parts[1]);
            if (isHeight) {
                return window.innerHeight * (q / 100);
            } else {
                return window.innerWidth * (q / 100);
            }
        }
        return 0;
    }

    private getSize(): void {
        this.canvas.width = this.canvas.clientWidth;
        this.canvas.height = this.canvas.clientHeight;
    }

    private setTimedInterval(callback: () => void, delay: number, timeout: number): void {
        const id: number = window.setInterval(callback, delay);
        window.setTimeout(() => {
            window.clearInterval(id);
        }, timeout);
    }

    private addListeners(): void {
        this.canvas.addEventListener('mouseenter', evt => {
            this.isInput = true;
            this.delta = undefined;
        }, true);

        this.canvas.addEventListener('touchstart', evt => {
            this.isInput = true;
            this.delta = undefined;
            this.mousePos = this.getTouchPos(this.canvas, evt);
            this.oldMousePos = this.mousePos;
        }, true);

        this.canvas.addEventListener('mousemove', evt => {
            this.oldMousePos = this.mousePos;
            this.mousePos = this.getMousePos(this.canvas, evt);
        }, false);
        
        this.canvas.addEventListener('touchmove', evt => {
            this.oldMousePos = this.mousePos;
            this.mousePos = this.getTouchPos(this.canvas, evt);
        }, false);

        this.canvas.addEventListener('touchend', evt => {
            this.delta = {
                x: this.mousePos.x - this.oldMousePos.x,
                y: this.mousePos.y - this.oldMousePos.y,
            };

            this.setTimedInterval(() => {
                if (this.delta) {
                    this.mousePos = {
                        x: this.mousePos.x + this.delta.x * this.lastInput / 250,
                        y: this.mousePos.y + this.delta.y * this.lastInput / 250,
                    };
                }
            }, 18, 1000);
            this.isInput = false;
        }, false);

        this.canvas.addEventListener('mouseleave', evt => {
            this.isInput = false;
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
            this.render();
        }
    }

    private addGLProperties(): void {
        if (this.gl) {
            const positionLocation: number = this.gl.getAttribLocation(this.program, 'position');
            this.gl.enableVertexAttribArray(positionLocation);
            this.gl.vertexAttribPointer(positionLocation, 2, this.gl.FLOAT, true, 0, 0);
            const mousePosition: WebGLUniformLocation | null = this.gl.getUniformLocation(
                this.program,
                'mouse'
            );
            this.gl.uniform2f(mousePosition, this.mousePos.x, this.mousePos.y);
            const resolutionPosition: WebGLUniformLocation | null = this.gl.getUniformLocation(
                this.program,
                'resolution'
            );
            this.gl.uniform2f(
                resolutionPosition,
                this.canvas.width,
                this.canvas.height
            );
            const rotationPosition: WebGLUniformLocation | null = this.gl.getUniformLocation(this.program, 'rotation');
            this.gl.uniform2f(rotationPosition, 0.5, 0.8);
            const timePosition: WebGLUniformLocation | null = this.gl.getUniformLocation(this.program, 'time');
            this.gl.uniform1f(timePosition, (new Date().getTime() - this.start) / 1000);
            const strengthPosition: WebGLUniformLocation | null = this.gl.getUniformLocation(this.program, 'strength');
            this.gl.uniform1f(strengthPosition, this.lastInput / 100.0);
            const offsetPosition: WebGLUniformLocation | null = this.gl.getUniformLocation(this.program, 'offset');
            this.gl.uniform2f(offsetPosition, 0, 0);
            const pitchPosition: WebGLUniformLocation | null = this.gl.getUniformLocation(this.program, 'pitch');
            this.gl.uniform2f(pitchPosition, 80, 80);
        }
    }

    private render(): void {
        if (this.gl) {
            this.addGLProperties();
            this.gl.drawArrays(this.gl.TRIANGLES, 0, 6);
            requestAnimationFrame(() => this.render());
            if (!this.isInput && this.lastInput > 3) {
                this.lastInput -= 2;
            } else if (this.isInput && this.lastInput < 150) {
                this.lastInput += 5;
            }
        }
    }
}

export { Renderer };
