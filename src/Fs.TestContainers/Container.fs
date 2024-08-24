module Fs.TestContainers.Container

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

  /// <summary>
  /// Sets the Docker image, which is used to create the Testcontainer instances.
  /// </summary>
  /// <param name="image">The Docker image.</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withImageName (image: string) (builder: Builders.ContainerBuilder) = builder.WithImage(image)

  /// <summary>
  /// Sets the Docker image, which is used to create the Testcontainer instances.
  /// </summary>
  /// <param name="image">The Docker image.</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withImage (image: IImage) (builder: Builders.ContainerBuilder) = builder.WithImage(image)

  /// <summary>
  /// Sets the image pull policy of the Testcontainer
  /// </summary>
  /// <param name="policy">The image pull policy</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withImagePullPolicy (policy: ImageInspectResponse -> bool) (builder: Builders.ContainerBuilder) =
    builder.WithImagePullPolicy(policy)

  /// <summary>
  /// Sets the name of the Testcontainer
  /// </summary>
  /// <param name="name">Testcontainer's name</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withName (name: string) (builder: Builders.ContainerBuilder) = builder.WithName(name)

  /// <summary>
  /// Sets the hostname of the Testcontainer
  /// </summary>
  /// <param name="hostname">Testcontainer's hostname</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withHostName (hostname: string) (builder: Builders.ContainerBuilder) = builder.WithHostname(hostname)

  /// <summary>
  /// Sets the MAC address of the Testcontainer
  /// </summary>
  /// <param name="macAddress">Testcontainer's MAC address</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withMacAddress (macAddress: string) (builder: Builders.ContainerBuilder) = builder.WithMacAddress(macAddress)

  /// <summary>
  /// Sets the working directory of the Testcontainer for the instruction sets
  /// </summary>
  /// <param name="workingDirectory">Working directory</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withWorkingDirectory (workingDirectory: string) (builder: Builders.ContainerBuilder) =
    builder.WithWorkingDirectory(workingDirectory)

  /// <summary>
  /// Overrides the entrypoint of the Testcontainer to configure an executable
  /// </summary>
  /// <param name="entrypoint">Entrypoint executable</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withEntryPoint (entrypoint: string) (builder: Builders.ContainerBuilder) = builder.WithEntrypoint(entrypoint)

  /// <summary>
  /// Overrides the command of the Testcontainer to provide defaults for executing
  /// </summary>
  /// <param name="command">List of commands, "executable", "param1", "param2" or "param1", "param2""</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withCommand (command: string[]) (builder: Builders.ContainerBuilder) = builder.WithCommand(command)

  /// <summary>
  /// Exports the environment variable in the Testcontainer
  /// </summary>
  /// <param name="name">Environment variable name</param>
  /// <param name="value">Environment variable value</param>
  let withEnvironment (name: string, value: string) (builder: Builders.ContainerBuilder) =
    builder.WithEnvironment(name, value)

  /// <summary>
  /// Exports the environment variable in the Testcontainer
  /// </summary>
  /// <param name="environmentVars">Dictionary of environment variables</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withEnvironmentVars (environmentVars: #IReadOnlyDictionary<string, string>) (builder: Builders.ContainerBuilder) =
    builder.WithEnvironment(environmentVars)

  /// <summary>
  /// Sets the port of the Testcontainer to expose, without publishing the port to the host system’s interfaces
  /// </summary>
  /// <param name="port">Port to expose</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withExposedPort (port: int) (builder: Builders.ContainerBuilder) = builder.WithExposedPort(port)

  /// <summary>
  /// Sets the port of the Testcontainer to expose, without publishing the port to the host system’s interfaces
  /// </summary>
  /// <param name="port">Port to expose</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withExposedPortString (port: string) (builder: Builders.ContainerBuilder) = builder.WithExposedPort(port)

  /// <summary>
  /// Binds the port of the Testcontainer to the same port of the host machine
  /// </summary>
  /// <param name="port">Port to bind between Testcontainer and host machine</param>
  /// <param name="assignRandomHostPort">True, Testcontainer will bind the port to a random host port, otherwise the host and container ports are the same</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withPortBinding (port: int, assignRandomHostPort: bool option) (builder: Builders.ContainerBuilder) =
    match assignRandomHostPort with
    | Some assignRandomHostPort -> builder.WithPortBinding(port, assignRandomHostPort)
    | None -> builder.WithPortBinding(port)

  /// <summary>
  /// Binds the port of the Testcontainer to the same port of the host machine
  /// </summary>
  /// <param name="port">Port to bind between Testcontainer and host machine</param>
  /// <param name="assignRandomHostPort">True, Testcontainer will bind the port to a random host port, otherwise the host and container ports are the same</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withPortBindingString (port: string, assignRandomHostPort: bool option) (builder: Builders.ContainerBuilder) =
    match assignRandomHostPort with
    | Some assignRandomHostPort -> builder.WithPortBinding(port, assignRandomHostPort)
    | None -> builder.WithPortBinding(port)

  /// <summary>
  /// Binds the port of the Testcontainer to the same port of the host machine
  /// </summary>
  /// <param name="hostPort">Port of the host machine</param>
  /// <param name="containerPort">Port of the Testcontainer</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withPortContainerBinding (hostPort: int, containerPort: int) (builder: Builders.ContainerBuilder) =
    builder.WithPortBinding(hostPort, containerPort)

  /// <summary>
  /// Binds the port of the Testcontainer to the same port of the host machine
  /// </summary>
  /// <param name="hostPort">Port of the host machine</param>
  /// <param name="containerPort">Port of the test container</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withPortContainerBindingString (hostPort: string, containerPort: string) (builder: Builders.ContainerBuilder) =
    builder.WithPortBinding(hostPort, containerPort)

  /// <summary>
  /// Sets the resource mapping of the Testcontainer
  /// </summary>
  /// <param name="resourceMapping">Resource mapping</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withResourceMapping (resourceMapping: IResourceMapping) (builder: Builders.ContainerBuilder) =
    builder.WithResourceMapping(resourceMapping)

  /// <summary>
  /// Sets the resource mapping of the Testcontainer
  /// </summary>
  /// <param name="source">Source of the resource mapping</param>
  /// <param name="destination">Destination of the resource mapping</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withResourceMappingString (source: string, destination: string) (builder: Builders.ContainerBuilder) =
    builder.WithResourceMapping(source, destination)

  /// <summary>
  /// Sets the resource mapping of the Testcontainer
  /// </summary>
  /// <param name="source">Source of the resource mapping</param>
  /// <param name="destination">Destination of the resource mapping</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withResourceMappingBytes (source: byte[], destination: string) (builder: Builders.ContainerBuilder) =
    builder.WithResourceMapping(source, destination)

  /// <summary>
  /// Sets the mount of the Testcontainer
  /// </summary>
  /// <param name="mount">Mount</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withMount (mount: IMount) (builder: Builders.ContainerBuilder) = builder.WithMount(mount)

  /// <summary>
  /// Sets the bind mount of the Testcontainer
  /// </summary>
  /// <param name="source">Source of the bind mount</param>
  /// <param name="destination">Destination of the bind mount</param>
  /// <param name="accessMode">Access mode of the bind mount</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withBindMount
    (source: string, destination: string, accessMode: AccessMode option)
    (builder: Builders.ContainerBuilder)
    =
    match accessMode with
    | Some accessMode -> builder.WithBindMount(source, destination, accessMode)
    | None -> builder.WithBindMount(source, destination)

  /// <summary>
  /// Sets the volume mount of the Testcontainer
  /// </summary>
  /// <param name="source">Source of the volume mount</param>
  /// <param name="destination">Destination of the volume mount</param>
  /// <param name="accessMode">Access mode of the volume mount</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withVolumeMountString
    (source: string, destination: string, accessMode: AccessMode option)
    (builder: Builders.ContainerBuilder)
    =
    match accessMode with
    | Some accessMode -> builder.WithVolumeMount(source, destination, accessMode)
    | None -> builder.WithVolumeMount(source, destination)

  /// <summary>
  /// Sets the volume mount of the Testcontainer
  /// </summary>
  /// <param name="source">Source of the volume mount</param>
  /// <param name="destination">Destination of the volume mount</param>
  /// <param name="accessMode">Access mode of the volume mount</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withVolumeMount
    (source: IVolume, destination: string, accessMode: AccessMode option)
    (builder: Builders.ContainerBuilder)
    =
    match accessMode with
    | Some accessMode -> builder.WithVolumeMount(source, destination, accessMode)
    | None -> builder.WithVolumeMount(source, destination)

  /// <summary>
  /// Sets the tmpfs mount of the Testcontainer
  /// </summary>
  /// <param name="destination">Destination of the tmpfs mount</param>
  /// <param name="accessMode">Access mode of the tmpfs mount</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withTmpfsMount (destination: string, accessMode: AccessMode option) (builder: Builders.ContainerBuilder) =
    match accessMode with
    | Some accessMode -> builder.WithTmpfsMount(destination, accessMode)
    | None -> builder.WithTmpfsMount(destination)

  /// <summary>
  /// Sets the network of the Testcontainer
  /// </summary>
  /// <param name="name">Network name</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withNetworkString (name: string) (builder: Builders.ContainerBuilder) = builder.WithNetwork(name)

  /// <summary>
  /// Sets the network of the Testcontainer
  /// </summary>
  /// <param name="network">Network</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withNetwork (network: INetwork) (builder: Builders.ContainerBuilder) = builder.WithNetwork(network)

  /// <summary>
  /// Sets the network aliases of the Testcontainer
  /// </summary>
  /// <param name="aliases">Network aliases</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withNetworkAliases (aliases: string seq) (builder: Builders.ContainerBuilder) =
    builder.WithNetworkAliases(aliases)

  /// <summary>
  /// Sets the auto remove of the Testcontainer
  /// </summary>
  /// <param name="autoRemove">True, Testcontainer will be removed after the Testcontainer is stopped</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withAutoRemove (autoRemove: bool) (builder: Builders.ContainerBuilder) = builder.WithAutoRemove(autoRemove)

  /// <summary>
  /// Sets the privileged of the Testcontainer
  /// </summary>
  /// <param name="privileged">True, Testcontainer will be privileged</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withPrivileged (privileged: bool) (builder: Builders.ContainerBuilder) = builder.WithPrivileged(privileged)

  /// <summary>
  /// Sets the wait strategy of the Testcontainer
  /// </summary>
  /// <param name="waitStrategy">Wait strategy</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withWaitStrategy (waitStrategy: IWaitForContainerOS) (builder: Builders.ContainerBuilder) =
    builder.WithWaitStrategy(waitStrategy)

  /// <summary>
  /// Sets the create parameter modifier of the Testcontainer
  /// </summary>
  /// <param name="parameterModifier">Create parameter modifier</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withCreateParameterModifier
    (parameterModifier: CreateContainerParameters -> unit)
    (builder: Builders.ContainerBuilder)
    =
    builder.WithCreateParameterModifier(parameterModifier)

  /// <summary>
  /// Sets the startup callback of the Testcontainer
  /// </summary>
  /// <param name="startupCallback">Startup callback</param>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let withStartupCallback
    (startupCallback: IContainer -> CancellationToken -> Task)
    (builder: Builders.ContainerBuilder)
    =
    builder.WithStartupCallback(startupCallback)

  /// <summary>
  /// Builds the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  let build (builder: Builders.ContainerBuilder) = builder.Build()

