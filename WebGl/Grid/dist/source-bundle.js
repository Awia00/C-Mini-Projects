/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, {
/******/ 				configurable: false,
/******/ 				enumerable: true,
/******/ 				get: getter
/******/ 			});
/******/ 		}
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = 0);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(1);


/***/ }),
/* 1 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

Object.defineProperty(exports, "__esModule", { value: true });
// index.
const renderer_1 = __webpack_require__(2);
let renderer = new renderer_1.Renderer();
renderer.renderOnCanvas(document.getElementById("grid"));


/***/ }),
/* 2 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

Object.defineProperty(exports, "__esModule", { value: true });
let vertexShaderSource = __webpack_require__(3);
let fragmentShaderSource = __webpack_require__(4);
class Renderer {
    constructor() {
        this.mousePos = { x: 0.0, y: 0.0 };
        this.oldMousePos = { x: 0.0, y: 0.0 };
        this.delta = undefined;
        this.start = new Date().getTime();
        this.lastInput = 3;
        this.isInput = false;
    }
    renderOnCanvas(div) {
        this.canvas = document.createElement("canvas");
        this.canvas.style.width = "100%";
        this.canvas.style.height = "100%";
        div.appendChild(this.canvas);
        this.gl = this.canvas.getContext("experimental-webgl");
        window.onload = () => this.init();
    }
    getMousePos(canvas, evt) {
        var rect = canvas.getBoundingClientRect();
        var mouseEvt = evt;
        return {
            x: (mouseEvt.clientX - rect.left) / (rect.right - rect.left) * canvas.width,
            y: (mouseEvt.clientY - rect.bottom) / (rect.top - rect.bottom) * canvas.height
        };
    }
    getTouchPos(canvas, evt) {
        var rect = canvas.getBoundingClientRect();
        var touchEvt = evt;
        return {
            x: (touchEvt.touches[0].clientX - rect.left) / (rect.right - rect.left) * canvas.width,
            y: (touchEvt.touches[0].clientY - rect.bottom) / (rect.top - rect.bottom) * canvas.height
        };
    }
    viewportToPixels(value, isHeight) {
        var parts = value.match(/([0-9\.]+)(vh|vw)/);
        var q = Number(parts[1]);
        if (isHeight) {
            return window.innerHeight * (q / 100);
        }
        else {
            return window.innerWidth * (q / 100);
        }
    }
    getSize() {
        this.canvas.width = this.canvas.clientWidth;
        this.canvas.height = this.canvas.clientHeight;
    }
    setTimedInterval(callback, delay, timeout) {
        var id = window.setInterval(callback, delay);
        window.setTimeout(() => {
            window.clearInterval(id);
        }, timeout);
    }
    addListeners() {
        this.canvas.addEventListener("mouseenter", (evt) => {
            this.isInput = true;
            this.delta = undefined;
        }, true);
        this.canvas.addEventListener("touchstart", (evt) => {
            this.isInput = true;
            this.delta = undefined;
            this.mousePos = this.getTouchPos(this.canvas, evt);
        }, true);
        this.canvas.addEventListener("mousemove", (evt) => {
            this.oldMousePos = this.mousePos;
            this.mousePos = this.getMousePos(this.canvas, evt);
        }, false);
        this.canvas.addEventListener("touchmove", (evt) => {
            this.oldMousePos = this.mousePos;
            this.mousePos = this.getTouchPos(this.canvas, evt);
        }, false);
        this.canvas.addEventListener("touchend", (evt) => {
            this.delta = { x: this.mousePos.x - this.oldMousePos.x, y: this.mousePos.y - this.oldMousePos.y };
            this.setTimedInterval(() => {
                if (this.delta) {
                    this.mousePos = {
                        x: this.mousePos.x + this.delta.x * (this.lastInput) / 250,
                        y: this.mousePos.y + this.delta.y * (this.lastInput) / 250
                    };
                }
            }, 18, 1000);
            this.isInput = false;
        }, false);
        this.canvas.addEventListener("mouseleave", (evt) => {
            this.isInput = false;
        }, false);
        window.onresize = () => setTimeout(() => this.getSize(), 1);
    }
    init() {
        var vertexShader;
        var fragmentShader;
        this.addListeners();
        this.getSize();
        var buffer = this.gl.createBuffer();
        this.gl.bindBuffer(this.gl.ARRAY_BUFFER, buffer);
        this.gl.bufferData(this.gl.ARRAY_BUFFER, new Float32Array([-1.0, -1.0,
            1.0, -1.0, -1.0, 1.0, -1.0, 1.0,
            1.0, -1.0,
            1.0, 1.0
        ]), this.gl.STATIC_DRAW);
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
        var strengthPosition = this.gl.getUniformLocation(this.program, "strength");
        this.gl.uniform1f(strengthPosition, this.lastInput / 100.0);
        var offsetPosition = this.gl.getUniformLocation(this.program, "offset");
        this.gl.uniform2f(offsetPosition, 0, 0);
        var pitchPosition = this.gl.getUniformLocation(this.program, "pitch");
        this.gl.uniform2f(pitchPosition, 80, 80);
    }
    render() {
        this.addGLProperties();
        this.gl.drawArrays(this.gl.TRIANGLES, 0, 6);
        requestAnimationFrame(() => this.render());
        if (!this.isInput && this.lastInput > 3) {
            this.lastInput -= 2;
        }
        else if (this.isInput && this.lastInput < 150) {
            this.lastInput += 5;
        }
    }
}
exports.Renderer = Renderer;


/***/ }),
/* 3 */
/***/ (function(module, exports) {

module.exports = "\r\nattribute vec2 position;\r\n  void main() {\r\n    gl_Position = vec4(position, 0, 1);\r\n  }"

/***/ }),
/* 4 */
/***/ (function(module, exports) {

module.exports = "precision highp float;\r\n\r\nuniform float time;\r\nuniform float strength;\r\nuniform vec2 mouse;\r\nuniform vec2 resolution;\r\nuniform vec2 rotation;\r\nuniform vec2 pitch; // number of grids\r\n\r\nvec4 grid(float modX, float modY, float distance) {\r\n  float density = 0.5 * (1.0 - abs((2.0-min(modX, modY))/2.0)); // grid alpa 0.5 and then anti alias.\r\n  float fade = max(0.0, 0.9-distance/length(resolution)); // 1.2 due to some scaling\r\n  return vec4(0.0, 0.0, 0.0, density*fade*min(1.0, max(0.7, strength)));\r\n}\r\n\r\nvoid main() {\r\n  float gravity = 40.0*strength;// + 1.5*sin(time*8.0);\r\n  float reach = 10000.0;\r\n\r\n  vec2 delta = mouse-gl_FragCoord.xy;\r\n  float distance = length(delta);\r\n  vec2 newPos = pitch + delta*gravity/((distance*distance + reach)); // gravity\r\n\r\n  float modX = mod(reach + gl_FragCoord.x, newPos.x);\r\n  float modY = mod(reach + gl_FragCoord.y, newPos.y);\r\n  \r\n  gl_FragColor = grid(modX, modY, distance);\r\n}"

/***/ })
/******/ ]);
//# sourceMappingURL=source-bundle.js.map