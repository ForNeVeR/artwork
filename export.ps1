param (
    $Backgrounds = @('black', 'white', 'none'),
    $Images = "$PSScriptRoot/*.svg",
    $Output = "$PSScriptRoot/png",
    $Size = '1024x1024'
)

$ErrorActionPreference = 'Stop'

if (!(Test-Path $Output -PathType Container)) {
    New-Item -ItemType Directory $Output
}

Get-ChildItem $Images | ForEach-Object {
    $image = $_.FullName
    $Backgrounds | ForEach-Object {
        $background = $_
        $fileName = [IO.Path]::GetFileNameWithoutExtension($image)
        $outputPath = "$Output/$fileName-$background.png"
        Write-Output "Converting $([IO.Path]::GetFileName($image)) to $([IO.Path]::GetFileName($outputPath))..."
        magick convert -resize $Size -depth 8 -background $background $image $outputPath
        if (!$?) {
            throw "magick returned error code: $LASTEXITCODE"
        }
    }
}
