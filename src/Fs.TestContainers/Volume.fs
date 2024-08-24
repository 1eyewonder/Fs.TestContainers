module Fs.TestContainers.Volume

open DotNet.Testcontainers

[<RequireQualifiedAccess>]
module VolumeBuilder =

  /// <summary>
  /// Sets the name of the Docker volume
  /// </summary>
  /// <param name="name">Docker volume name</param>
  /// <param name="builder">Docker volume being built</param>
  let withName name (builder: Builders.VolumeBuilder) = builder.WithName(name)

  /// <summary>
  /// Builds the Docker volume
  /// </summary>
  /// <param name="builder">Docker volume being built</param>
  let Build (builder: Builders.VolumeBuilder) = builder.Build()

type VolumeBuilder() =

  member _.Zero _ = Builders.VolumeBuilder()

  member _.Yield _ = Builders.VolumeBuilder()

  /// <summary>
  /// Sets the name of the Docker volume
  /// </summary>
  /// <param name="volume">Docker volume being built</param>
  /// <param name="name">Docker volume name</param>
  [<CustomOperation("name")>]
  member _.name(builder: Builders.VolumeBuilder, name: string) = builder.WithName(name)

let volume = VolumeBuilder()

module Volume =

  open DotNet.Testcontainers.Volumes

  let create (volume: IVolume) = volume.CreateAsync()

  let delete (volume: IVolume) = volume.DeleteAsync()
