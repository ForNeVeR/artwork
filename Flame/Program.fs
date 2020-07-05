open System
open System.Drawing.Imaging
open System.IO
open System.Drawing

let private printUsage() =
    printfn "Arguments: <resolution> <cycles> <output>"

let private gradations = 30

let private createModel resolution =
    Array2D.init resolution resolution (fun _ y ->
        if y = 0
        then gradations - 1
        else 0
    )

let private processStep model =
    Array2D.mapi (fun x y current ->
        if y = 0 then current
        else
            let below = Array2D.get model x (y - 1)
            Math.Clamp(below - 1, 0, gradations - 1)
    ) model

let private palette = [|
     let mutable color = Color.Black
     let increment = Byte.MaxValue / (byte gradations)
     for _ in 1..(gradations - 1) do
         color <- Color.FromArgb(int <| color.R + increment, int color.G, int color.B)
         color
     Color.Red
|]

let private saveImage model (output: Stream) =
    use bitmap = new Bitmap(Array2D.length1 model, Array2D.length2 model)
    Array2D.iteri (fun x y g ->
        bitmap.SetPixel(x, bitmap.Height - y - 1, palette.[g])
    ) model
    bitmap.Save(output, ImageFormat.Png)

let private generateImage resolution cycles output =
    let mutable model = createModel resolution
    for i in 1..cycles do
        model <- processStep model
    saveImage model output

[<EntryPoint>]
let main: string[] -> int = function
| [| resolution; cycles; output |] ->
    let resolution = int resolution
    let cycles = int cycles
    use output = new FileStream(output, FileMode.Create)
    generateImage resolution cycles output
    0
| _ -> printUsage(); 1
