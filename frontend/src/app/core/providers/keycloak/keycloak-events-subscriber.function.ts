import { AppContextService } from '@app/core/services'
import { KeycloakEventType, KeycloakService } from 'keycloak-angular'

export function keycloakEventsSubscriber(
    kc: KeycloakService,
    ctx: AppContextService
) {
    return () => {
        kc.keycloakEvents$.subscribe({
            next: (event) => {
                switch (event.type) {
                    case KeycloakEventType.OnAuthSuccess:
                        kc.loadUserProfile().then((res) => ctx.loadProfile(res))
                        break
                    case KeycloakEventType.OnAuthError:
                    case KeycloakEventType.OnAuthLogout:
                    case KeycloakEventType.OnAuthRefreshError:
                        ctx.clearProfile()
                        break
                    case KeycloakEventType.OnTokenExpired:
                        kc.updateToken()
                        break
                }
            },
            error: (err) => {
                console.error(err)
                ctx.clearProfile()
            },
        })
    }
}
