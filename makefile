DockerImage=fwks-service
ApiProject=--project ./src/App.Api/App.Api.csproj
DataProject=--startup-project ./src/Infra/Infra.csproj

run:
	dotnet restore
	dotnet build
	dotnet run ${ApiProject} --no-build

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

docker-build:
	docker build . --progress plain -t fwksapi:latest
docker-run:
	docker run -it -d --name fwksapi -p 5001:80 fwksapi
docker-rm:
	#docker compose -f ./sandbox/docker-compose.yml up -d --build	
	docker rm fwksapi

pack:
	dotnet pack ./libs/AspNetCore/AspNetCore.csproj -o ./artifacts -c Release /p:Version=0.0.1-beta
	dotnet pack ./libs/Core/Core.csproj -o ./artifacts -c Release /p:Version=0.0.1-beta
	dotnet pack ./libs/MongoDb/MongoDb.csproj -o ./artifacts -c Release /p:Version=0.0.1-beta
	dotnet pack ./libs/Postgres/Postgres.csproj -o ./artifacts -c Release /p:Version=0.0.1-beta
	dotnet pack ./libs/Redis/Redis.csproj -o ./artifacts -c Release /p:Version=0.0.1-beta
	dotnet pack ./libs/Security/Security.csproj -o ./artifacts -c Release /p:Version=0.0.1-beta


# dotnet pack .\src\example\example.csproj -o c:\published\example -c Release /p:Version=1.2.3