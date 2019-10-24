#!/usr/bin/bash

set -o xtrace

docker stop hophubapi
docker stop pgadmin
docker stop postgres

docker rm hophubapi
docker rm pgadmin
docker rm postgres

docker volume prune -f