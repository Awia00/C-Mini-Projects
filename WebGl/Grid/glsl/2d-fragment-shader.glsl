#define NUM_STEPS   50
  #define ZOOM_FACTOR 2.0
  #define X_OFFSET    0.5

#ifdef GL_FRAGMENT_PRECISION_HIGH
precision highp float;
#else
precision mediump float;
#endif

uniform float time;
uniform vec2 mouse;
uniform vec2 resolution;
uniform vec2 rotation;

uniform vec2 offset; // e.g. [-0.023500000000000434 0.9794000000000017], currently the same as the x/y offset in the mvMatrix
uniform vec2 pitch;  // e.g. [50 50]

void main() {
  float displacement = 1000.0;
  float reach = 50.0;

  float deltaX = mouse.x-gl_FragCoord.x;
  float deltaY = mouse.y-gl_FragCoord.y;
  float distance = sqrt(deltaX*deltaX + deltaY*deltaY)+reach;
  float newPosX = pitch[0] + deltaX*displacement/((distance*distance));
  float newPosY = pitch[1] + deltaY*displacement/((distance*distance));
  float offX = (offset[0]) + gl_FragCoord.x;
  float offY = (offset[1]) + (gl_FragCoord.y);

  float modX = mod(offX, newPosX);
  float modY = mod(offY, newPosY);
  if (modX <= 2.5 ||
      modY <= 2.5) {
    gl_FragColor = vec4(0.0, 0.0, 0.0, 0.15*(1.0-distance/resolution.x));
  } else {
    gl_FragColor = vec4(1.0, 1.0, 1.0, 1.0);
  }
}