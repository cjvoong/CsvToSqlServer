FROM mcr.microsoft.com/mssql/server

# Switch to root user for access to apt-get install
USER root

ENV MSSQL_SA_PASSWORD 3qoLxoCRw7
ENV SA_PASSWORD 3qoLxoCRw7
ENV ACCEPT_EULA Y

# Install node/npm
RUN apt-get -y update  && \
    apt-get install -y curl && \
    apt-get install -y dos2unix


# Create app directory
RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

COPY import-data.sh /usr/src/app
COPY schema.sql /usr/src/app
COPY entrypoint.sh /usr/src/app

RUN dos2unix *

# Switch back to mssql user and run the entrypoint script
USER mssql
ENTRYPOINT /bin/bash ./entrypoint.sh