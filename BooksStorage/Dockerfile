FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /app
COPY ["BooksStorage.csproj", "."]
RUN dotnet restore

COPY . .
RUN dotnet publish "BooksStorage.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "BooksStorage.dll"]