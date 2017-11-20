// index.
import { Renderer, IRenderable } from "./renderer";
import * as $ from "jquery";

let renderer:IRenderable = new Renderer();
renderer.renderOnCanvas(<HTMLElement> document.getElementById("grid"));