# turtles
Dotnet experiment. Analalyze data from Yahoo Finance

## Turtles-search

Find a symbol in Google Finance API (v7)

```bash
cd turtles-search
dotnet restore
dotnet build
dotnet run -- MSFT
```

## Turtles-buy

Generate a csv file from a list of stocks with some generated data. The csv should be imported in Google Sheets or Excel.

```bash
cd turtles-buy
dotnet restore
dotnet build
dotnet run -- dotnet run -- ../watchlists/moderngraham-watchlist.txt > ~/Desktop/modern-graham.csv
```

## Turtles-min

Generate a csv file from a list of stocks with the minimum from the last 4 weeks.

```bash
cd turtles-min
dotnet restore
dotnet build
dotnet run -- MSFT
```

## Dotnet cheatsheet

### Create library

```bash
dotnet new classlib -o libname
```

### Create unit tests for previous library

```bash
dotnet new xunit -o libname.unittests
dotnet add libname.unitests/libname.unittests.csproj reference libname/libname.csproj
```

### Add nuget package

```bash
dotnet add libname.csproj package restsharp.netcore
```

### Remove nuget package

```bash
dotnet remove libname.csproj package restsharp.netcore
```

### Add project to solution

```bash
dotnet sln solution.sln add libname/libname.csproj
```

### Restore a project

```bash
dotnet Restore
```

### Compile a project

```bash
dotnet build
```
