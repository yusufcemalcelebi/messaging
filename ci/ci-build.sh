#!/bin/bash
set -e

docker build \
    --build-arg container="$PROJECT_REPONAME" \
    --build-arg container="$BUILD_NUMBER" \
    --rm=false --file "./ci/app.dockerfile" -t yusufcemalcelebi/messaging .


echo "Build finished"