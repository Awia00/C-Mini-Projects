precision highp float;

uniform float time;
uniform vec2 resolution;
uniform vec2 rotation;
uniform vec2 pitch; // number of grids
uniform vec3 presses[7];

vec4 grid(float modX, float modY, float distance, float strength) {
	float density = 0.5 * (1.0 - abs((2.0-min(modX, modY))/2.0)); // grid alpa 0.5 and then anti alias.
	float fade = max(0.0, 0.9-distance/length(resolution)); // 1.2 due to some scaling
	return vec4(0.0, 0.0, 0.0, density*fade*min(1.0, max(0.7, strength)));
}

void main() {
	float gravity = 40.0;// + 1.5*sin(time*8.0);
	float reach = 10000.0;

    vec2 pull = vec2(0.0, 0.0);
    float strength = 0.0;
    float minDistance = 10000.0;
    for(int i = 0; i<2; i++){
        vec3 press = presses[i];
        vec2 delta = press.xy-gl_FragCoord.xy;
        float distance = length(delta);
        pull += delta * (gravity*press.z / (distance*distance + reach));

        strength = max(strength, press.z);
        minDistance = min(minDistance, distance);
    }

    vec2 newPos = pitch + pull;

    float modX = mod(reach + gl_FragCoord.x, newPos.x);
    float modY = mod(reach + gl_FragCoord.y, newPos.y);

	gl_FragColor = grid(modX, modY, minDistance, strength); // 
}