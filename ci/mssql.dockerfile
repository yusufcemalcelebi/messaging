FROM mcr.microsoft.com/mssql/server:2017-latest

# Create script directory
RUN mkdir -p /usr/src/myscript

# Install script
COPY ./schema.sql /usr/src/myscript/
COPY ./mssql-entrypoint.sh /usr/src/myscript/

EXPOSE 1433

WORKDIR /usr/src/myscript
CMD exec /bin/bash mssql-entrypoint.sh
