import { HTTP_INTERCEPTORS } from '@angular/common/http'
import { ProgressBarInterceptor } from '@app/interceptors'
import { ContextService } from '@app/services'

const _httpInterceptorProviders = [
    {
        provide: HTTP_INTERCEPTORS,
        useClass: ProgressBarInterceptor,
        multi: true,
        deps: [ContextService]
    }
]

export default _httpInterceptorProviders