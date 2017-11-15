!function(t){function r(i){if(e[i])return e[i].exports;var n=e[i]={i:i,l:!1,exports:{}};return t[i].call(n.exports,n,n.exports,r),n.l=!0,n.exports}var e={};r.m=t,r.c=e,r.d=function(t,e,i){r.o(t,e)||Object.defineProperty(t,e,{configurable:!1,enumerable:!0,get:i})},r.n=function(t){var e=t&&t.__esModule?function(){return t.default}:function(){return t};return r.d(e,"a",e),e},r.o=function(t,r){return Object.prototype.hasOwnProperty.call(t,r)},r.p="",r(r.s=3)}([function(t,r,e){"use strict";r.__esModule=!0;var i=e(1),n=e(2),o=function(){function t(){this.mousePos={x:0,y:0},this.start=(new Date).getTime()}return t.prototype.renderOnCanvas=function(t){var r=this;this.canvas=t,window.onload=function(){return r.init()}},t.prototype.getMousePos=function(t,r){var e=t.getBoundingClientRect(),i=r;return{x:(i.clientX-e.left)/(e.right-e.left)*t.width,y:(i.clientY-e.bottom)/(e.top-e.bottom)*t.height}},t.prototype.viewportToPixels=function(t,r){var e=t.match(/([0-9\.]+)(vh|vw)/),i=Number(e[1]);return r?window.innerHeight*(i/100):window.innerWidth*(i/100)},t.prototype.getSize=function(){this.canvas.width=this.viewportToPixels(this.canvas.style.width,!1),this.canvas.height=this.viewportToPixels(this.canvas.style.height,!0)},t.prototype.addListeners=function(){var t=this;this.canvas.addEventListener("mousemove",function(r){return t.mousePos=t.getMousePos(t.canvas,r)},!1),this.canvas.addEventListener("touchmove",function(r){return t.mousePos=t.getMousePos(t.canvas,r)},!1),window.onresize=function(){return t.getSize()}},t.prototype.init=function(){var t,r;this.addListeners(),this.getSize(),this.gl=this.canvas.getContext("experimental-webgl");var e=this.gl.createBuffer();this.gl.bindBuffer(this.gl.ARRAY_BUFFER,e),this.gl.bufferData(this.gl.ARRAY_BUFFER,new Float32Array([-1,-1,1,-1,-1,1,-1,1,1,-1,1,1]),this.gl.STATIC_DRAW),this.gl.viewport(0,0,this.gl.drawingBufferWidth,this.gl.drawingBufferHeight),t=this.gl.createShader(this.gl.VERTEX_SHADER),this.gl.shaderSource(t,i),this.gl.compileShader(t),r=this.gl.createShader(this.gl.FRAGMENT_SHADER),this.gl.shaderSource(r,n),this.gl.compileShader(r),this.program=this.gl.createProgram(),this.gl.attachShader(this.program,t),this.gl.attachShader(this.program,r),this.gl.linkProgram(this.program),this.gl.useProgram(this.program),this.render()},t.prototype.addGLProperties=function(){var t=this.gl.getAttribLocation(this.program,"position");this.gl.enableVertexAttribArray(t),this.gl.vertexAttribPointer(t,2,this.gl.FLOAT,!0,0,0);var r=this.gl.getUniformLocation(this.program,"mouse");this.gl.uniform2f(r,this.mousePos.x,this.mousePos.y);var e=this.gl.getUniformLocation(this.program,"resolution");this.gl.uniform2f(e,this.canvas.width,this.canvas.height);var i=this.gl.getUniformLocation(this.program,"rotation");this.gl.uniform2f(i,.5,.8);var n=this.gl.getUniformLocation(this.program,"time");this.gl.uniform1f(n,((new Date).getTime()-this.start)/1e3);var o=this.gl.getUniformLocation(this.program,"offset");this.gl.uniform2f(o,0,0);var s=this.gl.getUniformLocation(this.program,"pitch");this.gl.uniform2f(s,80,80)},t.prototype.render=function(){var t=this;window.requestAnimationFrame(function(){return t.render()}),this.addGLProperties(),this.gl.drawArrays(this.gl.TRIANGLES,0,6)},t}();r.Renderer=o},function(t,r){t.exports="\r\nattribute vec2 position;\r\n  void main() {\r\n    gl_Position = vec4(position, 0, 1);\r\n  }"},function(t,r){t.exports="precision mediump float;\r\n\r\nuniform float time;\r\nuniform vec2 mouse;\r\nuniform vec2 resolution;\r\nuniform vec2 rotation;\r\nuniform vec2 pitch; // number of grids\r\n\r\nvec4 grid(float modX, float modY, float distance) {\r\n  float strength = 0.5 * (1.0 - abs((2.0-min(modX, modY))/2.0)); // grid alpa 0.5 and then anti alias.\r\n  float fade = max(0.0, 0.9-distance/length(resolution)); // 1.2 due to some scaling\r\n  return vec4(0.0, 0.0, 0.0, strength*fade);\r\n}\r\n\r\nvoid main() {\r\n  float gravity = 50.0;// + 50.0*sin(time*4.0);\r\n  float reach = 10000.0;\r\n\r\n  vec2 delta = mouse-gl_FragCoord.xy;\r\n  float distance = length(delta);\r\n  vec2 newPos = pitch + delta*gravity/((distance*distance + reach)); // gravity\r\n\r\n  float modX = mod(reach + gl_FragCoord.x, newPos.x);\r\n  float modY = mod(reach + gl_FragCoord.y, newPos.y);\r\n  \r\n  gl_FragColor = grid(modX, modY, distance);\r\n}"},function(t,r,e){e(4),e(0),e(1),t.exports=e(2)},function(t,r,e){"use strict";r.__esModule=!0,(new(e(0).Renderer)).renderOnCanvas(document.getElementById("glscreen"))}]);