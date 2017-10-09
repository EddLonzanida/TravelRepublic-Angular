import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'ratingConverter'
})
export class RatingConverterPipe implements PipeTransform {

  transform(value: any, args?: any): any {
    return value / 10;
  }

}
