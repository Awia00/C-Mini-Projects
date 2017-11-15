// index.
import { Renderer, IRenderable } from "./glsl/renderer";

let renderer:IRenderable = new Renderer();
renderer.renderOnCanvas(<HTMLCanvasElement> document.getElementById("glscreen"));