FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY ["BooksStorage.Mail.csproj", "."]
RUN dotnet restore

COPY . .
RUN dotnet publish "BooksStorage.Mail.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "BooksStorage.Mail.dll"]
