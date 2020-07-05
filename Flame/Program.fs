﻿open System
open System.Drawing.Imaging
open System.IO
open System.Drawing

let private printUsage() =
    printfn "Arguments: <resolution> <cycles> <output>"

let private palette = Array.map Color.FromArgb [|
     0xFF070707
     0xFF1F0707
     0xFF2F0F07
     0xFF470F07
     0xFF571707
     0xFF671F07
     0xFF771F07
     0xFF8F2707
     0xFF9F2F07
     0xFFAF3F07
     0xFFBF4707
     0xFFC74707
     0xFFDF4F07
     0xFFDF5707
     0xFFDF5707
     0xFFD75F07
     0xFFD7670F
     0xFFCF6F0F
     0xFFCF770F
     0xFFCF7F0F
     0xFFCF8717
     0xFFC78717
     0xFFC78F17
     0xFFC7971F
     0xFFBF9F1F
     0xFFBF9F1F
     0xFFBFA727
     0xFFBFA727
     0xFFBFAF2F
     0xFFB7AF2F
     0xFFB7B72F
     0xFFB7B737
     0xFFCFCF6F
     0xFFDFDF9F
     0xFFEFEFC7
     0xFFFFFFFF
|]

let private gradations = palette.Length

let private createModel resolution =
    Array2D.init resolution resolution (fun _ y ->
        if y = 0
        then gradations - 1
        else 0
    )

let private decreasePossibility = 0.3

let private processStep (rng: Random) model =
    Array2D.mapi (fun x y current ->
        if y = 0 then current
        else
            let below = Array2D.get model x (y - 1)
            let newValue = if rng.NextDouble() >= 1.0 - decreasePossibility then below - 1 else below
            Math.Clamp(newValue, 0, gradations - 1)
    ) model

let private saveImage model (output: Stream) =
    use bitmap = new Bitmap(Array2D.length1 model, Array2D.length2 model)
    Array2D.iteri (fun x y g ->
        bitmap.SetPixel(x, bitmap.Height - y - 1, palette.[g])
    ) model
    bitmap.Save(output, ImageFormat.Png)

let private generateImage rng resolution cycles output =
    let mutable model = createModel resolution
    for i in 1..cycles do
        model <- processStep rng model
    saveImage model output

[<EntryPoint>]
let main: string[] -> int = function
| [| resolution; cycles; output |] ->
    let resolution = int resolution
    let cycles = int cycles
    use output = new FileStream(output, FileMode.Create)
    let rng = Random()
    generateImage rng resolution cycles output
    0
| _ -> printUsage(); 1
