module Fs.TestContainers.Container

open System
open System.Collections.Generic
open System.Threading
open System.Threading.Tasks
open Docker.DotNet.Models
open DotNet.Testcontainers
open DotNet.Testcontainers.Configurations
open DotNet.Testcontainers.Containers
open DotNet.Testcontainers.Images
open DotNet.Testcontainers.Networks
open DotNet.Testcontainers.Volumes

[<RequireQualifiedAccess>]
module ContainerBuilder =

  let withImageName (image: string) (builder: Builders.ContainerBuilder) = builder.WithImage(image)

  let withImage (image: IImage) (builder: Builders.ContainerBuilder) = builder.WithImage(image)

  let withImagePullPolicy (policy: ImageInspectResponse -> bool) (builder: Builders.ContainerBuilder) =
    builder.WithImagePullPolicy(policy)

  let withName (name: string) (builder: Builders.ContainerBuilder) = builder.WithName(name)

  let withHostName (hostname: string) (builder: Builders.ContainerBuilder) = builder.WithHostname(hostname)

  let withMacAddress (macAddress: string) (builder: Builders.ContainerBuilder) = builder.WithMacAddress(macAddress)

  let withWorkingDirectory (workingDirectory: string) (builder: Builders.ContainerBuilder) =
    builder.WithWorkingDirectory(workingDirectory)

  let withEntryPoint (entrypoint: string) (builder: Builders.ContainerBuilder) = builder.WithEntrypoint(entrypoint)

  let withCommand (command: string[]) (builder: Builders.ContainerBuilder) = builder.WithCommand(command)

  let withEnvironment (name: string, value: string) (builder: Builders.ContainerBuilder) =
    builder.WithEnvironment(name, value)

  let withEnvironmentVars (environmentVars: #IReadOnlyDictionary<string, string>) (builder: Builders.ContainerBuilder) =
    builder.WithEnvironment(environmentVars)

  let withExposedPort (port: int) (builder: Builders.ContainerBuilder) = builder.WithExposedPort(port)

  let withExposedPortString (port: string) (builder: Builders.ContainerBuilder) = builder.WithExposedPort(port)

  let withPortBinding (port: int, assignRandomHostPort: bool option) (builder: Builders.ContainerBuilder) =
    match assignRandomHostPort with
    | Some assignRandomHostPort -> builder.WithPortBinding(port, assignRandomHostPort)
    | None -> builder.WithPortBinding(port)

  let withPortBindingString (port: string, assignRandomHostPort: bool option) (builder: Builders.ContainerBuilder) =
    match assignRandomHostPort with
    | Some assignRandomHostPort -> builder.WithPortBinding(port, assignRandomHostPort)
    | None -> builder.WithPortBinding(port)

  let withPortContainerBinding (hostPort: int, containerPort: int) (builder: Builders.ContainerBuilder) =
    builder.WithPortBinding(hostPort, containerPort)

  let withPortContainerBindingString (hostPort: string, containerPort: string) (builder: Builders.ContainerBuilder) =
    builder.WithPortBinding(hostPort, containerPort)

  let withResourceMapping (resourceMapping: IResourceMapping) (builder: Builders.ContainerBuilder) =
    builder.WithResourceMapping(resourceMapping)

  let withResourceMappingString (source: string, destination: string) (builder: Builders.ContainerBuilder) =
    builder.WithResourceMapping(source, destination)

  let withResourceMappingBytes (source: byte[], destination: string) (builder: Builders.ContainerBuilder) =
    builder.WithResourceMapping(source, destination)

  let withMount (mount: IMount) (builder: Builders.ContainerBuilder) = builder.WithMount(mount)

  let withBindMount
    (source: string, destination: string, accessMode: AccessMode option)
    (builder: Builders.ContainerBuilder)
    =
    match accessMode with
    | Some accessMode -> builder.WithBindMount(source, destination, accessMode)
    | None -> builder.WithBindMount(source, destination)

  let withVolumeMountString
    (source: string, destination: string, accessMode: AccessMode option)
    (builder: Builders.ContainerBuilder)
    =
    match accessMode with
    | Some accessMode -> builder.WithVolumeMount(source, destination, accessMode)
    | None -> builder.WithVolumeMount(source, destination)

  let withVolumeMount
    (source: IVolume, destination: string, accessMode: AccessMode option)
    (builder: Builders.ContainerBuilder)
    =
    match accessMode with
    | Some accessMode -> builder.WithVolumeMount(source, destination, accessMode)
    | None -> builder.WithVolumeMount(source, destination)

  let withTmpfsMount (destination: string, accessMode: AccessMode option) (builder: Builders.ContainerBuilder) =
    match accessMode with
    | Some accessMode -> builder.WithTmpfsMount(destination, accessMode)
    | None -> builder.WithTmpfsMount(destination)

  let withNetworkString (name: string) (builder: Builders.ContainerBuilder) = builder.WithNetwork(name)

  let withNetwork (network: INetwork) (builder: Builders.ContainerBuilder) = builder.WithNetwork(network)

  let withNetworkAliases (aliases: string seq) (builder: Builders.ContainerBuilder) =
    builder.WithNetworkAliases(aliases)

  let withAutoRemove (autoRemove: bool) (builder: Builders.ContainerBuilder) = builder.WithAutoRemove(autoRemove)

  let withPrivileged (privileged: bool) (builder: Builders.ContainerBuilder) = builder.WithPrivileged(privileged)

  let withWaitStrategy (waitStrategy: IWaitForContainerOS) (builder: Builders.ContainerBuilder) =
    builder.WithWaitStrategy(waitStrategy)

  let withCreateParameterModifier
    (parameterModifier: CreateContainerParameters -> unit)
    (builder: Builders.ContainerBuilder)
    =
    builder.WithCreateParameterModifier(parameterModifier)

  let withStartupCallback
    (startupCallback: IContainer -> CancellationToken -> Task)
    (builder: Builders.ContainerBuilder)
    =
    builder.WithStartupCallback(startupCallback)

  let build (builder: Builders.ContainerBuilder) = builder.Build()

type ContainerBuilder() =

  member _.Zero _ = Builders.ContainerBuilder()

  member _.Yield _ = Builders.ContainerBuilder()

  /// <summary>
  /// Sets the Docker image, which is used to create the Testcontainer instances.
  /// </summary>
  /// <param name="c">A configured instance of ContainerBuilder</param>
  /// <param name="image">The Docker image.</param>
  [<CustomOperation "imageName">]
  member _.imageName(builder: Builders.ContainerBuilder, image: string) =
    builder |> ContainerBuilder.withImageName image

  /// <summary>
  /// Sets the Docker image, which is used to create the Testcontainer instances.
  /// </summary>
  /// <param name="c">A configured instance of ContainerBuilder</param>
  /// <param name="image">The Docker image.</param>
  [<CustomOperation "image">]
  member _.image(builder: Builders.ContainerBuilder, image: IImage) =
    builder |> ContainerBuilder.withImage image

  /// <summary>
  /// Sets the image pull policy of the Testcontainer
  /// </summary>
  /// <param name="c">A configured instance of ContainerBuilder</param>
  /// <param name="policy">The image pull policy</param>
  [<CustomOperation "imagePullPolicy">]
  member _.imagePullPolicy(builder: Builders.ContainerBuilder, policy: ImageInspectResponse -> bool) =
    builder |> ContainerBuilder.withImagePullPolicy policy

  /// <summary>
  /// Sets the name of the Testcontainer
  /// </summary>
  /// <param name="c">A configured instance of ContainerBuilder</param>
  /// <param name="name">Testcontainer's name</param>
  [<CustomOperation "name">]
  member _.withName(builder: Builders.ContainerBuilder, name: string) =
    builder |> ContainerBuilder.withName name

  /// <summary>
  /// Sets the hostname of the Testcontainer
  /// </summary>
  /// <param name="c">A configured instance of ContainerBuilder</param>
  /// <param name="hostname">Testcontainer's hostname</param>
  [<CustomOperation "hostname">]
  member _.hostname(builder: Builders.ContainerBuilder, hostname: string) =
    builder |> ContainerBuilder.withHostName hostname

  /// <summary>
  /// Sets the MAC address of the Testcontainer
  /// </summary>
  /// <param name="c">A configured instance of ContainerBuilder</param>
  /// <param name="macAddress">Testcontainer's MAC address</param>
  [<CustomOperation "macAddress">]
  member _.macAddress(builder: Builders.ContainerBuilder, macAddress: string) =
    builder |> ContainerBuilder.withMacAddress macAddress

  /// <summary>
  /// Sets the working directory of the Testcontainer for the instruction sets
  /// </summary>
  /// <param name="c">A configured instance of ContainerBuilder</param>
  /// <param name="workingDirectory">Working directory</param>
  [<CustomOperation "workingDirectory">]
  member _.workingDirectory(builder: Builders.ContainerBuilder, workingDirectory: string) =
    builder |> ContainerBuilder.withWorkingDirectory workingDirectory

  /// <summary>
  /// Overrides the entrypoint of the Testcontainer to configure an executable
  /// </summary>
  /// <param name="c">A configured instance of ContainerBuilder</param>
  /// <param name="entrypoint">Entrypoint executable</param>
  [<CustomOperation "entrypoint">]
  member _.entrypoint(builder: Builders.ContainerBuilder, entrypoint: string) =
    builder |> ContainerBuilder.withEntryPoint entrypoint

  /// <summary>
  /// Overrides the command of the Testcontainer to provide defaults for executing
  /// </summary>
  /// <param name="c">A configured instance of ContainerBuilder</param>
  /// <param name="command">List of commands, "executable", "param1", "param2" or "param1", "param2""</param>
  [<CustomOperation "commands">]
  member _.commands(builder: Builders.ContainerBuilder, command: string[]) =
    builder |> ContainerBuilder.withCommand command

  /// <summary>
  /// Exports the environment variable in the Testcontainer
  /// </summary>
  /// <param name="c">A configured instance of ContainerBuilder</param>
  /// <param name="name">Environment variable name</param>
  /// <param name="value">Environment variable value</param>
  [<CustomOperation "environment">]
  member _.environment(builder: Builders.ContainerBuilder, name: string, value: string) =
    builder |> ContainerBuilder.withEnvironment (name, value)

  /// <summary>
  /// Exports the environment variable in the Testcontainer
  /// </summary>
  /// <param name="c">A configured instance of ContainerBuilder</param>
  /// <param name="environmentVars">Dictionary of environment variables</param>
  [<CustomOperation "environment'">]
  member _.withEnvironment(builder: Builders.ContainerBuilder, environmentVars: #IReadOnlyDictionary<string, string>) =
    builder |> ContainerBuilder.withEnvironmentVars environmentVars

  /// <summary>
  /// Sets the port of the Testcontainer to expose, without publishing the port to the host system’s interfaces
  /// </summary>
  /// <param name="c">A configured instance of ContainerBuilder</param>
  /// <param name="port">Port to expose</param>
  [<CustomOperation "exposedPortInt">]
  member _.exposedPortInt(builder: Builders.ContainerBuilder, port: int) =
    builder |> ContainerBuilder.withExposedPort port

  /// <summary>
  /// Sets the port of the Testcontainer to expose, without publishing the port to the host system’s interfaces
  /// </summary>
  /// <param name="c">A configured instance of ContainerBuilder</param>
  /// <param name="port">Port to expose</param>
  [<CustomOperation "exposedPortString">]
  member _.exposedPortString(builder: Builders.ContainerBuilder, port: string) =
    builder |> ContainerBuilder.withExposedPortString port

  /// <summary>
  /// Binds the port of the Testcontainer to the same port of the host machine
  /// </summary>
  /// <param name="c">A configured instance of ContainerBuilder</param>
  /// <param name="port">Port to bind between Testcontainer and host machine</param>
  /// <param name="assignRandomHostPort">True, Testcontainer will bind the port to a random host port, otherwise the host and container ports are the same</param>
  [<CustomOperation "portBinding">]
  member _.portBinding(builder: Builders.ContainerBuilder, port: int, ?assignRandomHostPort: bool) =
    builder |> ContainerBuilder.withPortBinding (port, assignRandomHostPort)

  /// <summary>
  /// Binds the port of the Testcontainer to the same port of the host machine
  /// </summary>
  /// <param name="c">A configured instance of ContainerBuilder</param>
  /// <param name="port">Port to bind between Testcontainer and host machine</param>
  /// <param name="assignRandomHostPort">True, Testcontainer will bind the port to a random host port, otherwise the host and container ports are the same</param>
  /// <remarks>Append /tcp|udp|sctp to change the protocol e.g. "53/udp". Default: tcp</remarks>
  [<CustomOperation "portBindingString">]
  member _.portBindingString(builder: Builders.ContainerBuilder, port: string, ?assignRandomHostPort: bool) =
    builder |> ContainerBuilder.withPortBindingString (port, assignRandomHostPort)

  /// <summary>
  /// Binds the port of the Testcontainer to the same port of the host machine
  /// </summary>
  /// <param name="c">A configured instance of ContainerBuilder</param>
  /// <param name="hostPort">Port of the host machine</param>
  /// <param name="containerPort">Port of the Testcontainer</param>
  [<CustomOperation "portContainerBinding">]
  member _.portContainerBinding(builder: Builders.ContainerBuilder, hostPort: int, containerPort: int) =
    builder |> ContainerBuilder.withPortContainerBinding (hostPort, containerPort)

  /// <summary>
  /// Binds the port of the Testcontainer to the same port of the host machine
  /// </summary>
  /// <param name="c">A configured instance of ContainerBuilder</param>
  /// <param name="hostPort">Port of the host machine</param>
  /// <param name="containerPort">Port of the test container</param>
  /// <remarks>Append /tcp|udp|sctp to change the protocol e.g. "53/udp". Default: tcp</remarks>
  [<CustomOperation "portContainerBindingString">]
  member _.portContainerBindingString(builder: Builders.ContainerBuilder, hostPort: string, containerPort: string) =
    builder
    |> ContainerBuilder.withPortContainerBindingString (hostPort, containerPort)

  [<CustomOperation "resourceMappingString">]
  member _.resourceMappingString(builder: Builders.ContainerBuilder, source: string, destination: string) =
    builder |> ContainerBuilder.withResourceMappingString (source, destination)

  [<CustomOperation "resourceMappingBytes">]
  member _.resourceMappingBytes(builder: Builders.ContainerBuilder, source: byte[], destination: string) =
    builder |> ContainerBuilder.withResourceMappingBytes (source, destination)

  [<CustomOperation "resourceMapping">]
  member _.resourceMapping(builder: Builders.ContainerBuilder, resourceMapping: IResourceMapping) =
    builder |> ContainerBuilder.withResourceMapping resourceMapping

  [<CustomOperation "mount">]
  member _.mount(builder: Builders.ContainerBuilder, mount: IMount) =
    builder |> ContainerBuilder.withMount mount

  [<CustomOperation "bindMount">]
  member _.bindMount(builder: Builders.ContainerBuilder, source: string, destination: string, ?accessMode: AccessMode) =
    builder |> ContainerBuilder.withBindMount (source, destination, accessMode)

  [<CustomOperation "volumeMountString">]
  member _.volumeMountString
    (builder: Builders.ContainerBuilder, source: string, destination: string, ?accessMode: AccessMode)
    =
    builder
    |> ContainerBuilder.withVolumeMountString (source, destination, accessMode)

  [<CustomOperation "volumeMount">]
  member _.volumeMount
    (builder: Builders.ContainerBuilder, source: IVolume, destination: string, ?accessMode: AccessMode)
    =
    builder |> ContainerBuilder.withVolumeMount (source, destination, accessMode)

  [<CustomOperation "tmpfsMount">]
  member _.tmpfsMount(builder: Builders.ContainerBuilder, destination: string, ?accessMode: AccessMode) =
    builder |> ContainerBuilder.withTmpfsMount (destination, accessMode)

  [<CustomOperation "networkString">]
  member _.networkString(builder: Builders.ContainerBuilder, name: string) =
    builder |> ContainerBuilder.withNetworkString name

  [<CustomOperation "network">]
  member _.network(builder: Builders.ContainerBuilder, network: INetwork) =
    builder |> ContainerBuilder.withNetwork network

  [<CustomOperation "networkAliases">]
  member _.networkAliases(builder: Builders.ContainerBuilder, aliases: string seq) =
    builder |> ContainerBuilder.withNetworkAliases aliases

  [<CustomOperation "autoRemove">]
  member _.autoRemove(builder: Builders.ContainerBuilder, ?autoRemove: bool) =
    builder |> ContainerBuilder.withAutoRemove (defaultArg autoRemove true)

  [<CustomOperation "privileged">]
  member _.privileged(builder: Builders.ContainerBuilder, privileged: bool) =
    builder |> ContainerBuilder.withPrivileged privileged

  [<CustomOperation "waitStrategy">]
  member _.waitStrategy(builder: Builders.ContainerBuilder, waitStrategy: IWaitForContainerOS) =
    builder |> ContainerBuilder.withWaitStrategy waitStrategy

  [<CustomOperation "createParameterModifier">]
  member _.createParameterModifier
    (builder: Builders.ContainerBuilder, parameterModifier: CreateContainerParameters -> unit)
    =
    builder |> ContainerBuilder.withCreateParameterModifier parameterModifier

  [<CustomOperation "startupCallback">]
  member _.startupCallback
    (builder: Builders.ContainerBuilder, startupCallback: IContainer -> CancellationToken -> Task)
    =
    builder |> ContainerBuilder.withStartupCallback startupCallback

let container = ContainerBuilder()
