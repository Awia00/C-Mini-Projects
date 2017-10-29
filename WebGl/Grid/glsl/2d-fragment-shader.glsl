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

  vec4 mandelbrot() {
	vec2 z;
    float x,y;
    int steps;
    float normalizedX = (gl_FragCoord.x - 320.0) / 640.0 * ZOOM_FACTOR *
                        (640.0 / 480.0) - X_OFFSET;
    float normalizedY = (gl_FragCoord.y - 240.0) / 480.0 * ZOOM_FACTOR;
 
    z.x = normalizedX;
    z.y = normalizedY;
 
    for (int i=0;i<NUM_STEPS;i++) {
 
	steps = i;
 
        x = (z.x * z.x - z.y * z.y) + normalizedX;
        y = (z.y * z.x + z.x * z.y) + normalizedY;
 
        if((x * x + y * y) > 4.0) {
		  break;
		}
 
        z.x = x;
        z.y = y;
    }
 
    if (steps == NUM_STEPS-1) {
		return vec4(1.0, 0.0, 0.0, 1.0);
    } else {
      	return vec4(0.0, 0.0, 0.0, 1.0);
    }
  }

  vec4 spotlight() {
	vec2 mouse_distance = mouse - (gl_FragCoord.xy / resolution);
	float red = 1.0 - length(mouse_distance);
	return vec4(red, 0.0, 0, 1.0);
  }

  vec4 funky(){
	vec2 pos = ( gl_FragCoord.xy / resolution.xy ) * 26.0 - 13.0;
	float x = sin(time + length(pos.xy)) + cos((mouse.x * 10.0) + pos.x);
	float y = cos(time + length(pos.xy)) + sin((mouse.y * 10.0)+ pos.y);
	return vec4( x * 0.5, y * 0.5, x * y, 1.0 );
  }

  void main() {
    gl_FragColor = spotlight() + funky();
  }