type ContainerBuilder() =

  member _.Zero _ = Builders.ContainerBuilder()

  member _.Yield _ = Builders.ContainerBuilder()

  /// <summary>
  /// Sets the Docker image, which is used to create the Testcontainer instances.
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="image">The Docker image.</param>
  [<CustomOperation "imageName">]
  member _.imageName(builder: Builders.ContainerBuilder, image: string) =
    builder |> ContainerBuilder.withImageName image

  /// <summary>
  /// Sets the Docker image, which is used to create the Testcontainer instances.
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="image">The Docker image.</param>
  [<CustomOperation "image">]
  member _.image(builder: Builders.ContainerBuilder, image: IImage) =
    builder |> ContainerBuilder.withImage image

  /// <summary>
  /// Sets the image pull policy of the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="policy">The image pull policy</param>
  [<CustomOperation "imagePullPolicy">]
  member _.imagePullPolicy(builder: Builders.ContainerBuilder, policy: ImageInspectResponse -> bool) =
    builder |> ContainerBuilder.withImagePullPolicy policy

  /// <summary>
  /// Sets the name of the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="name">Testcontainer's name</param>
  [<CustomOperation "name">]
  member _.withName(builder: Builders.ContainerBuilder, name: string) =
    builder |> ContainerBuilder.withName name

  /// <summary>
  /// Sets the hostname of the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="hostname">Testcontainer's hostname</param>
  [<CustomOperation "hostname">]
  member _.hostname(builder: Builders.ContainerBuilder, hostname: string) =
    builder |> ContainerBuilder.withHostName hostname

  /// <summary>
  /// Sets the MAC address of the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="macAddress">Testcontainer's MAC address</param>
  [<CustomOperation "macAddress">]
  member _.macAddress(builder: Builders.ContainerBuilder, macAddress: string) =
    builder |> ContainerBuilder.withMacAddress macAddress

  /// <summary>
  /// Sets the working directory of the Testcontainer for the instruction sets
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="workingDirectory">Working directory</param>
  [<CustomOperation "workingDirectory">]
  member _.workingDirectory(builder: Builders.ContainerBuilder, workingDirectory: string) =
    builder |> ContainerBuilder.withWorkingDirectory workingDirectory

  /// <summary>
  /// Overrides the entrypoint of the Testcontainer to configure an executable
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="entrypoint">Entrypoint executable</param>
  [<CustomOperation "entrypoint">]
  member _.entrypoint(builder: Builders.ContainerBuilder, entrypoint: string) =
    builder |> ContainerBuilder.withEntryPoint entrypoint

  /// <summary>
  /// Overrides the command of the Testcontainer to provide defaults for executing
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="command">List of commands, "executable", "param1", "param2" or "param1", "param2""</param>
  [<CustomOperation "commands">]
  member _.commands(builder: Builders.ContainerBuilder, command: string[]) =
    builder |> ContainerBuilder.withCommand command

  /// <summary>
  /// Exports the environment variable in the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="name">Environment variable name</param>
  /// <param name="value">Environment variable value</param>
  [<CustomOperation "environment">]
  member _.environment(builder: Builders.ContainerBuilder, name: string, value: string) =
    builder |> ContainerBuilder.withEnvironment (name, value)

  /// <summary>
  /// Exports the environment variable in the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="environmentVars">Dictionary of environment variables</param>
  [<CustomOperation "environment'">]
  member _.withEnvironment(builder: Builders.ContainerBuilder, environmentVars: #IReadOnlyDictionary<string, string>) =
    builder |> ContainerBuilder.withEnvironmentVars environmentVars

  /// <summary>
  /// Sets the port of the Testcontainer to expose, without publishing the port to the host system’s interfaces
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="port">Port to expose</param>
  [<CustomOperation "exposedPortInt">]
  member _.exposedPortInt(builder: Builders.ContainerBuilder, port: int) =
    builder |> ContainerBuilder.withExposedPort port

  /// <summary>
  /// Sets the port of the Testcontainer to expose, without publishing the port to the host system’s interfaces
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="port">Port to expose</param>
  [<CustomOperation "exposedPortString">]
  member _.exposedPortString(builder: Builders.ContainerBuilder, port: string) =
    builder |> ContainerBuilder.withExposedPortString port

  /// <summary>
  /// Binds the port of the Testcontainer to the same port of the host machine
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="port">Port to bind between Testcontainer and host machine</param>
  /// <param name="assignRandomHostPort">True, Testcontainer will bind the port to a random host port, otherwise the host and container ports are the same</param>
  [<CustomOperation "portBinding">]
  member _.portBinding(builder: Builders.ContainerBuilder, port: int, ?assignRandomHostPort: bool) =
    builder |> ContainerBuilder.withPortBinding (port, assignRandomHostPort)

  /// <summary>
  /// Binds the port of the Testcontainer to the same port of the host machine
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="port">Port to bind between Testcontainer and host machine</param>
  /// <param name="assignRandomHostPort">True, Testcontainer will bind the port to a random host port, otherwise the host and container ports are the same</param>
  /// <remarks>Append /tcp|udp|sctp to change the protocol e.g. "53/udp". Default: tcp</remarks>
  [<CustomOperation "portBindingString">]
  member _.portBindingString(builder: Builders.ContainerBuilder, port: string, ?assignRandomHostPort: bool) =
    builder |> ContainerBuilder.withPortBindingString (port, assignRandomHostPort)

  /// <summary>
  /// Binds the port of the Testcontainer to the same port of the host machine
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="hostPort">Port of the host machine</param>
  /// <param name="containerPort">Port of the Testcontainer</param>
  [<CustomOperation "portContainerBinding">]
  member _.portContainerBinding(builder: Builders.ContainerBuilder, hostPort: int, containerPort: int) =
    builder |> ContainerBuilder.withPortContainerBinding (hostPort, containerPort)

  /// <summary>
  /// Binds the port of the Testcontainer to the same port of the host machine
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="hostPort">Port of the host machine</param>
  /// <param name="containerPort">Port of the test container</param>
  /// <remarks>Append /tcp|udp|sctp to change the protocol e.g. "53/udp". Default: tcp</remarks>
  [<CustomOperation "portContainerBindingString">]
  member _.portContainerBindingString(builder: Builders.ContainerBuilder, hostPort: string, containerPort: string) =
    builder
    |> ContainerBuilder.withPortContainerBindingString (hostPort, containerPort)

  /// <summary>
  /// Sets the resource mapping of the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="source">Source of the resource mapping</param>
  /// <param name="destination">Destination of the resource mapping</param
  [<CustomOperation "resourceMappingString">]
  member _.resourceMappingString(builder: Builders.ContainerBuilder, source: string, destination: string) =
    builder |> ContainerBuilder.withResourceMappingString (source, destination)

  /// <summary>
  /// Sets the resource mapping of the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="source">Source of the resource mapping</param>
  /// <param name="destination">Destination of the resource mapping</param
  [<CustomOperation "resourceMappingBytes">]
  member _.resourceMappingBytes(builder: Builders.ContainerBuilder, source: byte[], destination: string) =
    builder |> ContainerBuilder.withResourceMappingBytes (source, destination)

  /// <summary>
  /// Sets the mount of the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="resourceMapping">Resource mapping</param>
  [<CustomOperation "resourceMapping">]
  member _.resourceMapping(builder: Builders.ContainerBuilder, resourceMapping: IResourceMapping) =
    builder |> ContainerBuilder.withResourceMapping resourceMapping

  /// <summary>
  /// Sets the mount of the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="mount">Mount</param>
  [<CustomOperation "mount">]
  member _.mount(builder: Builders.ContainerBuilder, mount: IMount) =
    builder |> ContainerBuilder.withMount mount

  /// <summary>
  /// Sets the bind mount of the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="source">Source of the bind mount</param>
  [<CustomOperation "bindMount">]
  member _.bindMount(builder: Builders.ContainerBuilder, source: string, destination: string, ?accessMode: AccessMode) =
    builder |> ContainerBuilder.withBindMount (source, destination, accessMode)

  /// <summary>
  /// Sets the volume mount of the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="source">Source of the volume mount</param>
  /// <param name="destination">Destination of the volume mount</param>
  /// <param name="accessMode">Access mode of the volume mount</param
  [<CustomOperation "volumeMountString">]
  member _.volumeMountString
    (builder: Builders.ContainerBuilder, source: string, destination: string, ?accessMode: AccessMode)
    =
    builder
    |> ContainerBuilder.withVolumeMountString (source, destination, accessMode)

  /// <summary>
  /// Sets the volume mount of the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="source">Source of the volume mount</param>
  /// <param name="destination">Destination of the volume mount</param>
  /// <param name="accessMode">Access mode of the volume mount</param
  [<CustomOperation "volumeMount">]
  member _.volumeMount
    (builder: Builders.ContainerBuilder, source: IVolume, destination: string, ?accessMode: AccessMode)
    =
    builder |> ContainerBuilder.withVolumeMount (source, destination, accessMode)

  /// <summary>
  /// Sets the tmpfs mount of the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="destination">Destination of the tmpfs mount</param>
  /// <param name="accessMode">Access mode of the tmpfs mount</param
  [<CustomOperation "tmpfsMount">]
  member _.tmpfsMount(builder: Builders.ContainerBuilder, destination: string, ?accessMode: AccessMode) =
    builder |> ContainerBuilder.withTmpfsMount (destination, accessMode)

  /// <summary>
  /// Sets the network of the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="name">Network name</param
  [<CustomOperation "networkString">]
  member _.networkString(builder: Builders.ContainerBuilder, name: string) =
    builder |> ContainerBuilder.withNetworkString name

  /// <summary>
  /// Sets the network of the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="network">Network</param
  [<CustomOperation "network">]
  member _.network(builder: Builders.ContainerBuilder, network: INetwork) =
    builder |> ContainerBuilder.withNetwork network

  /// <summary>
  /// Sets the network aliases of the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="aliases">Network aliases</param
  [<CustomOperation "networkAliases">]
  member _.networkAliases(builder: Builders.ContainerBuilder, aliases: string seq) =
    builder |> ContainerBuilder.withNetworkAliases aliases

  /// <summary>
  /// Sets the auto remove of the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="autoRemove">True, Testcontainer will be removed after the Testcontainer is stopped</param
  [<CustomOperation "autoRemove">]
  member _.autoRemove(builder: Builders.ContainerBuilder, ?autoRemove: bool) =
    builder |> ContainerBuilder.withAutoRemove (defaultArg autoRemove true)

  /// <summary>
  /// Sets the privileged of the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="privileged">True, Testcontainer will be privileged</param
  [<CustomOperation "privileged">]
  member _.privileged(builder: Builders.ContainerBuilder, privileged: bool) =
    builder |> ContainerBuilder.withPrivileged privileged

  /// <summary>
  /// Sets the wait strategy of the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="waitStrategy">Wait strategy</param
  [<CustomOperation "waitStrategy">]
  member _.waitStrategy(builder: Builders.ContainerBuilder, waitStrategy: IWaitForContainerOS) =
    builder |> ContainerBuilder.withWaitStrategy waitStrategy

  /// <summary>
  /// Sets the create parameter modifier of the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="parameterModifier">Create parameter modifier</param
  [<CustomOperation "createParameterModifier">]
  member _.createParameterModifier
    (builder: Builders.ContainerBuilder, parameterModifier: CreateContainerParameters -> unit)
    =
    builder |> ContainerBuilder.withCreateParameterModifier parameterModifier

  /// <summary>
  /// Sets the startup callback of the Testcontainer
  /// </summary>
  /// <param name="builder">A configured instance of ContainerBuilder</param>
  /// <param name="startupCallback">Startup callback</param
  [<CustomOperation "startupCallback">]
  member _.startupCallback
    (builder: Builders.ContainerBuilder, startupCallback: IContainer -> CancellationToken -> Task)
    =
    builder |> ContainerBuilder.withStartupCallback startupCallback

let container = ContainerBuilder()
