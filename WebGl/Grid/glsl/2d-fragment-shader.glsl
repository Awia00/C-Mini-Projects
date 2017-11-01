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

uniform vec2 offset; // e.g. [-0.023500000000000434 0.9794000000000017], currently the same as the x/y offset in the mvMatrix
uniform vec2 pitch;  // e.g. [50 50]

void main() {
  float scaleFactor = 1000.0;
  float displacement = 100000.0;

  vec2 pitchGravitated = pitch;
  
  float deltaX = abs(gl_FragCoord.x-mouse.x);
  float deltaY = abs(mouse.y-gl_FragCoord.y);
  float distance = sqrt(deltaX*deltaX + deltaY*deltaY);
  float newPosX = pitch[0] + displacement/((distance*distance));
  float newPosY = pitch[1] + displacement/((distance*distance));
  float offX = (scaleFactor * offset[0]) + gl_FragCoord.x;
  float offY = (scaleFactor * offset[1]) + (gl_FragCoord.y);

  float modX = mod(offX, newPosX);
  float modY = mod(offY, newPosY);
  if (modX <= 2.5 ||
      modY <= 2.5) {
    gl_FragColor = vec4(0.0, 0.0, 0.0, 0.15*(1.0-distance/resolution.x));
  } else {
    gl_FragColor = vec4(1.0, 1.0, 1.0, 1.0);
  }
}