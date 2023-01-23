export const environment = {
  production: true,
  name: 'live',
  endpoints: {
    api: 'https://live-api-instance/api'
  },
  authServer: {
    url: 'http://live-keycloak-instance/auth',
    realm: 'dev-service',
    clientId: 'development',
    scopes: 'openid profile email roles'
  }
}