#!/bin/bash
set -e

PROJECT_REPONAME="messaging"
BUILD_NUMBER="1"

docker build \
    --build-arg container="$PROJECT_REPONAME" \
    --build-arg container="$BUILD_NUMBER" \
    --rm=false --file "./ci/app.dockerfile" -t yusufcemalcelebi/messaging .


echo "Build finished"