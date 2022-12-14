export const environment = {
    production: true,
    name: 'qa',
    endpoints: {
        api: 'http://host.docker.internal:25001'
    },
    authServer: {
        url: 'http://host.docker.internal:9999/auth',
        realm: 'development',
        clientId: 'dev-ui',
        scopes: 'openid profile email roles'
    }
}