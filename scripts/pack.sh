#!/bin/bash
git clean -xfd ..
dotnet pack ../src/SImpl.Stack.sln -c Release
for filename in ../dist/*.nupkg
do
    echo "$filename"
done
