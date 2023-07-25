FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Books Storage/Books Storage.csproj", "Books Storage/"]
RUN dotnet restore "Books Storage/Books Storage.csproj"
COPY . .
WORKDIR "/src/Books Storage"
RUN dotnet build "Books Storage.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Books Storage.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Books Storage.dll"]
