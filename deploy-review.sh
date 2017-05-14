#!/bin/sh

set -e

sh ./clean-test.sh

dotnet publish --configuration Release

cf push -f deploy-manifest-review.yml