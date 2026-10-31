# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LampShade.Api/LampShade.Api.csproj", "LampShade.Api/"]
COPY ["AccountManagement/AM.Infrustructure/AccountManagement.Configuration/AccountManagement.Configuration.csproj", "AccountManagement/AM.Infrustructure/AccountManagement.Configuration/"]
COPY ["01_LampShadeQuery/01_LampShadeQuery.csproj", "01_LampShadeQuery/"]
COPY ["BlogManagement/BM.Infrastructure/BlogManagement.Infrastructure.EFCore/BlogManagement.Infrastructure.EFCore.csproj", "BlogManagement/BM.Infrastructure/BlogManagement.Infrastructure.EFCore/"]
COPY ["BlogManagement/BM.Domain/BlogManagement.Domain/BlogManagement.Domain.csproj", "BlogManagement/BM.Domain/BlogManagement.Domain/"]
COPY ["0_Framework/0_Framework/0_Framework.csproj", "0_Framework/0_Framework/"]
COPY ["BlogManagement/BM.Application/BlogManagement.Application.Contract/BlogManagement.Application.Contract.csproj", "BlogManagement/BM.Application/BlogManagement.Application.Contract/"]
COPY ["CommentManagement/CM.Infrastructure/CommentManagement.Infrastructure.EFCore/CommentManagement.Infrastructure.EFCore.csproj", "CommentManagement/CM.Infrastructure/CommentManagement.Infrastructure.EFCore/"]
COPY ["CommentManagement/CM.Domain/CommentManagement.Domain/CommentManagement.Domain.csproj", "CommentManagement/CM.Domain/CommentManagement.Domain/"]
COPY ["CommentManagement/CM.Applicaion/CommentManagement.Application.Contract/CommentManagement.Application.Contract.csproj", "CommentManagement/CM.Applicaion/CommentManagement.Application.Contract/"]
COPY ["DiscountManagement/DM.Infrastructure/DiscountManagement.Infrastructure.EFCore/DiscountManagement.Infrastructure.EFCore.csproj", "DiscountManagement/DM.Infrastructure/DiscountManagement.Infrastructure.EFCore/"]
COPY ["Shopmanagement/SM.Infrastructure/ShopManagement.Infrastructure.EFCore/ShopManagement.Infrastructure.EFCore.csproj", "Shopmanagement/SM.Infrastructure/ShopManagement.Infrastructure.EFCore/"]
COPY ["AccountManagement/AM.Infrustructure/AccountManagement.Infrastructure.EFCore/AccountManagement.Infrastructure.EFCore.csproj", "AccountManagement/AM.Infrustructure/AccountManagement.Infrastructure.EFCore/"]
COPY ["AccountManagement/AM.Domain/AccountManagement.Domain/AccountManagement.Domain.csproj", "AccountManagement/AM.Domain/AccountManagement.Domain/"]
COPY ["AccountManagement/AM.Application/AccountManagement.Application.Contracts/AccountManagement.Application.Contracts.csproj", "AccountManagement/AM.Application/AccountManagement.Application.Contracts/"]
COPY ["Shopmanagement/SM.Domain/ShopManagement.Domain/ShopManagement.Domain.csproj", "Shopmanagement/SM.Domain/ShopManagement.Domain/"]
COPY ["Shopmanagement/SM.Application/ShopManagement.Application.Contract/ShopManagement.Application.Contract.csproj", "Shopmanagement/SM.Application/ShopManagement.Application.Contract/"]
COPY ["DiscountManagement/DM.Domain/DiscountManagement.Domain/DiscountManagement.Domain.csproj", "DiscountManagement/DM.Domain/DiscountManagement.Domain/"]
COPY ["DiscountManagement/DM.Application/DIscountManagement.Application.Contract/DiscountManagement.Application.Contract.csproj", "DiscountManagement/DM.Application/DIscountManagement.Application.Contract/"]
COPY ["InventoryManagement/IM.Infrastructure/InventoryManagement.Infrastructure.EFCore/InventoryManagement.Infrastructure.EFCore.csproj", "InventoryManagement/IM.Infrastructure/InventoryManagement.Infrastructure.EFCore/"]
COPY ["InventoryManagement/IM.Domain/InventoryManagement.Domain/InventoryManagement.Domain.csproj", "InventoryManagement/IM.Domain/InventoryManagement.Domain/"]
COPY ["InventoryManagement/IM.Application/InventoryManagement.Application.Contract/InventoryManagement.Application.Contract.csproj", "InventoryManagement/IM.Application/InventoryManagement.Application.Contract/"]
COPY ["AccountManagement/AM.Application/AccountManagement.Application/AccountManagement.Application.csproj", "AccountManagement/AM.Application/AccountManagement.Application/"]
COPY ["BlogManagement/BM.Infrastructure/BlogManagement.Infrastructure.Configuration/BlogManagement.Infrastructure.Configuration.csproj", "BlogManagement/BM.Infrastructure/BlogManagement.Infrastructure.Configuration/"]
COPY ["BlogManagement/BM.Application/BlogManagement.Application/BlogManagement.Application.csproj", "BlogManagement/BM.Application/BlogManagement.Application/"]
COPY ["CommentManagement/CM.Infrastructure/CommentManagement.Configuration/CommentManagement.Configuration.csproj", "CommentManagement/CM.Infrastructure/CommentManagement.Configuration/"]
COPY ["CommentManagement/CM.Applicaion/CommentManagement.Application/CommentManagement.Application.csproj", "CommentManagement/CM.Applicaion/CommentManagement.Application/"]
COPY ["DiscountManagement/DM.Infrastructure/DiscountManagement.Configuration/DiscountManagement.Configuration.csproj", "DiscountManagement/DM.Infrastructure/DiscountManagement.Configuration/"]
COPY ["DiscountManagement/DM.Application/DiscountManagement.Application/DiscountManagement.Application.csproj", "DiscountManagement/DM.Application/DiscountManagement.Application/"]
COPY ["InventoryManagement/IM.Infrastructure/InventoryManagement.Configuration/InventoryManagement.Configuration.csproj", "InventoryManagement/IM.Infrastructure/InventoryManagement.Configuration/"]
COPY ["InventoryManagement/IM.Application/InventoryManagement.Application/InventoryManagement.Application.csproj", "InventoryManagement/IM.Application/InventoryManagement.Application/"]
COPY ["Shopmanagement/SM.Infrastructure/ShopManagement.Configuration/ShopManagement.Configuration.csproj", "Shopmanagement/SM.Infrastructure/ShopManagement.Configuration/"]
COPY ["Shopmanagement/SM.Application/ShopManagement.Application/ShopManagement.Application.csproj", "Shopmanagement/SM.Application/ShopManagement.Application/"]
COPY ["Shopmanagement/SM.Infrastructure/ShopManagement.Infrastructure.AccountAcl/ShopManagement.Infrastructure.AccountAcl.csproj", "Shopmanagement/SM.Infrastructure/ShopManagement.Infrastructure.AccountAcl/"]
COPY ["Shopmanagement/SM.Infrastructure/ShopManagement.Infrastructure.InventoryAcl/ShopManagement.Infrastructure.InventoryAcl.csproj", "Shopmanagement/SM.Infrastructure/ShopManagement.Infrastructure.InventoryAcl/"]
RUN dotnet restore "./LampShade.Api/LampShade.Api.csproj"
COPY . .
WORKDIR "/src/LampShade.Api"
RUN dotnet build "./LampShade.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LampShade.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LampShade.Api.dll"]