FROM mcr.microsoft.com/dotnet/sdk:7.0 as base
RUN apt-get update

FROM base as server


FROM base as cli-client

FROM base as web-client