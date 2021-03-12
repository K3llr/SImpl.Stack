#!/bin/bash
for filename in ../dist/*.nupkg
do
    dotnet nuget push "$filename" --source https://api.nuget.org/v3/index.json --skip-duplicate $@
done