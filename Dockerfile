FROM mcr.microsoft.com/dotnet/sdk:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release

# Install Node.js and npm
RUN apt-get update && apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs && \
    npm install -g npm@latest

WORKDIR /src
COPY ["Book.Server/Book.Server.csproj","Book.Server/"]
COPY ["Core/Core.csproj","Core/"]
COPY ["book.client/book.client.esproj","book.client/"]
COPY ["book.client/package.json", "book.client/"]
COPY ["book.client/package-lock.json", "book.client/"]
WORKDIR "/src/book.client"
RUN npm ci  # Install JS dependencies
WORKDIR "/src"
RUN dotnet restore "./Book.Server/Book.Server.csproj"
COPY . .
WORKDIR "/src/Book.Server"
RUN dotnet build "./Book.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN ls -la /src/Book.Server/ || dir  # 'ls' for Linux, 'dir' for Windows (just in case)
RUN dotnet publish "./Book.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base As final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Book.Server.dll"]