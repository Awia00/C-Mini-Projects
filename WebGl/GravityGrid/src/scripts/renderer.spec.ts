import { Renderer } from './renderer';

describe('Renderer', () => {
  let greeter: Renderer;
  let element: HTMLElement;
  beforeEach(() => {
    greeter = new Renderer();
    element = new HTMLElement();
  });
  it('should not crash', () => {
    expect(greeter.renderGravityGrid(element)).Any();
  });
});
