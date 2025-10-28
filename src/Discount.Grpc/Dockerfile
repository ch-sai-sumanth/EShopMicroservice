# Stage 1: Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 2: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Discount.Grpc/Discount.Grpc.csproj", "Discount.Grpc/"]
RUN dotnet restore "Discount.Grpc/Discount.Grpc.csproj"
COPY . .
WORKDIR "/src/Discount.Grpc"
RUN dotnet build "Discount.Grpc.csproj" -c Release -o /app/build

# Stage 3: Publish the app
FROM build AS publish
RUN dotnet publish "Discount.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final runtime container
FROM base AS final
WORKDIR /app

# ✅ Ensure we’re copying from the correct stage and path
COPY --from=publish /app/publish .

# ✅ Ensure /app is writable and owned by non-root user
RUN chmod -R 777 /app

# ✅ Fix SQLite permission issue at container startup
ENTRYPOINT ["sh", "-c", "chown -R $APP_UID /app && chmod -R 777 /app && dotnet Discount.Grpc.dll"]
