# Stage 1: Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

COPY HelloGithubOAuth.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Production Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

COPY --from=build-env /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "HelloGitHubOAuth.dll"]
