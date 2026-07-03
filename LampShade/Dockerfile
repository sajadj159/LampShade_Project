# Stage 1: Restore dependencies
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS restore
WORKDIR /src

COPY LampShade.sln ./
COPY 0_Framework/0_Framework/0_Framework.csproj 0_Framework/0_Framework/
COPY 01_LampShadeQuery/01_LampShadeQuery.csproj 01_LampShadeQuery/
COPY ServiceHost/ServiceHost.csproj ServiceHost/
COPY LampShade.Api/LampShade.Api.csproj LampShade.Api/
COPY AccountManagement/AM.Domain/AccountManagement.Domain/AccountManagement.Domain.csproj AccountManagement/AM.Domain/AccountManagement.Domain/
COPY AccountManagement/AM.Application/AccountManagement.Application/AccountManagement.Application.csproj AccountManagement/AM.Application/AccountManagement.Application/
COPY AccountManagement/AM.Application/AccountManagement.Application.Contracts/AccountManagement.Application.Contracts.csproj AccountManagement/AM.Application/AccountManagement.Application.Contracts/
COPY AccountManagement/AM.Infrustructure/AccountManagement.Infrastructure.EFCore/AccountManagement.Infrastructure.EFCore.csproj AccountManagement/AM.Infrustructure/AccountManagement.Infrastructure.EFCore/
COPY AccountManagement/AM.Infrustructure/AccountManagement.Configuration/AccountManagement.Configuration.csproj AccountManagement/AM.Infrustructure/AccountManagement.Configuration/
COPY BlogManagement/BM.Domain/BlogManagement.Domain/BlogManagement.Domain.csproj BlogManagement/BM.Domain/BlogManagement.Domain/
COPY BlogManagement/BM.Application/BlogManagement.Application/BlogManagement.Application.csproj BlogManagement/BM.Application/BlogManagement.Application/
COPY BlogManagement/BM.Application/BlogManagement.Application.Contract/BlogManagement.Application.Contract.csproj BlogManagement/BM.Application/BlogManagement.Application.Contract/
COPY BlogManagement/BM.Infrastructure/BlogManagement.Infrastructure.EFCore/BlogManagement.Infrastructure.EFCore.csproj BlogManagement/BM.Infrastructure/BlogManagement.Infrastructure.EFCore/
COPY BlogManagement/BM.Infrastructure/BlogManagement.Infrastructure.Configuration/BlogManagement.Infrastructure.Configuration.csproj BlogManagement/BM.Infrastructure/BlogManagement.Infrastructure.Configuration/
COPY CommentManagement/CM.Domain/CommentManagement.Domain/CommentManagement.Domain.csproj CommentManagement/CM.Domain/CommentManagement.Domain/
COPY CommentManagement/CM.Applicaion/CommentManagement.Application/CommentManagement.Application.csproj CommentManagement/CM.Applicaion/CommentManagement.Application/
COPY CommentManagement/CM.Applicaion/CommentManagement.Application.Contract/CommentManagement.Application.Contract.csproj CommentManagement/CM.Applicaion/CommentManagement.Application.Contract/
COPY CommentManagement/CM.Infrastructure/CommentManagement.Infrastructure.EFCore/CommentManagement.Infrastructure.EFCore.csproj CommentManagement/CM.Infrastructure/CommentManagement.Infrastructure.EFCore/
COPY CommentManagement/CM.Infrastructure/CommentManagement.Configuration/CommentManagement.Configuration.csproj CommentManagement/CM.Infrastructure/CommentManagement.Configuration/
COPY DiscountManagement/DM.Domain/DiscountManagement.Domain/DiscountManagement.Domain.csproj DiscountManagement/DM.Domain/DiscountManagement.Domain/
COPY DiscountManagement/DM.Application/DiscountManagement.Application/DiscountManagement.Application.csproj DiscountManagement/DM.Application/DiscountManagement.Application/
COPY DiscountManagement/DM.Application/DIscountManagement.Application.Contract/DiscountManagement.Application.Contract.csproj DiscountManagement/DM.Application/DIscountManagement.Application.Contract/
COPY DiscountManagement/DM.Infrastructure/DiscountManagement.Infrastructure.EFCore/DiscountManagement.Infrastructure.EFCore.csproj DiscountManagement/DM.Infrastructure/DiscountManagement.Infrastructure.EFCore/
COPY DiscountManagement/DM.Infrastructure/DiscountManagement.Configuration/DiscountManagement.Configuration.csproj DiscountManagement/DM.Infrastructure/DiscountManagement.Configuration/
COPY InventoryManagement/IM.Domain/InventoryManagement.Domain/InventoryManagement.Domain.csproj InventoryManagement/IM.Domain/InventoryManagement.Domain/
COPY InventoryManagement/IM.Application/InventoryManagement.Application/InventoryManagement.Application.csproj InventoryManagement/IM.Application/InventoryManagement.Application/
COPY InventoryManagement/IM.Application/InventoryManagement.Application.Contract/InventoryManagement.Application.Contract.csproj InventoryManagement/IM.Application/InventoryManagement.Application.Contract/
COPY InventoryManagement/IM.Infrastructure/InventoryManagement.Infrastructure.EFCore/InventoryManagement.Infrastructure.EFCore.csproj InventoryManagement/IM.Infrastructure/InventoryManagement.Infrastructure.EFCore/
COPY InventoryManagement/IM.Infrastructure/InventoryManagement.Configuration/InventoryManagement.Configuration.csproj InventoryManagement/IM.Infrastructure/InventoryManagement.Configuration/
COPY Shopmanagement/SM.Domain/ShopManagement.Domain/ShopManagement.Domain.csproj Shopmanagement/SM.Domain/ShopManagement.Domain/
COPY Shopmanagement/SM.Application/ShopManagement.Application/ShopManagement.Application.csproj Shopmanagement/SM.Application/ShopManagement.Application/
COPY Shopmanagement/SM.Application/ShopManagement.Application.Contract/ShopManagement.Application.Contract.csproj Shopmanagement/SM.Application/ShopManagement.Application.Contract/
COPY Shopmanagement/SM.Infrastructure/ShopManagement.Infrastructure.EFCore/ShopManagement.Infrastructure.EFCore.csproj Shopmanagement/SM.Infrastructure/ShopManagement.Infrastructure.EFCore/
COPY Shopmanagement/SM.Infrastructure/ShopManagement.Infrastructure.AccountAcl/ShopManagement.Infrastructure.AccountAcl.csproj Shopmanagement/SM.Infrastructure/ShopManagement.Infrastructure.AccountAcl/
COPY Shopmanagement/SM.Infrastructure/ShopManagement.Infrastructure.InventoryAcl/ShopManagement.Infrastructure.InventoryAcl.csproj Shopmanagement/SM.Infrastructure/ShopManagement.Infrastructure.InventoryAcl/
COPY Shopmanagement/SM.Infrastructure/ShopManagement.Configuration/ShopManagement.Configuration.csproj Shopmanagement/SM.Infrastructure/ShopManagement.Configuration/
COPY Endpoint/OnlineShop/OnlineShop.csproj Endpoint/OnlineShop/
COPY docker-compose.dcproj ./

RUN dotnet restore LampShade.sln

# Stage 2: Build and publish
FROM restore AS build
COPY . .
RUN dotnet build LampShade.sln -c Release --no-restore
RUN dotnet publish ServiceHost/ServiceHost.csproj -c Release -o /app/publish --no-restore

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

ENTRYPOINT ["dotnet", "ServiceHost.dll"]
