FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY ["BooksStorage.FrontEnd/BooksStorage.FrontEnd.csproj", "BooksStorage.FrontEnd/"]
RUN dotnet restore "BooksStorage.FrontEnd/BooksStorage.FrontEnd.csproj"
COPY . .
WORKDIR "/src/BooksStorage.FrontEnd"
RUN dotnet build "BooksStorage.FrontEnd.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BooksStorage.FrontEnd.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BooksStorage.FrontEnd.dll"]
