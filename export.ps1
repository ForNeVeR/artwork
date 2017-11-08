param (
    $Background = 'black',
    $Image = "$PSScriptRoot/adept-unhooded.svg",
    $Output = "$PSScriptRoot/adept-unhooded.png"
)

magick convert -resize 512x512 -depth 8 -background $Background $Image $Output