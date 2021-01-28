#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
#https://github.com/NuGet/Announcements/issues/49
FROM mcr.microsoft.com/dotnet/sdk:5.0.102-ca-patch-buster-slim AS build-env
WORKDIR /source

COPY *.csproj .
COPY NuGet.Config .
RUN dotnet restore --configfile "./NuGet.Config"

COPY . .
RUN dotnet publish -c release -o /dist --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /dist .

ENTRYPOINT ["dotnet", "SImpl.Templates.WebApi.dll"]