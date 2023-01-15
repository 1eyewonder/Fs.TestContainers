module Fs.TestContainers.Abstract

open System
open DotNet.Testcontainers.Builders
open DotNet.Testcontainers.Configurations

type AbstractBuilder<'a> () =

    /// <summary>
    /// Sets the Docker API endpoint
    /// </summary>
    /// <param name="builder">The builder entity</param>
    /// <param name="endpoint">The Docker API endpoint</param>
    [<CustomOperation "endpoint">]
    member _.withDockerEndpoint(builder : IAbstractBuilder<'a>, endpoint : string) =
        builder.WithDockerEndpoint(endpoint)

    /// <summary>
    /// Sets the Docker API endpoint
    /// </summary>
    /// <param name="builder">The builder entity</param>
    /// <param name="endpoint">The Docker API endpoint</param>
    [<CustomOperation "endpoint'">]
    member _.withDockerEndpoint2(builder : IAbstractBuilder<'a>, endpoint : Uri) =
        builder.WithDockerEndpoint(endpoint)

    /// <summary>
    /// Sets the Docker API endpoint
    /// </summary>
    /// <param name="builder">The builder entity</param>
    /// <param name="endpoint">The Docker API endpoint authentication configuration</param>
    [<CustomOperation "endpointAuth">]
    member _.withDockerEndpoint3(builder : IAbstractBuilder<'a>, endpoint : IDockerEndpointAuthenticationConfiguration) =
        builder.WithDockerEndpoint(endpoint)

    /// <summary>
    /// If true, the ResourceReaper will remove the Docker resource automatically. Otherwise, the Docker resource will be kept.
    /// </summary>
    /// <param name="builder">The builder entity</param>
    /// <param name="cleanUp">Cleanup flag</param>
    [<CustomOperation "cleanUp">]
    member _.withCleanUp(builder : IAbstractBuilder<'a>, cleanUp : bool) =
        builder.WithCleanUp cleanUp

    /// <summary>
    /// Adds user-defined metadata to the Docker resource
    /// </summary>
    /// <param name="builder">The builder entity</param>
    /// <param name="name">Label name</param>
    /// <param name="value">Label value</param>
    [<CustomOperation "label">]
    member _.withLabel(builder : IAbstractBuilder<'a>, name, value) =
        builder.WithLabel(name, value)

    /// <summary>
    /// Sets the Resource Reaper session id
    /// </summary>
    /// <param name="builder">The builder entity</param>
    /// <param name="sessionId">The session id of the ResourceReaper instance</param>
    [<CustomOperation "sessionId">]
    member _.withResourceReaperSessionId(builder : IAbstractBuilder<'a>, sessionId : Guid) =
        builder.WithResourceReaperSessionId(sessionId)
