#!/usr/bin/bash

set -o xtrace

docker stop hophubapi
docker stop hophubpgadmin
docker stop hophubpostgres

docker rm hophubapi
docker rm hophubpgadmin
docker rm hophubpostgres

docker volume prune -f