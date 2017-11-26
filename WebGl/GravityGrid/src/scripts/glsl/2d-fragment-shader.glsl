precision highp float;

uniform float time;
uniform float gravity;
uniform float reach;
uniform int amtPresses;
uniform vec2 resolution;
uniform vec2 rotation;
uniform vec2 pitch; // number of grids
uniform vec3 presses[6];

vec4 grid(float modX, float modY, float strength) {
	float density = 0.4 * (1.0 - abs((2.0-min(modX, modY))/2.0)); // grid alpa 0.5 and then anti alias.
	return vec4(0.0, 0.0, 0.0, density*strength);
}

void main() {
    float resLength = length(resolution);

    vec2 pull = vec2(0.0, 0.0);
    float strength = 0.15;
    for(int i = 0; i<=6; i++){
        if (amtPresses==i)
            break;
        vec3 press = presses[i];
        vec2 delta = press.xy-gl_FragCoord.xy;
        float distance = length(delta);
        pull += delta * (gravity*press.z / (distance*distance + reach));
        
        float fade = max(0.0, 0.9-distance/resLength); // 1.2 due to some scaling
        float dist = min(1.0, max(0.0, press.z));
        strength = max(strength, fade*dist);
    }

    vec2 newPos = pitch + pull;

    float modX = mod(reach + gl_FragCoord.x, newPos.x);
    float modY = mod(reach + gl_FragCoord.y, newPos.y);

	gl_FragColor = grid(modX, modY, strength);
}