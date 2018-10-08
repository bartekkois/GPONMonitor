FROM microsoft/dotnet:2.2-sdk AS testrunner
WORKDIR /src
COPY *.sln .
COPY src/GPONMonitor/*.csproj ./src/GPONMonitor/
COPY tests/*.csproj ./tests/
RUN dotnet restore ./GPONMonitor.sln
COPY . .
WORKDIR /src/tests
ENTRYPOINT ["dotnet", "test", "--logger:trx", "--results-directory:/var/temp"]
