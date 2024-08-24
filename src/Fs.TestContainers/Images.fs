module Fs.TestContainers.Image

open Docker.DotNet.Models
open DotNet.Testcontainers.Builders
open System

[<RequireQualifiedAccess>]
module ImageBuilder =

  /// <summary>
  /// Sets the name of the Docker image
  /// </summary>
  /// <param name="name">Docker image name</param>
  /// <param name="image">Docker image being built</param>
  let withName (name: string) (image: ImageFromDockerfileBuilder) = image.WithName(name)

  /// <summary>
  /// Sets the name of the Dockerfile
  /// </summary>
  /// <param name="dockerfile">Dockerfile name</param>
  /// <param name="image">Docker image being built</param>
  let withDockerfile dockerfile (image: ImageFromDockerfileBuilder) = image.WithDockerfile(dockerfile)

  /// <summary>
  /// Sets the base directory of the Dockerfile
  /// </summary>
  /// <param name="directory">Dockerfile base directory</param>
  /// <param name="image">Docker image being built</param>
  let withDockerfileDirectory directory (image: ImageFromDockerfileBuilder) =
    image.WithDockerfileDirectory(directory)

  /// <summary>
  /// Sets the image build policy
  /// </summary>
  /// <param name="policy">Image build policy</param>
  /// <param name="image">Docker image being built</param>
  let withImageBuildPolicy (policy: ImageInspectResponse -> bool) (image: ImageFromDockerfileBuilder) =
    image.WithImageBuildPolicy(policy)

  /// <summary>
  /// If true, Testcontainer will remove the existing Docker image. Otherwise, Testcontainer will keep the Docker image
  /// </summary>
  /// <param name="deleteIfExists">True, Testcontainer will remove the Docker image. Otherwise, Testcontainer will keep it</param>
  /// <param name="image">Docker image being built</param>
  let deleteIfExists deleteIfExists (image: ImageFromDockerfileBuilder) =
    image.WithDeleteIfExists(deleteIfExists)

  /// <summary>
  /// Adds a Docker build argument
  /// </summary>
  /// <param name="name">Build argument name</param>
  /// <param name="value">Build argument name</param>
  let withBuildArg (name, value) (image: ImageFromDockerfileBuilder) = image.WithBuildArgument(name, value)

  /// <summary>
  /// Adds arguments to Docker build
  /// </summary>
  /// <param name="args">Collection of build argument name * build argument value</param>
  /// <param name="image">Docker image being built</param>
  let withBuildArgs (args: (string * string) seq) (image: ImageFromDockerfileBuilder) =
    args
    |> Seq.fold
      (fun (builder: ImageFromDockerfileBuilder) (name, value) -> builder.WithBuildArgument(name, value))
      image

  let build (image: ImageFromDockerfileBuilder) = image.Build()

type ImageBuilder() =

  member _.Zero _ = ImageFromDockerfileBuilder()

  member _.Yield _ = ImageFromDockerfileBuilder()

  /// <summary>
  /// Sets the name of the Docker image
  /// </summary>
  /// <param name="image">Docker image being built</param>
  /// <param name="name">Docker image name</param>
  [<CustomOperation("name")>]
  member _.name(image: ImageFromDockerfileBuilder, name: string) = image |> ImageBuilder.withName name

  /// <summary>
  /// Sets the name of the Dockerfile
  /// </summary>
  /// <param name="image">Docker image being built</param>
  /// <param name="dockerfile">Dockerfile name</param>
  [<CustomOperation("dockerfile")>]
  member _.dockerfile(image: ImageFromDockerfileBuilder, dockerfile: string) =
    image |> ImageBuilder.withDockerfile dockerfile

  /// <summary>
  /// Sets the base directory of the Dockerfile
  /// </summary>
  /// <param name="image">Docker image being built</param>
  /// <param name="directory">Dockerfile base directory</param>
  [<CustomOperation("directory")>]
  member _.directory(image: ImageFromDockerfileBuilder, directory: string) =
    image |> ImageBuilder.withDockerfileDirectory directory

  /// <summary>
  /// If true, Testcontainer will remove the existing Docker image. Otherwise, Testcontainer will keep the Docker image
  /// </summary>
  /// <param name="image">Docker image being built</param>
  /// <param name="deleteIfExists">Specifies if the image will be deleted if it already exists. Defaults to true if not provided</param>
  [<CustomOperation("deleteIfExists")>]
  member _.deleteIfExists(image: ImageFromDockerfileBuilder, ?deleteIfExists: bool) =
    match deleteIfExists with
    | Some deleteIfExists -> image |> ImageBuilder.deleteIfExists deleteIfExists
    | None -> image |> ImageBuilder.deleteIfExists true

  /// <summary>
  /// Adds a Docker build argument
  /// </summary>
  /// <param name="image">Docker image being built</param>
  /// <param name="name">Build argument name</param>
  /// <param name="value">Build argument name</param>
  [<CustomOperation("buildArg")>]
  member _.buildArg(image: ImageFromDockerfileBuilder, name, value) =
    image |> ImageBuilder.withBuildArg (name, value)

  /// <summary>
  /// Adds arguments to Docker build
  /// </summary>
  /// <param name="image">Docker image being built</param>
  /// <param name="args">Collection of build argument name * build argument value</param>
  [<CustomOperation("buildArgs")>]
  member _.buildArgs(image, args: (string * string) seq) =
    image |> ImageBuilder.withBuildArgs args

let image = ImageBuilder()

[<RequireQualifiedAccess>]
module Image =

  open DotNet.Testcontainers.Images

  let create (image: IFutureDockerImage) = image.CreateAsync()

  let delete (image: IFutureDockerImage) = image.DeleteAsync()
