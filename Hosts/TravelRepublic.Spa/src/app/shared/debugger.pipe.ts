import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'debugger'
})
export class DebuggerPipe implements PipeTransform {

  transform(value: any, args?: any): any {

    console.warn('===myDebugger');
    console.warn(value);

    return value;

  }
}
