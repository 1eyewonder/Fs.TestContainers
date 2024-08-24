# Fs.TestContainers (in development)

Fs.TestContainers is a wrapper around the fluent builders found in [testcontainers-dotnet](https://github.com/testcontainers/testcontainers-dotnet). It allows us to create images, containers, etc. using the F# computation expression syntax.

## Builds

GitHub Actions |
:---: |
[![GitHub Actions](https://github.com/1eyewonder/Fs.TestContainers/workflows/Build%20main/badge.svg)](https://github.com/1eyewonder/Fs.TestContainers/actions?query=branch%3Amain) |
[![Build History](https://buildstats.info/github/chart/1eyewonder/Fs.TestContainers)](https://github.com/1eyewonder/Fs.TestContainers/actions?query=branch%3Amain) |

## NuGet

| Package           | Stable                                                                                                               | Prerelease                                                                                                                                   |
| ----------------- | -------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------- |
| Fs.TestContainers | [![NuGet Badge](https://buildstats.info/nuget/Fs.TestContainers)](https://www.nuget.org/packages/Fs.TestContainers/) | [![NuGet Badge](https://buildstats.info/nuget/Fs.TestContainers?includePreReleases=true)](https://www.nuget.org/packages/Fs.TestContainers/) |

---

## Examples

```fsharp
let myImage =
  image {
    name "somecoolimage"
    directory "testing"
    dockerfile "Dockerfile"
  }
  |> ImageBuilder.build

let container =
  container {
    image myImage
    commands [| "-t" |]
    autoRemove
  }
  |> ContainerBuilder.build
---




