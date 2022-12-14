import { Pipe, PipeTransform } from '@angular/core'

@Pipe({
    name: 'isStatusCodeError',
    standalone: true
})
export class IsStatusCodeErrorPipe implements PipeTransform {
    transform(statusCode: number | null, errors: number[] = [400, 500]): boolean {
        return statusCode ? errors.indexOf(statusCode) > -1 : false
    }
}
