#!/bin/bash
for filename in ../dist/*.nupkg
do
    dotnet nuget push "$filename" -s http://nuget.development.novicell.dk/ --skip-duplicate $@
done