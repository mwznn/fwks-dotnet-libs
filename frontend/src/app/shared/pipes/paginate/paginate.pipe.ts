import { Pipe, PipeTransform } from '@angular/core'
import { PaginatePipeArgs } from './paginate.pipe.args'

export interface PipeState {
    collection: any[]
    size: number
    slice: any[]
    start: number
    end: number
}

@Pipe({
    name: 'paginate',
    standalone: true,
})
export class PaginatePipe implements PipeTransform {
    state: Record<string, PipeState>

    constructor() {
        this.state = {}
    }

    transform(collection: any[], args: PaginatePipeArgs): any[] {
        const id = args.id ?? '__DEFAULT__'
        const pageSize = args.pageSize ?? 10
        const start = (args.page - 1) * pageSize
        const end = args.page * pageSize

        const isEqual = this.stateIsEqual(id, collection, start, end)

        if (isEqual) {
            return this.state[id].slice
        } else {
            const slice = collection.slice(start, end)
            this.saveState(id, collection, slice, start, end)
            return slice
        }
    }

    private saveState(id: string, collection: any[], slice: any[], start: number, end: number): void {
        this.state[id] = {
            collection,
            size: collection.length,
            slice,
            start,
            end,
        }
    }

    private stateIsEqual(id: string, collection: any[], start: number, end: number): boolean {
        const state = this.state[id]

        if (!state) {
            return false
        }

        if (!(state.size === collection.length && state.start === start && state.end === end)) {
            return false
        }

        return state.slice.every((element, index) => element === collection[start + index])
    }
}
