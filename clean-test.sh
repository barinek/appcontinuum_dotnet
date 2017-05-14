#!/bin/sh

set -e

find . -type d -name 'bin' -exec rm -r {} +
find . -type d -name 'obj' -exec rm -r {} +

dotnet restore
dotnet build

sh ./test.sh