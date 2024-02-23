# Use the .NET 6 SDK image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy the solution file and restore dependencies
COPY *.sln .
COPY Healthcare.Api/*.csproj ./Healthcare.Api/
COPY Healthcare.Api.Core/*.csproj ./Healthcare.Api.Core/
COPY Healthcare.Api.Service/*.csproj ./Healthcare.Api.Service/
COPY Healthcare.Api.Repository/*.csproj ./Healthcare.Api.Repository/

# Restore NuGet packages
RUN dotnet restore

# Copy the remaining source code
COPY . .

# Build the application
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Healthcare.Api.dll"]