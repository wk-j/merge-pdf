open iTextSharp.text
open iTextSharp.text.pdf
open System.IO

let merge (files: string array) (output: string) =
    do
        use document = new Document()
        use fileStream = new FileStream(output, FileMode.OpenOrCreate);
        use copy = new PdfSmartCopy(document, fileStream)

        document.Open()

        for item in files do
            use reader = new PdfReader(item)
            copy.AddDocument(reader)
            copy.FreeReader(reader)
            reader.Close()

        document.Close()

[<EntryPoint>]
let main argv =
    let allFiles = argv |> Array.take (argv.Length - 1)
    let output = argv |> Array.last

    merge allFiles output
    0
