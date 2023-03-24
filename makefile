ApiProject=--project ./backend/src/App.Api/App.Api.csproj
InfraProject=--startup-project ./backend/src/Infra/Infra.csproj

app-run:
	dotnet run ${ApiProject} & cd frontend && ng serve -o
app-build:
	dotnet build backend/FwksService.sln
app-compose:
	docker compose up -d --build

db-create:
	dotnet ef ${InfraProject} migrations add DropkickDb -o Postgres/Migrations/History
db-drop:
	dotnet ef ${InfraProject} database drop
db-update:
	dotnet ef ${InfraProject} database update -- --environment Development
db-script:
	dotnet ef ${InfraProject} migrations script -- --environment Development

libs-pack:
	rm artifacts -rdf
	dotnet restore
	dotnet build -c Release

	for project in libs/**/*.csproj; do\
		dotnet pack $${project} --no-restore --no-build -c Release -o artifacts/ --version-suffix nuget-test1 -p:Version=0.0.$$(date +"%y%j") ;\
	done

ef-update:
	dotnet tool update --global dotnet-ef