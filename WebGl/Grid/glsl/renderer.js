exports.render = function() {
    var gl;
    var canvas;
    var buffer;
    var mousePos = { x: 0.0, y: 0.0 };
    var start = new Date().getTime();

    window.onload = init;

    function getMousePos(canvas, evt) {
        var rect = canvas.getBoundingClientRect();
        return {
            x: (evt.clientX - rect.left) / (rect.right - rect.left) * canvas.width,
            y: (evt.clientY - rect.bottom) / (rect.top - rect.bottom) * canvas.height
        };
    }

    function viewportToPixels(value) {
        var parts = value.match(/([0-9\.]+)(vh|vw)/)
        var q = Number(parts[1])
        var side = window[['innerHeight', 'innerWidth'][
            ['vh', 'vw'].indexOf(parts[2])
        ]]
        return side * (q / 100)
    }

    function getSize() {
        canvas.width = viewportToPixels(canvas.style.width);
        canvas.height = viewportToPixels(canvas.style.height);
    }

    function init() {

        var shaderScript;
        var shaderSource;
        var vertexShader;
        var fragmentShader;

        canvas = document.getElementById('glscreen');
        canvas.addEventListener('mousemove', function(evt) {
            mousePos = getMousePos(canvas, evt);
        }, false);
        window.addEventListener('resize', function(evt) {
            getSize();
        });
        getSize();

        gl = canvas.getContext('experimental-webgl');


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

    function render() {
        window.requestAnimationFrame(render, canvas);

        positionLocation = gl.getAttribLocation(program, "position");
        gl.enableVertexAttribArray(positionLocation);
        gl.vertexAttribPointer(positionLocation, 2, gl.FLOAT, true, 0, 0);

        mousePosition = gl.getUniformLocation(program, "mouse");
        gl.uniform2f(mousePosition, mousePos.x, mousePos.y);

        resolutionPosition = gl.getUniformLocation(program, "resolution");
        gl.uniform2f(resolutionPosition, canvas.width, canvas.height);

        rotationPosition = gl.getUniformLocation(program, "rotation");
        gl.uniform2f(rotationPosition, 0.5, 0.8);

        timePosition = gl.getUniformLocation(program, "time");
        gl.uniform1f(timePosition, (new Date().getTime() - start) / 1000);

        offsetPosition = gl.getUniformLocation(program, "offset");
        gl.uniform2f(offsetPosition, 0, 0);

        pitchPosition = gl.getUniformLocation(program, "pitch");
        gl.uniform2f(pitchPosition, 80, 80);

        gl.drawArrays(gl.TRIANGLES, 0, 6);
    }
};