namespace Core

open System

module Decode =

    type Dimension = (int * int)
    type Layer = string list
    type Image = {dimension:Dimension; layers:string list list}

    let parse (input: string) width height =
        let digits = input |> Seq.toList
        let layers =
            digits
            |> List.chunkBySize (width * height)
            |> List.map (fun layer -> layer |> List.chunkBySize width |> List.map (fun pixel -> pixel |> String.Concat))
        {dimension=(width, height); layers=layers}

    let computePixelColor higher lower =
        match (higher, lower) with
        | ('2', low) -> low
        | (high, _) -> high

    let update current underLayer =
        List.map2 (fun x y -> computePixelColor x y) current underLayer |> String.Concat

    let rec loop (list: string list) (current: string) =
        match list with
        | [] -> current
        | elem::rest -> loop rest (update (Seq.toList current) (Seq.toList elem))

    let mapLayers image =
        let layers = image.layers |> List.map (fun x -> x|> List.fold (fun r s -> r + s) "")
        let result = loop (layers |> List.skip 1) (layers |> List.head)
        result


