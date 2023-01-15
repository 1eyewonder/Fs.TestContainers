module Fs.TestContainers.Test

open DotNet.Testcontainers.Builders
open Image
open System

open Container

let envVars =
    [
        "env1", "value1"
        "env2", "value2"
    ]
    |> Map.ofList

let imageName =
    image {
        name (Guid.NewGuid().ToString("D"))
        directory' (CommonDirectoryPath.GetSolutionDirectory()) "World"
        dockerfile "Dockerfile"
    } |> Image.build

let container =
    container {
        entrypoint "nginx"
        commands [|"-t"|]
    } |> Container.build
