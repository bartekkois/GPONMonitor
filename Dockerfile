FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
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
