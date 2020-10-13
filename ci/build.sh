#!/bin/bash
set -e

configuration=${1:-Debug}

dotnet build ./Messaging.sln -c $configuration