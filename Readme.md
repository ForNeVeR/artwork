ForNeVeR artwork
================

These're some images crafted by me.

## Exporting to PNG

To export the images to PNG, run the `export.ps1` script:

```console
$ powershell ./export.ps1
```

It will generate all the images with `black`, `white` and `none` backgrounds in
the `png` directory.

## Flame

There's a program to generate flame images written in style of DOOM
 (implementation partially based on [this article][fire-spreading-algo]).
 Execute with .NET 3.1+ SDK:

```
$ dotnet run -- 256 80 T:\Temp\flame.png
$ #  <resolution> <steps> <outfile>
```

## License [![logo-cc-by][]][cc-by-license]

The images in this repositories are licensed under a [Creative Commons
Attribution 4.0 International License][cc-by-license], with the exception of the
 `memes` directory.

The `memes` directory may contain copyrighted material the use of which has not
always been specifically authorized by the copyright owner. It is believed that
this constitutes a 'fair use' of any such copyrighted material as parody.

The code in this repository is licensed under the [MIT license][mit-license].

[cc-by-license]: https://creativecommons.org/licenses/by/4.0/
[fire-spreading-algo]: https://habr.com/ru/post/435122/
[mit-license]: ./License.md

[logo-cc-by]: https://i.creativecommons.org/l/by/4.0/80x15.png
