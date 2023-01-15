# Fs.TestContainers (in development)

Fs.TestContainers is a wrapper around the fluent builders found in [testcontainers-dotnet](https://github.com/testcontainers/testcontainers-dotnet). It allows us to create images, containers, etc. using the F# computation expression syntax.

## Builds

GitHub Actions |
:---: |
[![GitHub Actions](https://github.com/1eyewonder/Fs.TestContainers/workflows/Build%20master/badge.svg)](https://github.com/1eyewonder/Fs.TestContainers/actions?query=branch%3Amaster) |
[![Build History](https://buildstats.info/github/chart/1eyewonder/Fs.TestContainers)](https://github.com/1eyewonder/Fs.TestContainers/actions?query=branch%3Amaster) |

## NuGet

Package | Stable | Prerelease
--- | --- | ---
Fs.TestContainers | [![NuGet Badge](https://buildstats.info/nuget/Fs.TestContainers)](https://www.nuget.org/packages/Fs.TestContainers/) | [![NuGet Badge](https://buildstats.info/nuget/Fs.TestContainers?includePreReleases=true)](https://www.nuget.org/packages/Fs.TestContainers/)

---

## Examples

### Images

C# equivalent found [here](https://dotnet.testcontainers.org/api/create_docker_image/)
```fsharp
open DotNet.Testcontainers.Builders
open Image
open System

let imageName =
    image {
        name (Guid.NewGuid().ToString("D"))
        directory' (CommonDirectoryPath.GetSolutionDirectory()) "src"
        dockerfile "Dockerfile"
    } |> Image.build
```

### Containers

C# equivalent found [here](https://dotnet.testcontainers.org/api/create_docker_container/)
```fsharp
open Container

let container =
    container {
        entrypoint "nginx"
        commands [|"-t"|]
    } |> Container.build
```

---




