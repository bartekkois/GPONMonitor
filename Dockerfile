FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY src/GPONMonitor/*.csproj ./src/GPONMonitor/
RUN dotnet restore src/GPONMonitor//GPONMonitor.csproj
COPY . .
RUN dotnet build "src/GPONMonitor/GPONMonitor.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "src/GPONMonitor/GPONMonitor.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GPONMonitor.dll"]
