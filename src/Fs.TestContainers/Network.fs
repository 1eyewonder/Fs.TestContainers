module Fs.TestContainers.Network

open DotNet.Testcontainers
open DotNet.Testcontainers.Configurations

[<RequireQualifiedAccess>]
module NetworkBuilder =

  /// <summary>
  /// Sets the name of the Docker network
  /// </summary>
  /// <param name="name">Docker network name</param>
  /// <param name="builder">Docker network being built</param>
  let withName (name: string) (builder: Builders.NetworkBuilder) = builder.WithName(name)

  /// <summary>
  /// Sets the driver of the Docker network
  /// </summary>
  /// <param name="driver">The driver</param>
  /// <param name="builder">Docker network being built</param>
  let withDriver (driver: NetworkDriver) (builder: Builders.NetworkBuilder) = builder.WithDriver(driver)

  /// <summary>
  /// Sets the option of the Docker network
  /// </summary>
  /// <param name="key">The option name</param>
  /// <param name="value">The option value</param>
  let withOption (key: string, value: string) (builder: Builders.NetworkBuilder) = builder.WithOption(key, value)

  /// <summary>
  /// Builds the Docker network
  /// </summary>
  /// <param name="builder">Docker network being built</param>
  let build (builder: Builders.NetworkBuilder) = builder.Build()

type NetworkBuilder() =

  /// <summary>
  /// Sets the name of the Docker network
  /// </summary>
  /// <param name="network">Docker network being built</param>
  /// <param name="name">Docker network name</param>
  [<CustomOperation("name")>]
  member _.name(builder: Builders.NetworkBuilder, name: string) = builder |> NetworkBuilder.withName name

  /// <summary>
  /// Sets the driver of the Docker network
  /// </summary>
  /// <param name="network">Docker network being built</param>
  /// <param name="driver">The driver</param>
  [<CustomOperation("driver")>]
  member _.driver(builder: Builders.NetworkBuilder, driver: NetworkDriver) =
    builder |> NetworkBuilder.withDriver driver

  /// <summary>
  /// Sets the option of the Docker network
  /// </summary>
  /// <param name="network">Docker network being built</param>
  /// <param name="key">The option name</param>
  /// <param name="value">The option value</param>
  [<CustomOperation("option")>]
  member _.option(builder: Builders.NetworkBuilder, key: string, value: string) =
    builder |> NetworkBuilder.withOption (key, value)

let network = NetworkBuilder()
