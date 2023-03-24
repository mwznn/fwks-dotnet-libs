import { APP_INITIALIZER } from '@angular/core'
import { KeycloakService } from 'keycloak-angular'
import { AppContextService } from '@app/core/services'
import { keycloakInitializer } from './keycloak-initializer.function'
import { keycloakEventsSubscriber } from './keycloak-events-subscriber.function'

const _keycloakProviders = [
    {
        provide: APP_INITIALIZER,
        useFactory: keycloakInitializer,
        multi: true,
        deps: [KeycloakService],
    },
    {
        provide: APP_INITIALIZER,
        useFactory: keycloakEventsSubscriber,
        multi: true,
        deps: [KeycloakService, AppContextService],
    },
]

export default _keycloakProviders
