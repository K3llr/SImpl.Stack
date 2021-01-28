#!/bin/bash
git clean -xfd
dotnet pack ../src/spike.stack.sln -c Release
for filename in ../dist/*.nupkg
do
    echo "$filename"
done