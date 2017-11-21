import '../styles/base.scss';

import { Greeter } from './greeter';

const greeter: Greeter = new Greeter('gravity-grid');

const el = document.getElementById('greeting');
if (el) {
  el.innerHTML = greeter.greet();
}
