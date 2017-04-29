import { UtPage } from './app.po';

describe('ut App', () => {
  let page: UtPage;

  beforeEach(() => {
    page = new UtPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
