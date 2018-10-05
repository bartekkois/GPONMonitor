FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src/GPONMonitor
COPY GPONMonitor.csproj .
RUN dotnet restore "GPONMonitor.csproj"

FROM build AS publish
COPY . .
RUN dotnet publish "GPONMonitor.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GPONMonitor.dll"]
