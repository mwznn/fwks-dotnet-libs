import { Injectable } from '@angular/core'
import { KeycloakService } from 'keycloak-angular'
import { KeycloakProfile } from 'keycloak-js'

@Injectable({
    providedIn: 'root',
})
export class AppContextService {
    isLoadingResources: boolean

    hasContext: boolean
    profile: KeycloakProfile | undefined

    constructor(private kc: KeycloakService) {
        this.hasContext = false
        this.isLoadingResources = false
    }

    loadProfile(profile: KeycloakProfile): void {
        this.profile = profile
        this.hasContext = true
    }

    clearProfile(): void {
        this.profile = undefined
        this.hasContext = false
    }

    toggle(): void {
        this.hasContext ? this.kc.logout() : this.kc.login()
    }
}
