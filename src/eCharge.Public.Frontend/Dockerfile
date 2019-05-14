ARG NODE_IMAGE=node:latest

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk as dotnet-build
WORKDIR /src

FROM ${NODE_IMAGE} as node-build
WORKDIR /web
COPY . .
WORKDIR /web/src/ECharge.Public.Frontend
RUN apt-get update
RUN apt-get install nasm
RUN npm install
RUN npm run build

FROM dotnet-build as build
WORKDIR /src
COPY . .
WORKDIR /src/src/ECharge.Public.Frontend/wwwroot
COPY --from=node-build /web/src/ECharge.Public.Frontend/wwwroot .
WORKDIR /src/src/ECharge.Public.Frontend
RUN dotnet restore -nowarn:msb3202,nu1503
RUN dotnet build --no-restore -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ECharge.Public.Frontend.dll"]