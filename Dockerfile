FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-backend
WORKDIR /app

COPY . ./

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM node:16-alpine AS build-overlay
WORKDIR /app
COPY ./Overlay/package.json ./
COPY ./Overlay/yarn.lock ./
RUN yarn
COPY ./Overlay ./
RUN yarn build

FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Install libpcap with remote control support
RUN apt-get update \
    && apt-get install flex bison wget build-essential -y --no-install-recommends \
    && apt-get clean

RUN wget https://www.tcpdump.org/release/libpcap-1.10.1.tar.gz && tar xzf libpcap-1.10.1.tar.gz \
    && cd libpcap-1.10.1 \
    && ./configure --enable-remote && make install

WORKDIR /app
COPY --from=build-backend /app/out .
RUN mkdir -p /app/frontend
COPY --from=build-overlay /app/dist /app/frontend
COPY ./bin/* ./
ENTRYPOINT ["dotnet", "LostArkLogger.dll"]