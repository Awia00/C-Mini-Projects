precision highp float;

uniform float time;
uniform float strength;
uniform vec2 mouse;
uniform vec2 resolution;
uniform vec2 rotation;
uniform vec2 pitch; // number of grids

vec4 grid(float modX, float modY, float distance) {
  float density = 0.5 * (1.0 - abs((2.0-min(modX, modY))/2.0)); // grid alpa 0.5 and then anti alias.
  float fade = max(0.0, 0.9-distance/length(resolution)); // 1.2 due to some scaling
  return vec4(0.0, 0.0, 0.0, density*fade);
}

void main() {
  float gravity = 40.0*strength;// + 1.5*sin(time*8.0);
  float reach = 10000.0;

  vec2 delta = mouse-gl_FragCoord.xy;
  float distance = length(delta);
  vec2 newPos = pitch + delta*gravity/((distance*distance + reach)); // gravity

  float modX = mod(reach + gl_FragCoord.x, newPos.x);
  float modY = mod(reach + gl_FragCoord.y, newPos.y);
  
  gl_FragColor = grid(modX, modY, distance);
}