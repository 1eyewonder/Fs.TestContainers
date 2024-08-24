namespace Fs.TestContainers.Tests

open Expecto

module ExpectoTemplate =

  [<EntryPoint>]
  let main argv =
    SayTests.tests |> runTestsWithCLIArgs Seq.empty argv
