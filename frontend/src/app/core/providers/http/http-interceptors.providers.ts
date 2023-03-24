import { HTTP_INTERCEPTORS } from '@angular/common/http'
import { ProgressBarInterceptor } from '@app/core/interceptors'
import { AppContextService } from '@app/core/services'

const _httpInterceptorProviders = [
    {
        provide: HTTP_INTERCEPTORS,
        useClass: ProgressBarInterceptor,
        multi: true,
        deps: [AppContextService],
    },
]

export default _httpInterceptorProviders
