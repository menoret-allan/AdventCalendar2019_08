namespace Core

open System

module Decode =

    type Dimension = (int * int)
    type Layer = string list
    type Image = {dimension:Dimension; layers:Layer list}

    let parse (input: string) width height =
        let digits = input |> Seq.toList
        let layers =
            digits
            |> List.chunkBySize (width * height)
            |> List.map (fun layer -> layer |> List.chunkBySize width |> List.map (fun pixel -> pixel |> String.Concat))
        {dimension=(width, height); layers=layers}




