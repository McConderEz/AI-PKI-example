FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CA/CertificationAuthority.Web/CertificationAuthority.Web.csproj", "src/CA/CertificationAuthority.Web/"]
COPY ["src/Common/Pki.Messaging.Contracts/Pki.Messaging.Contracts.csproj", "src/Common/Pki.Messaging.Contracts/"]
RUN dotnet restore "src/CA/CertificationAuthority.Web/CertificationAuthority.Web.csproj"
COPY . .
WORKDIR "/src/src/CA/CertificationAuthority.Web"
RUN dotnet build "./CertificationAuthority.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CertificationAuthority.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CertificationAuthority.Web.dll"]
