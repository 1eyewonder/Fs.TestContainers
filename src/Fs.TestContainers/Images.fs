/// <summary> Module containing image functions </summary>
module Fs.TestContainers.Image

open System.Collections.Generic
open DotNet.Testcontainers.Builders
open DotNet.Testcontainers.Images

type ImageBuilder () =
    inherit Abstract.AbstractBuilder<IImageFromDockerfileBuilder> ()

    member _.Zero _ = ImageFromDockerfileBuilder()

    member _.Yield _ = ImageFromDockerfileBuilder()

    /// <summary>
    /// Sets the name of the Docker image
    /// </summary>
    /// <param name="image">Docker image being built</param>
    /// <param name="name">Docker image name</param>
    [<CustomOperation "name">]
    member _.withName(image: IImageFromDockerfileBuilder, name: string) = image.WithName(name)

    /// <summary>
    /// Sets the name of the Docker image
    /// </summary>
    /// /// <param name="image">Docker image being built</param>
    /// <param name="name">Docker image name</param>
    [<CustomOperation "imageName">]
    member _.withImageName(image: IImageFromDockerfileBuilder, name: IDockerImage) =
        image.WithName(name)

    /// <summary>
    /// Sets the name of the Dockerfile
    /// </summary>
    /// /// <param name="image">Docker image being built</param>
    /// <param name="dockerfile">Dockerfile name</param>
    [<CustomOperation "dockerfile">]
    member _.withDockerfile(image: IImageFromDockerfileBuilder, dockerfile: string) =
        image.WithDockerfile(dockerfile)

    /// <summary>
    /// Sets the base directory of the Dockerfile
    /// </summary>
    /// /// <param name="image">Docker image being built</param>
    /// <param name="directory">Dockerfile base directory</param>
    [<CustomOperation "directory">]
    member _.withDockerfileDirectory(image: IImageFromDockerfileBuilder, directory: string) =
        image.WithDockerfileDirectory(directory)

    /// <summary>
    /// Sets the base directory of the Dockerfile
    /// </summary>
    /// <param name="image">Docker image being built</param>
    /// <param name="commonDirectoryPath">Common directory path that contains the Dockerfile base directory</param>
    /// <param name="dockerfileDirectory">Dockerfile base directory</param>
    [<CustomOperation "directory'">]
    member _.withDockerfileDirectory2
        (
            image: IImageFromDockerfileBuilder,
            commonDirectoryPath,
            dockerfileDirectory
        ) =
        image.WithDockerfileDirectory(commonDirectoryPath, dockerfileDirectory)

    /// <summary>
    /// If true, Testcontainer will remove the existing Docker image. Otherwise, Testcontainer will keep the Docker image
    /// </summary>
    /// /// <param name="image">Docker image being built</param>
    /// <param name="deleteIfExists">True, Testcontainer will remove the Docker image. Otherwise, Testcontainer will keep it</param>
    [<CustomOperation "deleteIfExists">]
    member _.withDeleteIfExists(image: IImageFromDockerfileBuilder, deleteIfExists: bool) =
        image.WithDeleteIfExists(deleteIfExists)

    /// <summary>
    /// Adds a Docker build argument
    /// </summary>
    /// /// <param name="image">Docker image being built</param>
    /// <param name="name">Build argument name</param>
    /// <param name="value">Build argument name</param>
    [<CustomOperation "arg">]
    member _.withArgument(image: IImageFromDockerfileBuilder, name, value) =
        image.WithBuildArgument(name, value)

    /// <summary>
    /// Adds arguments to Docker build
    /// </summary>
    /// /// <param name="image">Docker image being built</param>
    /// <param name="args">Collection of build argument name * build argument value</param>
    [<CustomOperation "args">]
    member _.withBuildArgs(image, args: #IEnumerable<KeyValuePair<string, string>>) =
        args
        |> Seq.fold
            (fun (acc: IImageFromDockerfileBuilder) arg -> acc.WithBuildArgument(arg.Key, arg.Value))
            image

let image = ImageBuilder()

/// <summary>
/// Builds the instance of <see cref="DotNet.TestContainers.Builders.IImageFromDockerfileBuilder"/> with the given configuration
/// </summary>
/// <returns>FullName of the created image.</returns>
let build (image: IImageFromDockerfileBuilder) =
    image.Build()
    |> Async.AwaitTask
    |> Async.RunSynchronously
