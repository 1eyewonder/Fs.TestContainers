module Fs.TestContainers.Test

open DotNet.Testcontainers.Builders
open Image
open System

open Container

let envVars = [ "env1", "value1"; "env2", "value2" ] |> Map.ofList

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
