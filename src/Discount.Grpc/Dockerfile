# Use the ASP.NET runtime image as base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution file and restore dependencies
COPY ["Discount.Grpc/Discount.Grpc.csproj", "Discount.Grpc/"]
RUN dotnet restore "Discount.Grpc/Discount.Grpc.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/Discount.Grpc"
RUN dotnet build "Discount.Grpc.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
WORKDIR "/src/Discount.Grpc"
RUN dotnet publish "Discount.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.Grpc.dll"]
