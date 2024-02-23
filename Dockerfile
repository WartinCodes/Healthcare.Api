FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
# COPY DockerTest/*.csproj ./DockerTest/
COPY Healthcare.Api/*.csproj ./Healthcare.Api.Api/
COPY Healthcare.Api.Service/*.csproj ./Healthcare.Api.Service/
COPY Healthcare.Api.Core/*.csproj ./Healthcare.Api.Core/
COPY Healthcare.Api.Repository/*.csproj ./Healthcare.Api.Repository/

RUN dotnet restore *.sln

# copy everything else and build app
COPY Healthcare.Api/. ./Healthcare.Api.Api/
COPY Healthcare.Api.Service/. ./Healthcare.Api.Service/
COPY Healthcare.Api.Core/. ./Healthcare.Api.Core/
COPY Healthcare.Api.Repository/. ./Healthcare.Api.Repository/

WORKDIR /app/Healthcare.Api
RUN dotnet publish -c Release -o /release

FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY --from=build /release ./   
ENTRYPOINT ["dotnet", "Healthcare.Api.dll"]