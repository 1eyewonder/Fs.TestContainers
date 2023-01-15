module Fs.TestContainers.Network

open DotNet.Testcontainers.Builders
open DotNet.Testcontainers.Configurations

type NetworkBuilder () =
    inherit Abstract.AbstractBuilder<ITestcontainersNetworkBuilder> ()

    /// <summary>
    /// Sets the name of the Docker network
    /// </summary>
    /// <param name="network">Docker network being built</param>
    /// <param name="name">Docker network name</param>
    [<CustomOperation "name">]
    member _.withName(network: ITestcontainersNetworkBuilder, name: string) = network.WithName(name)

    /// <summary>
    /// Sets the driver of the Docker network
    /// </summary>
    /// <param name="network">Docker network being built</param>
    /// <param name="driver">The driver</param>
    [<CustomOperation "driver">]
    member _.withDriver(network : ITestcontainersNetworkBuilder, driver : NetworkDriver) = network.WithDriver(driver)

    /// <summary>
    /// Sets the option of the Docker network
    /// </summary>
    /// <param name="network">Docker network being built</param>
    /// <param name="key">The option name</param>
    /// <param name="value">The option value</param>
    [<CustomOperation "option">]
    member _.withOption(network : ITestcontainersNetworkBuilder, key : string, value : string) = network.WithOption(key, value)

let network = NetworkBuilder()

let build (network: ITestcontainersNetworkBuilder) = network.Build()
