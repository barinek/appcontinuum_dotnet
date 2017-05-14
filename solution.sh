#!/bin/sh

set -e

rm appcontinuum.sln

dotnet new sln -n appcontinuum

for module in {'Applications','Components'}; do
for directory in $module/*; do
    project_name=$(basename "$directory")
    dotnet sln appcontinuum.sln add $module/$project_name/$project_name.csproj
done
done
