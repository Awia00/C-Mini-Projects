declare function require(name:string):any;
let vertexShaderSource:any = require("./2d-vertex-shader.glsl");
let fragmentShaderSource:any = require("./2d-fragment-shader.glsl");

export interface IRenderable {
    renderOnCanvas(canvas : HTMLCanvasElement): void;
}

class Renderer implements IRenderable {
    gl:WebGLRenderingContext;
    program:WebGLProgram;
    canvas:HTMLCanvasElement;
    mousePos = { x: 0.0, y: 0.0 };
    start = new Date().getTime();

    renderOnCanvas (canvas : HTMLCanvasElement): void {
        this.canvas = canvas;
        window.onload = () => this.init();
    }

    getMousePos(canvas:HTMLCanvasElement, evt:Event): {x:number, y:number} {
        var rect : ClientRect = canvas.getBoundingClientRect();
        var mouseEvt : MouseEvent = <MouseEvent> evt;
        return {
            x: (mouseEvt.clientX - rect.left) / (rect.right - rect.left) * canvas.width,
            y: (mouseEvt.clientY - rect.bottom) / (rect.top - rect.bottom) * canvas.height
        };
    }

    viewportToPixels(value:string, isHeight:boolean): number {
        var parts: RegExpMatchArray = value.match(/([0-9\.]+)(vh|vw)/);
        var q: number = Number(parts[1]);
        if(isHeight) {
            return window.innerHeight * (q / 100);
        } else {
            return window.innerWidth * (q / 100);
        }
    }

    getSize(): void {
        this.canvas.width = this.viewportToPixels(this.canvas.style.width, false);
        this.canvas.height = this.viewportToPixels(this.canvas.style.height, true);
    }

    addListeners(): void {
        this.canvas.addEventListener("mousemove", (evt) => this.mousePos = this.getMousePos(this.canvas, evt), false);
        this.canvas.addEventListener("touchmove", (evt) => this.mousePos = this.getMousePos(this.canvas, evt), false);
        window.onresize = () => this.getSize();
    }

    init(): void {
        var vertexShader: WebGLShader;
        var fragmentShader: WebGLShader;

        this.addListeners();
        this.getSize();

        this.gl = this.canvas.getContext("experimental-webgl");

        var buffer: WebGLBuffer = this.gl.createBuffer();
        this.gl.bindBuffer(this.gl.ARRAY_BUFFER, buffer);
        this.gl.bufferData(
            this.gl.ARRAY_BUFFER,
            new Float32Array([-1.0, -1.0,
                1.0, -1.0, -1.0, 1.0, -1.0, 1.0,
                1.0, -1.0,
                1.0, 1.0
            ]),
            this.gl.STATIC_DRAW
        );

        this.gl.viewport(0, 0, this.gl.drawingBufferWidth, this.gl.drawingBufferHeight);

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

    addGLProperties(): void {
        var positionLocation:number = this.gl.getAttribLocation(this.program, "position");
        this.gl.enableVertexAttribArray(positionLocation);
        this.gl.vertexAttribPointer(positionLocation, 2, this.gl.FLOAT, true, 0, 0);

        var mousePosition:WebGLUniformLocation = this.gl.getUniformLocation(this.program, "mouse");
        this.gl.uniform2f(mousePosition, this.mousePos.x, this.mousePos.y);

        var resolutionPosition:WebGLUniformLocation = this.gl.getUniformLocation(this.program, "resolution");
        this.gl.uniform2f(resolutionPosition, this.canvas.width, this.canvas.height);

        var rotationPosition:WebGLUniformLocation = this.gl.getUniformLocation(this.program, "rotation");
        this.gl.uniform2f(rotationPosition, 0.5, 0.8);

        var timePosition:WebGLUniformLocation = this.gl.getUniformLocation(this.program, "time");
        this.gl.uniform1f(timePosition, (new Date().getTime() - this.start) / 1000);

        var offsetPosition:WebGLUniformLocation = this.gl.getUniformLocation(this.program, "offset");
        this.gl.uniform2f(offsetPosition, 0, 0);

        var pitchPosition:WebGLUniformLocation = this.gl.getUniformLocation(this.program, "pitch");
        this.gl.uniform2f(pitchPosition, 80, 80);
    }

    render(): void {
        window.requestAnimationFrame(() => this.render());

        this.addGLProperties();

        this.gl.drawArrays(this.gl.TRIANGLES, 0, 6);
    }
}

export { Renderer };
