# Fwks Service App

App used to kickstart new applications.

## Software

### Required

- [Visual Studio 2022 Latest](https://visualstudio.microsoft.com/)
- [.NET Core SDK 7](https://dotnet.microsoft.com/download/dotnet-core)
- [Chocolatey](https://chocolatey.org/)
  - `choco install make`
- [Node.js](https://nodejs.org/en/)
  - `npm i @angular/cli -g`

### Optional

- [Solution Error Visualizer](https://marketplace.visualstudio.com/items?itemName=VisualStudioPlatformTeam.SolutionErrorVisualizer2022)
- Choco Packages
  - `choco install act-cli` for running github actions locally
  - `choco install minikube` for running k8s locally
- NPM Packages
  - `npm i pnpm -g`
  - `npm i depcheck -g`

## Before running either locally or within docker

1. Create a user on [keycloak development realm](http://localhost:9999/auth/admin/master/console/#/development/users).
2. Install or update dotnet ef tools, run `make ef-update`
3. Execute `make db-update` to run migrations.

## Running Locally

To start the apps, run `make app-run`

## Running on Docker

To start the apps using docker, run `make app-compose`

## Environments endpoints

- Development [UI](http://localhost:4200) / [Api](https://localhost:5001/swagger)
- Docker [UI](http://localhost:25000) / [Api](https://localhost:25001/swagger)
- Live [UI](WIP) / [Api](WIP)

## Fwks Libraries Docs & Guides (WIP)

- [AspNetCore](./docs/libs/aspnetcore/README.md)
- [Core](./docs/libs/core/README.md)
- [MongoDb](./docs/libs/mongodb/README.md)
- [Postgres](./docs/libs/postgres/README.md)
- [Redis](./docs/libs/redis/README.md)
- [Security](./docs/libs/security/README.md)
