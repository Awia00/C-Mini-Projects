precision mediump float;

uniform float time;
uniform vec2 mouse;
uniform vec2 resolution;
uniform vec2 rotation;

uniform vec2 offset;
uniform vec2 pitch;  // e.g. [50 50]

void main() {
  float displacement = 1000.0;// + 50.0*sin(time*4.0);
  float reach = 50.0;

  float deltaX = mouse.x-gl_FragCoord.x;
  float deltaY = mouse.y-gl_FragCoord.y;
  float distance = sqrt(deltaX*deltaX + deltaY*deltaY) + reach;
  float newPosX = pitch[0] + deltaX*displacement/((distance*distance));
  float newPosY = pitch[1] + deltaY*displacement/((distance*distance));

  float modX = mod(gl_FragCoord.x, newPosX);
  float modY = mod(gl_FragCoord.y, newPosY);
  if (modX <= 2.5 ||
      modY <= 2.5) {
    gl_FragColor = vec4(0.0, 0.0, 0.0, 0.15*(1.0-distance/resolution.x));
  } else {
    gl_FragColor = vec4(1.0, 1.0, 1.0, 1.0);
  }
}