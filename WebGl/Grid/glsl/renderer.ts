declare function require(name:string);
let vertexShaderSource = require('./2d-vertex-shader.glsl');
let fragmentShaderSource = require('./2d-fragment-shader.glsl');

class Renderer  {
    gl:WebGLRenderingContext;
    program:WebGLProgram;
    canvas:HTMLCanvasElement = <HTMLCanvasElement> document.getElementById('glscreen');
    mousePos = { x: 0.0, y: 0.0 };
    start = new Date().getTime();
    
    doStuff(){
        window.onload = () => this.init();
    }

    getMousePos(canvas:HTMLCanvasElement, evt:Event) {
        var rect = canvas.getBoundingClientRect();
        var mouseEvt = <MouseEvent> evt;
        var touchEvt = <TouchEvent> evt;
        return {
            x: (mouseEvt.clientX - rect.left) / (rect.right - rect.left) * canvas.width,
            y: (mouseEvt.clientY - rect.bottom) / (rect.top - rect.bottom) * canvas.height
        };
    }

    viewportToPixels(value:string, isHeight:boolean): number {
        var parts = value.match(/([0-9\.]+)(vh|vw)/);
        var q = Number(parts[1]);
        if(isHeight)
            return window.innerHeight * (q/100);
        else
            return window.innerWidth * (q/100);
    }

    getSize() {
        this.canvas.width = this.viewportToPixels(this.canvas.style.width, false);
        this.canvas.height = this.viewportToPixels(this.canvas.style.height, true);
    }

    addListeners() {
        this.canvas.addEventListener('mousemove', (evt) => this.mousePos = this.getMousePos(this.canvas, evt), false);
        this.canvas.addEventListener('touchmove', (evt) => this.mousePos = this.getMousePos(this.canvas, evt), false);
        window.onresize = () => this.getSize();
    }

    init() {
        var vertexShader;
        var fragmentShader;

        this.addListeners();
        this.getSize();

        this.gl = this.canvas.getContext('experimental-webgl');

        var buffer = this.gl.createBuffer();
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

    addGLProperties() {
        var positionLocation = this.gl.getAttribLocation(this.program, "position");
        this.gl.enableVertexAttribArray(positionLocation);
        this.gl.vertexAttribPointer(positionLocation, 2, this.gl.FLOAT, true, 0, 0);

        var mousePosition = this.gl.getUniformLocation(this.program, "mouse");
        this.gl.uniform2f(mousePosition, this.mousePos.x, this.mousePos.y);

        var resolutionPosition = this.gl.getUniformLocation(this.program, "resolution");
        this.gl.uniform2f(resolutionPosition, this.canvas.width, this.canvas.height);

        var rotationPosition = this.gl.getUniformLocation(this.program, "rotation");
        this.gl.uniform2f(rotationPosition, 0.5, 0.8);

        var timePosition = this.gl.getUniformLocation(this.program, "time");
        this.gl.uniform1f(timePosition, (new Date().getTime() - this.start) / 1000);

        var offsetPosition = this.gl.getUniformLocation(this.program, "offset");
        this.gl.uniform2f(offsetPosition, 0, 0);

        var pitchPosition = this.gl.getUniformLocation(this.program, "pitch");
        this.gl.uniform2f(pitchPosition, 80, 80);
    }

    render() {
        window.requestAnimationFrame(() => this.render());

        this.addGLProperties();

        this.gl.drawArrays(this.gl.TRIANGLES, 0, 6);
    }
}

export { Renderer };