ApiProject=--project ./src/App.Api/App.Api.csproj
DataProject=--startup-project ./src/Infra/Infra.csproj

run:
	dotnet restore
	dotnet build
	dotnet run ${ApiProject} --no-build &
	cd ui && ng serve -o

run-no-build:
	dotnet run ${ApiProject} --no-build

db-create:
	dotnet ef ${DataProject} migrations add DropkickDb -o Postgres/Migrations/History
db-drop:
	dotnet ef ${DataProject} database drop
db-update:
	dotnet ef ${DataProject} database update -- --environment Development
db-script:
	dotnet ef ${DataProject} migrations script -- --environment Development

ef-install:
	dotnet tool install --global dotnet-ef
ef-update:
	dotnet tool update --global dotnet-ef