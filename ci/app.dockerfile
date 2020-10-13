FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

COPY /ci /ci
COPY /Messaging.sln .
COPY /test /test
COPY /src /src

RUN ["sh", "./ci/build.sh", "Release"]
RUN ["sh", "./ci/test.sh", "Release"]

WORKDIR /src/Messaging.Api

RUN ["dotnet", "publish", "--configuration", "Release", "--no-build", "--no-restore", "-v", "m", "-o", "../../artifacts"]

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as runtime-env

ARG container
ARG imagetag

ENV CONTAINER_NAME=${container:-"local"}
ENV IMAGE_TAG=${imagetag:-"local"}

WORKDIR /app

COPY --from=build-env /artifacts .

ADD /ci/docker-entrypoint.sh ./docker-entrypoint.sh
RUN chmod +x ./docker-entrypoint.sh

RUN groupadd -r messaging && useradd -r -s /bin/false -g messaging messaging
RUN chown -R messaging:messaging /app
USER messaging

ENV ASPNETCORE_URLS=http://*:5000
EXPOSE 5000

ENTRYPOINT ["bash", "docker-entrypoint.sh"]
