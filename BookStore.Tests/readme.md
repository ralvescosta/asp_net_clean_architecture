Install CLI 

```
dotnet tool install -g dotnet-reportgenerator-globaltool
```

Run Test with coverage

```
dotnet test --collect:"XPlat Code Coverage"
```

Run Report

```
reportgenerator -reports:BookStore.Tests/TestResults/016365ac-b427-4f8c-8d25-8ea502e3a436/coverage.cobertura.xml -targetdir:CoverageReport -reporttypes:Html
```
