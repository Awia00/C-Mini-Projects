exports.render = function() {
    var gl;
    var canvas;
    var buffer;
    var mousePos = { x: 0.0, y: 0.0 };

    window.onload = init;


    function init() {

        var shaderScript;
        var shaderSource;
        var vertexShader;
        var fragmentShader;

        canvas = document.getElementById('glscreen');
        canvas.addEventListener('mousemove', function(evt) {
            mousePos = getMousePos(canvas, evt);
        }, false);
        gl = canvas.getContext('experimental-webgl');
        canvas.width = 640;
        canvas.height = 480;

        buffer = gl.createBuffer();
        gl.bindBuffer(gl.ARRAY_BUFFER, buffer);
        gl.bufferData(
            gl.ARRAY_BUFFER,
            new Float32Array([-1.0, -1.0,
                1.0, -1.0, -1.0, 1.0, -1.0, 1.0,
                1.0, -1.0,
                1.0, 1.0
            ]),
            gl.STATIC_DRAW
        );

        gl.viewport(0, 0, gl.drawingBufferWidth, gl.drawingBufferHeight);

        shaderSource = require('./2d-vertex-shader.glsl');
        vertexShader = gl.createShader(gl.VERTEX_SHADER);
        gl.shaderSource(vertexShader, shaderSource);
        gl.compileShader(vertexShader);

        shaderSource = require('./2d-fragment-shader.glsl');
        fragmentShader = gl.createShader(gl.FRAGMENT_SHADER);
        gl.shaderSource(fragmentShader, shaderSource);
        gl.compileShader(fragmentShader);

        program = gl.createProgram();
        gl.attachShader(program, vertexShader);
        gl.attachShader(program, fragmentShader);
        gl.linkProgram(program);
        gl.useProgram(program);

        render();

    }

    function getMousePos(canvas, evt) {
        var rect = canvas.getBoundingClientRect();
        return {
            x: evt.clientX - rect.left,
            y: rect.bottom - evt.clientY
        };
    }

    function render() {

        window.requestAnimationFrame(render, canvas);

        positionLocation = gl.getAttribLocation(program, "a_position");
        gl.enableVertexAttribArray(positionLocation);
        gl.vertexAttribPointer(positionLocation, 2, gl.FLOAT, false, 0, 0);

        mousePosition = gl.getUniformLocation(program, "mouse");
        gl.uniform2f(mousePosition, mousePos.x / canvas.width, mousePos.y / canvas.height);

        resolutionPosition = gl.getUniformLocation(program, "resolution");
        gl.uniform2f(resolutionPosition, canvas.width, canvas.height);

        gl.drawArrays(gl.TRIANGLES, 0, 6);
    }
};