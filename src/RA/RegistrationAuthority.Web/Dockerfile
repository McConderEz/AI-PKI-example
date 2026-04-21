FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/RA/RegistrationAuthority.Web/RegistrationAuthority.Web.csproj", "src/RA/RegistrationAuthority.Web/"]
COPY ["src/Common/Pki.Messaging.Contracts/Pki.Messaging.Contracts.csproj", "src/Common/Pki.Messaging.Contracts/"]
RUN dotnet restore "src/RA/RegistrationAuthority.Web/RegistrationAuthority.Web.csproj"
COPY . .
WORKDIR "/src/src/RA/RegistrationAuthority.Web"
RUN dotnet build "./RegistrationAuthority.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RegistrationAuthority.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RegistrationAuthority.Web.dll"]
