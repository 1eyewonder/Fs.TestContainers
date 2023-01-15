module Fs.TestContainers.Container

open System
open System.Collections.Generic
open System.Threading
open System.Threading.Tasks
open Docker.DotNet.Models
open DotNet.Testcontainers.Builders
open DotNet.Testcontainers.Configurations
open DotNet.Testcontainers.Containers
open DotNet.Testcontainers.Images
open DotNet.Testcontainers.Networks
open DotNet.Testcontainers.Volumes

type ContainerBuilder () =
    inherit Abstract.AbstractBuilder<ITestcontainersBuilder<ITestcontainersContainer>> ()

    member _.Zero _ = TestcontainersBuilder<ITestcontainersContainer>()

    member _.Yield _ = TestcontainersBuilder<ITestcontainersContainer>()

    /// <summary>
    /// Sets the module configuration of the Testcontainer to override custom properties
    /// </summary>
    /// <param name="c">A configured instance of ITestcontainersBuilder</param>
    /// <param name="moduleConfiguration">Module configuration action</param>
    [<CustomOperation "configure">]
    member _.configureContainer(c : ITestcontainersBuilder<ITestcontainersContainer>,
                                moduleConfiguration : Action<ITestcontainersContainer>) =
        c.ConfigureContainer(moduleConfiguration)

    /// <summary>
    /// Sets the Docker image, which is used to create the Testcontainer instances.
    /// </summary>
    /// <param name="c">A configured instance of ITestcontainersBuilder</param>
    /// <param name="image">The Docker image.</param>
    [<CustomOperation "image">]
    member _.withImage(c : ITestcontainersBuilder<ITestcontainersContainer>, image : string) =
        c.WithImage(image)

    /// <summary>
    /// Sets the Docker image, which is used to create the Testcontainer instances.
    /// </summary>
    /// <param name="c">A configured instance of ITestcontainersBuilder</param>
    /// <param name="image">The Docker image.</param>
    [<CustomOperation "image'">]
    member _.withImage2(c : ITestcontainersBuilder<ITestcontainersContainer>, image : IDockerImage) =
        c.WithImage(image)

    /// <summary>
    /// Sets the image pull policy of the Testcontainer
    /// </summary>
    /// <param name="c">A configured instance of ITestcontainersBuilder</param>
    /// <param name="policy">The image pull policy</param>
    [<CustomOperation "imagePull">]
    member _.withImagePullPolicy(c : ITestcontainersBuilder<ITestcontainersContainer>,
                                 policy: ImagesListResponse -> bool) =
        c.WithImagePullPolicy(Func<_,_>(policy))

    /// <summary>
    /// Sets the name of the Testcontainer
    /// </summary>
    /// <param name="c">A configured instance of ITestcontainersBuilder</param>
    /// <param name="name">Testcontainer's name</param>
    [<CustomOperation "name">]
    member _.withName(c : ITestcontainersBuilder<ITestcontainersContainer>, name : string) =
        c.WithName(name)

    /// <summary>
    /// Sets the hostname of the Testcontainer
    /// </summary>
    /// <param name="c">A configured instance of ITestcontainersBuilder</param>
    /// <param name="hostname">Testcontainer's hostname</param>
    [<CustomOperation "hostname">]
    member _.withHostname(c : ITestcontainersBuilder<ITestcontainersContainer>, hostname : string) =
        c.WithHostname(hostname)

    /// <summary>
    /// Sets the MAC address of the Testcontainer
    /// </summary>
    /// <param name="c">A configured instance of ITestcontainersBuilder</param>
    /// <param name="macAddress">Testcontainer's MAC address</param>
    [<CustomOperation "macAddress">]
    member _.withMacAddress(c : ITestcontainersBuilder<ITestcontainersContainer>, macAddress : string) =
        c.WithMacAddress(macAddress)

    /// <summary>
    /// Sets the working directory of the Testcontainer for the instruction sets
    /// </summary>
    /// <param name="c">A configured instance of ITestcontainersBuilder</param>
    /// <param name="workingDirectory">Working directory</param>
    [<CustomOperation "workingDirectory">]
    member _.withWorkingDirectory(c : ITestcontainersBuilder<ITestcontainersContainer>, workingDirectory : string) =
        c.WithWorkingDirectory(workingDirectory)

    /// <summary>
    /// Overrides the entrypoint of the Testcontainer to configure an executable
    /// </summary>
    /// <param name="c">A configured instance of ITestcontainersBuilder</param>
    /// <param name="entrypoint">Entrypoint executable</param>
    [<CustomOperation "entrypoint">]
    member _.withEntrypoint(c : ITestcontainersBuilder<ITestcontainersContainer>, entrypoint : string) =
        c.WithEntrypoint(entrypoint)

    /// <summary>
    /// Overrides the command of the Testcontainer to provide defaults for executing
    /// </summary>
    /// <param name="c">A configured instance of ITestcontainersBuilder</param>
    /// <param name="command">List of commands, "executable", "param1", "param2" or "param1", "param2""</param>
    [<CustomOperation "commands">]
    member _.withCommands(c : ITestcontainersBuilder<ITestcontainersContainer>, command : string[]) =
        c.WithCommand(command)

    /// <summary>
    /// Exports the environment variable in the Testcontainer
    /// </summary>
    /// <param name="c">A configured instance of ITestcontainersBuilder</param>
    /// <param name="name">Environment variable name</param>
    /// <param name="value">Environment variable value</param>
    [<CustomOperation "environment">]
    member _.withEnvironment(c : ITestcontainersBuilder<ITestcontainersContainer>, name : string, value : string) =
        c.WithEnvironment(name, value)

    /// <summary>
    /// Exports the environment variable in the Testcontainer
    /// </summary>
    /// <param name="c">A configured instance of ITestcontainersBuilder</param>
    /// <param name="environmentVars">Dictionary of environment variables</param>
    [<CustomOperation "environment'">]
    member _.withEnvironment(c : ITestcontainersBuilder<ITestcontainersContainer>, environmentVars : #IReadOnlyDictionary<string, string>) =
        c.WithEnvironment(environmentVars)

    /// <summary>
    /// Sets the port of the Testcontainer to expose, without publishing the port to the host system’s interfaces
    /// </summary>
    /// <param name="c">A configured instance of ITestcontainersBuilder</param>
    /// <param name="port">Port to expose</param>
    [<CustomOperation "exposedPort">]
    member _.withExposedPort(c : ITestcontainersBuilder<ITestcontainersContainer>, port : int) =
        c.WithExposedPort(port)

    /// <summary>
    /// Sets the port of the Testcontainer to expose, without publishing the port to the host system’s interfaces
    /// </summary>
    /// <param name="c">A configured instance of ITestcontainersBuilder</param>
    /// <param name="port">Port to expose</param>
    [<CustomOperation "exposedPort'">]
    member _.withExposedPort2(c : ITestcontainersBuilder<ITestcontainersContainer>, port : string) =
        c.WithExposedPort(port)

    /// <summary>
    /// Binds the port of the Testcontainer to the same port of the host machine
    /// </summary>
    /// <param name="c">A configured instance of ITestcontainersBuilder</param>
    /// <param name="port">Port to bind between Testcontainer and host machine</param>
    /// <param name="assignRandomHostPort">True, Testcontainer will bind the port to a random host port, otherwise the host and container ports are the same</param>
    [<CustomOperation "portBinding">]
    member _.withPortBinding(c : ITestcontainersBuilder<ITestcontainersContainer>, port : int, ?assignRandomHostPort : bool) =
        match assignRandomHostPort with
        | Some assignRandomHostPort -> c.WithPortBinding(port, assignRandomHostPort)
        | None -> c.WithPortBinding(port)

    /// <summary>
    /// Binds the port of the Testcontainer to the same port of the host machine
    /// </summary>
    /// <param name="c">A configured instance of ITestcontainersBuilder</param>
    /// <param name="hostPort">Port of the host machine</param>
    /// <param name="containerPort">Port of the Testcontainer</param>
    [<CustomOperation "portBinding'">]
    member _.withPortBinding2(c : ITestcontainersBuilder<ITestcontainersContainer>, hostPort : int, containerPort : int) =
        c.WithPortBinding(hostPort, containerPort)

    /// <summary>
    /// Binds the port of the Testcontainer to the same port of the host machine
    /// </summary>
    /// <param name="c">A configured instance of ITestcontainersBuilder</param>
    /// <param name="port">Port to bind between Testcontainer and host machine</param>
    /// <param name="assignRandomHostPort">True, Testcontainer will bind the port to a random host port, otherwise the host and container ports are the same</param>
    /// <remarks>Append /tcp|udp|sctp to change the protocol e.g. "53/udp". Default: tcp</remarks>
    [<CustomOperation "sPortBinding">]
    member _.withPortBinding3(c : ITestcontainersBuilder<ITestcontainersContainer>, port : string, ?assignRandomHostPort : bool) =
        match assignRandomHostPort with
        | Some assignRandomHostPort -> c.WithPortBinding(port, assignRandomHostPort)
        | None -> c.WithPortBinding(port)

    /// <summary>
    /// Binds the port of the Testcontainer to the same port of the host machine
    /// </summary>
    /// <param name="c">A configured instance of ITestcontainersBuilder</param>
    /// <param name="hostPort">Port of the host machine</param>
    /// <param name="containerPort">Port of the test container</param>
    /// <remarks>Append /tcp|udp|sctp to change the protocol e.g. "53/udp". Default: tcp</remarks>
    [<CustomOperation "sPortBinding'">]
    member _.withPortBinding4(c : ITestcontainersBuilder<ITestcontainersContainer>, hostPort : string, containerPort : string) =
        c.WithPortBinding(hostPort, containerPort)

    [<CustomOperation "resourceMapping">]
    member _.withResourceMapping(c : ITestcontainersBuilder<ITestcontainersContainer>, source : string, destination : string) =
        c.WithResourceMapping(source, destination)

    [<CustomOperation "resourceMapping'">]
    member _.withResourceMapping2(c : ITestcontainersBuilder<ITestcontainersContainer>, source : byte[], destination : string) =
        c.WithResourceMapping(source, destination)

    [<CustomOperation "resourceMapping''">]
    member _.withResourceMapping3(c : ITestcontainersBuilder<ITestcontainersContainer>, resourceMapping : IResourceMapping) =
        c.WithResourceMapping(resourceMapping)

    [<CustomOperation "mount">]
    member _.withMount(c : ITestcontainersBuilder<ITestcontainersContainer>, mount : IMount) =
        c.WithMount(mount)

    [<CustomOperation "bindMount">]
    member _.withBindMount(c : ITestcontainersBuilder<ITestcontainersContainer>, source : string, destination : string, ?accessMode : AccessMode) =
        match accessMode with
        | Some accessMode -> c.WithBindMount(source, destination, accessMode)
        | None -> c.WithBindMount(source, destination)

    [<CustomOperation "volumeMount">]
    member _.withVolumeMount(c : ITestcontainersBuilder<ITestcontainersContainer>, source : string, destination : string, ?accessMode : AccessMode) =
        match accessMode with
        | Some accessMode -> c.WithVolumeMount(source, destination, accessMode)
        | None -> c.WithVolumeMount(source, destination)

    [<CustomOperation "volumeMount'">]
    member _.withVolumeMount2(c : ITestcontainersBuilder<ITestcontainersContainer>, source : IDockerVolume, destination : string, ?accessMode : AccessMode) =
        match accessMode with
        | Some accessMode -> c.WithVolumeMount(source, destination, accessMode)
        | None -> c.WithVolumeMount(source, destination)

    [<CustomOperation "tmpfsMount">]
    member _.withTmpfsMount(c : ITestcontainersBuilder<ITestcontainersContainer>, destination : string, ?accessMode : AccessMode) =
        match accessMode with
        | Some accessMode -> c.WithTmpfsMount(destination, accessMode)
        | None -> c.WithTmpfsMount(destination)

    [<CustomOperation "network">]
    member _.withNetwork(c : ITestcontainersBuilder<ITestcontainersContainer>, source : string, destination : string) =
        c.WithNetwork(source, destination)

    [<CustomOperation "network'">]
    member _.withNetwork2(c : ITestcontainersBuilder<ITestcontainersContainer>, network : IDockerNetwork) =
        c.WithNetwork(network)

    [<CustomOperation "networkAliases">]
    member _.withNetworkAliases(c : ITestcontainersBuilder<ITestcontainersContainer>, aliases : string seq) =
        c.WithNetworkAliases(aliases)

    member _.withAutoRemove(c : ITestcontainersBuilder<ITestcontainersContainer>, autoRemove : bool) =
        c.WithAutoRemove(autoRemove)

    [<CustomOperation "privileged">]
    member _.withPrivileged(c : ITestcontainersBuilder<ITestcontainersContainer>, privileged : bool) =
        c.WithPrivileged(privileged)

    [<CustomOperation "waitStrategy">]
    member _.withWaitStrategy(c : ITestcontainersBuilder<ITestcontainersContainer>, waitStrategy : IWaitForContainerOS) =
        c.WithWaitStrategy(waitStrategy)

    [<CustomOperation "parameterModifier">]
    member _.withCreateContainerParametersModifier(c : ITestcontainersBuilder<ITestcontainersContainer>, parameterModifier : CreateContainerParameters -> unit) =
        c.WithCreateContainerParametersModifier(Action<_>(parameterModifier))

    [<CustomOperation "startupCallback">]
    member _.withStartupCallback(c : ITestcontainersBuilder<ITestcontainersContainer>, startupCallback : IRunningDockerContainer -> CancellationToken -> Task) =
        c.WithStartupCallback(Func<_,_,_>(startupCallback))

let container = ContainerBuilder()

/// <summary>
/// Builds the container with the given configuration
/// </summary>
/// <returns>FullName of the created image.</returns>
let build (container: ITestcontainersBuilder<ITestcontainersContainer>) =
    container.Build()
