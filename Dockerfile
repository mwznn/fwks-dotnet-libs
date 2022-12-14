#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ./src ./src
COPY ./libs ./libs
COPY ./*.sln .

RUN dotnet build "./src/App.Api/App.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./src/App.Api/App.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fwks.FwksService.App.Api.dll"]


# FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# WORKDIR /src

# COPY ./src ./src
# COPY ./libs ./libs
# COPY ./*.sln .

# # ARG AppApi=""

# # RUN dotnet restore --use-current-runtime && \
# #     dotnet publish src/App.Api/App.Api.csproj -c Release -o /release --no-restore --use-current-runtime

# # RUN dotnet restore ${AppApi} --use-current-runtime

# RUN dotnet publish ./src/App.Api/App.Api.csproj -c Release -o ./output

# FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS app
# WORKDIR /app
# COPY --from=build /output .

# ENV ASPNETCORE_URLS "http://*:80"
# EXPOSE 80
# ENTRYPOINT [ "dotnet", "Fwks.FwksService.App.Api.dll" ]