import { APP_INITIALIZER } from '@angular/core'
import { KeycloakService } from 'keycloak-angular'
import { ContextService } from '@app/services'
import { keycloakInitializer } from './keycloak-initializer.function'
import { keycloakEventsSubscriber } from './keycloak-events-subscriber.function'

const _keycloakProviders = [
    {
        provide: APP_INITIALIZER,
        useFactory: keycloakInitializer,
        multi: true,
        deps: [KeycloakService]
    },
    {
        provide: APP_INITIALIZER,
        useFactory: keycloakEventsSubscriber,
        multi: true,
        deps: [KeycloakService, ContextService]
    }
]

export default _keycloakProviders