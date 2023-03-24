import { Injectable } from '@angular/core'
import { HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http'
import { Observable } from 'rxjs/internal/Observable'
import { tap } from 'rxjs/operators'
import { AppContextService } from '@app/core/services'

@Injectable({
    providedIn: 'root',
})
export class ProgressBarInterceptor implements HttpInterceptor {
    constructor(private ctx: AppContextService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<any> {
        this.ctx.isLoadingResources = true
        return next.handle(request).pipe(
            tap({
                error: () => this.finalize(),
                complete: () => this.finalize(),
            })
        )
    }

    private finalize(): void {
        this.ctx.isLoadingResources = false
    }
}
