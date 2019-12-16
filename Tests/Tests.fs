module Tests

open System
open Xunit
open FsUnit
open Core.Decode

[<Fact>]
let ``Parse into 2 layers`` () =
    let input = "123456789012"
    let image = parse input 3 2
    image.layers |> should haveLength 2

[<Fact>]
let ``Parse generate width with 3 of size`` () =
    let input = "123456789012"
    let image = parse input 3 2
    image.layers |> List.head |> should haveLength 2
    image.layers |> List.last |> should haveLength 2

[<Fact>]
let ``Parse generate with correct values`` () =
    let input = "123456789012"
    let image = parse input 3 2
    image.layers |> List.concat |> should equivalent [123;456;789;012] 

