#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/sdk:7.0-bullseye-slim AS build
WORKDIR /src
COPY ["DocuSign.MyWebForms/*.sln", "DocuSign.MyWebForms/"]
COPY ["DocuSign.MyWebForms/DocuSign.MyWebForms/DocuSign.MyWebForms.csproj", "DocuSign.MyWebForms/DocuSign.MyWebForms/"]
WORKDIR /src/DocuSign.MyWebForms
RUN dotnet restore
COPY . .
RUN dotnet build "DocuSign.MyWebForms/DocuSign.MyWebForms/DocuSign.MyWebForms.csproj" -c Release -o /app/build

FROM build AS publish
RUN apt-get update \
    && apt-get install -y ca-certificates curl gnupg \
    && mkdir -p /etc/apt/keyrings \
    && curl -fsSL https://deb.nodesource.com/gpgkey/nodesource-repo.gpg.key | gpg --dearmor -o /etc/apt/keyrings/nodesource.gpg \
    && echo "deb [signed-by=/etc/apt/keyrings/nodesource.gpg] https://deb.nodesource.com/node_20.x nodistro main" | tee /etc/apt/sources.list.d/nodesource.list \
    && apt-get update && apt-get -y install nodejs \
    && npm install -g @angular/cli \
    && npm update \
    && npm config delete proxy
RUN dotnet publish "DocuSign.MyWebForms/DocuSign.MyWebForms/DocuSign.MyWebForms.csproj" -c Release -o /app/publish

FROM node:20.11.0-bullseye-slim AS node-builder
WORKDIR /node
COPY ["DocuSign.MyWebForms/DocuSign.MyWebForms/ClientApp", "/node"]
RUN npm install && npm run build

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=node-builder /node/build ./ClientApp/dist
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "DocuSign.MyWebForms.dll"]
