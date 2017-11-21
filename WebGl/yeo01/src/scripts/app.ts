import '../styles/base.scss';

import { Greeter } from './greeter';

const greeter: Greeter = new Greeter('yeo01');

const el = document.getElementById('greeting');
if (el) {
  el.innerHTML = greeter.greet();
}
