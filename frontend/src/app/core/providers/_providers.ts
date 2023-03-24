import _httpInterceptorProviders from './http/http-interceptors.providers'
import _keycloakProviders from './keycloak/keycloak.providers'

const _providers = [
    ..._httpInterceptorProviders,
    // ..._keycloakProviders
]

export default _providers
