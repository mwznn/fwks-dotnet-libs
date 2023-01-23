export const environment = {
    production: true,
    name: 'docker',
    endpoints: {
        api: 'http://host.docker.internal:25001'
    },
    authServer: {
        url: 'http://host.docker.internal:9999/auth',
        realm: 'development',
        clientId: 'development',
        scopes: 'openid profile email roles'
    }
}