#!/bin/bash
for filename in ../dist/*.nupkg
do
    dotnet nuget push "$filename" -s http://nuget.development.novicell.dk/ $@
done
#rm -rf dist