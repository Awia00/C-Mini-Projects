import '../styles/base.scss';

import { Greeter } from './greeter';
import { IRenderable, Renderer } from './renderer';

const greeter: Greeter = new Greeter('gravity-grid');
const renderer : IRenderable = new Renderer();

const el = document.getElementById('grid');
if (el) {
  renderer.renderOnCanvas(el);
}
