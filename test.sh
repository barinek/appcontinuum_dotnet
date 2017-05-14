#!/bin/sh

set -e

for directory in components/*; do
    project_name=$(basename "$directory")
    if [[ $project_name == *"Test" ]]; then
        dotnet test components/$project_name/$project_name.csproj
    fi
done
