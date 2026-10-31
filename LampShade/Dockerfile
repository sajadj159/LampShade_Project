# Stage 1: Restore dependencies
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS restore
WORKDIR /src

# Shared build settings and central package versions, then only the project files the API needs,
# so the restore layer stays cached until a dependency changes.
COPY Directory.Build.props Directory.Packages.props ./
COPY src/BuildingBlocks/LampShade.BuildingBlocks.Domain/*.csproj src/BuildingBlocks/LampShade.BuildingBlocks.Domain/
COPY src/BuildingBlocks/LampShade.BuildingBlocks.Application/*.csproj src/BuildingBlocks/LampShade.BuildingBlocks.Application/
COPY src/BuildingBlocks/LampShade.BuildingBlocks.Infrastructure/*.csproj src/BuildingBlocks/LampShade.BuildingBlocks.Infrastructure/
COPY src/Host/LampShade.Api/*.csproj src/Host/LampShade.Api/
COPY src/Modules/Account/AccountManagement.Contracts/*.csproj src/Modules/Account/AccountManagement.Contracts/
COPY src/Modules/Account/AccountManagement.Domain/*.csproj src/Modules/Account/AccountManagement.Domain/
COPY src/Modules/Account/AccountManagement.Application/*.csproj src/Modules/Account/AccountManagement.Application/
COPY src/Modules/Account/AccountManagement.Infrastructure/*.csproj src/Modules/Account/AccountManagement.Infrastructure/
COPY src/Modules/Account/AccountManagement.Presentation/*.csproj src/Modules/Account/AccountManagement.Presentation/
COPY src/Modules/Blog/BlogManagement.Contracts/*.csproj src/Modules/Blog/BlogManagement.Contracts/
COPY src/Modules/Blog/BlogManagement.Domain/*.csproj src/Modules/Blog/BlogManagement.Domain/
COPY src/Modules/Blog/BlogManagement.Application/*.csproj src/Modules/Blog/BlogManagement.Application/
COPY src/Modules/Blog/BlogManagement.Infrastructure/*.csproj src/Modules/Blog/BlogManagement.Infrastructure/
COPY src/Modules/Blog/BlogManagement.Presentation/*.csproj src/Modules/Blog/BlogManagement.Presentation/
COPY src/Modules/Comment/CommentManagement.Contracts/*.csproj src/Modules/Comment/CommentManagement.Contracts/
COPY src/Modules/Comment/CommentManagement.Domain/*.csproj src/Modules/Comment/CommentManagement.Domain/
COPY src/Modules/Comment/CommentManagement.Application/*.csproj src/Modules/Comment/CommentManagement.Application/
COPY src/Modules/Comment/CommentManagement.Infrastructure/*.csproj src/Modules/Comment/CommentManagement.Infrastructure/
COPY src/Modules/Comment/CommentManagement.Presentation/*.csproj src/Modules/Comment/CommentManagement.Presentation/
COPY src/Modules/Discount/DiscountManagement.Contracts/*.csproj src/Modules/Discount/DiscountManagement.Contracts/
COPY src/Modules/Discount/DiscountManagement.Domain/*.csproj src/Modules/Discount/DiscountManagement.Domain/
COPY src/Modules/Discount/DiscountManagement.Application/*.csproj src/Modules/Discount/DiscountManagement.Application/
COPY src/Modules/Discount/DiscountManagement.Infrastructure/*.csproj src/Modules/Discount/DiscountManagement.Infrastructure/
COPY src/Modules/Discount/DiscountManagement.Presentation/*.csproj src/Modules/Discount/DiscountManagement.Presentation/
COPY src/Modules/Inventory/InventoryManagement.Contracts/*.csproj src/Modules/Inventory/InventoryManagement.Contracts/
COPY src/Modules/Inventory/InventoryManagement.Domain/*.csproj src/Modules/Inventory/InventoryManagement.Domain/
COPY src/Modules/Inventory/InventoryManagement.Application/*.csproj src/Modules/Inventory/InventoryManagement.Application/
COPY src/Modules/Inventory/InventoryManagement.Infrastructure/*.csproj src/Modules/Inventory/InventoryManagement.Infrastructure/
COPY src/Modules/Inventory/InventoryManagement.Presentation/*.csproj src/Modules/Inventory/InventoryManagement.Presentation/
COPY src/Modules/Shop/ShopManagement.Contracts/*.csproj src/Modules/Shop/ShopManagement.Contracts/
COPY src/Modules/Shop/ShopManagement.Domain/*.csproj src/Modules/Shop/ShopManagement.Domain/
COPY src/Modules/Shop/ShopManagement.Application/*.csproj src/Modules/Shop/ShopManagement.Application/
COPY src/Modules/Shop/ShopManagement.Infrastructure/*.csproj src/Modules/Shop/ShopManagement.Infrastructure/
COPY src/Modules/Shop/ShopManagement.Presentation/*.csproj src/Modules/Shop/ShopManagement.Presentation/

RUN dotnet restore src/Host/LampShade.Api/LampShade.Api.csproj

# Stage 2: Build and publish
FROM restore AS build
ARG BUILD_CONFIG=Release
COPY src/ src/
RUN dotnet publish src/Host/LampShade.Api/LampShade.Api.csproj -c ${BUILD_CONFIG} -o /app/publish --no-restore /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

RUN groupadd --system --gid 1001 appgroup && \
    useradd --system --uid 1001 --gid appgroup --shell /bin/false appuser

COPY --from=build /app/publish .

RUN chown -R appuser:appgroup /app
USER appuser

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_RUNNING_IN_CONTAINER=true

ENTRYPOINT ["dotnet", "LampShade.Api.dll"]
