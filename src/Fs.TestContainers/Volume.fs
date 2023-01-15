module Fs.TestContainers.Volume

open DotNet.Testcontainers.Builders

type VolumeBuilder () =
    inherit Abstract.AbstractBuilder<ITestcontainersVolumeBuilder> ()

    /// <summary>
    /// Sets the name of the Docker volume
    /// </summary>
    /// <param name="volume">Docker volume being built</param>
    /// <param name="name">Docker volume name</param>
    [<CustomOperation "name">]
    member _.withName(volume: ITestcontainersVolumeBuilder, name: string) = volume.WithName(name)

let volume = VolumeBuilder ()

let build (volume: ITestcontainersVolumeBuilder) = volume.Build()


