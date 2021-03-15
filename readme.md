# SImpl Stack - A .NET host runtime with a unified and stackable module abstraction

[![Build status](https://ci.appveyor.com/api/projects/status/qdy4xmxq81tapubr/branch/master?svg=true)](https://ci.appveyor.com/project/SImpl/simpl-stack/branch/master) ![Nuget](https://img.shields.io/nuget/v/SImpl)

## Working with templates

The solution contains "runable" .NET project based on the dotnet new scaffolding engine. The template can be modified as any other .NET solution.r

### Installing and updating the templates from the source code

You can use the template locally by using the following command. You need to change to the proper path to the source code on your machine.

```cil
dotnet new --install ./SImpl.Templates.WebApi/
```

### Resetting the dotnet new tool

Sometimes, when developing templates, things get a little messy and you'll need to reset the dotnet new tool in order to get rid of installed templates on your machine.

Run the following command to reset the dotnet new tool:

```cli
dotnet new --debug:reinit
```
