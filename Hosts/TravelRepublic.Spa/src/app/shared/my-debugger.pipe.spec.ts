import { MyDebuggerPipe } from './my-debugger.pipe';

describe('MyDebuggerPipe', () => {
  it('create an instance', () => {
    const pipe = new MyDebuggerPipe();
    expect(pipe).toBeTruthy();
  });
});
