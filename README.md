# MapCreator
Generate Dark Age of Camelot (DAoC) maps using the game client.

## Requirements
You need to install [.NET 4.0: Visual C++ Redistributable](http://www.microsoft.com/en-us/download/details.aspx?id=30679) for 
x64 or x86 to run Magick.NET which is required to get any result.

### Please note
- The map rendering is **very** expensive, produces a **high load** of CPU and uses a lot **memory**! This depends on the map size!
- It is recommended to use the map dimensions of 2^x, this means: 512, 1024, 2048, 4096. DAoC itself uses 2^16.
- Don't try to create maps larger than 8096 pixels, the data is too much to handle....

### Changelog
**08/23/2015**
- Fixed crash when fixtures have wrong coordinates (thanks to HunabKu for reporting!)

**02/16/2014**
- bug fix in image replacement
- added image replacement examples for relic temples
- found some trees that are nor marked as trees (please report if you find more)
- changed all opacity to transparency fields to be more clear
- added option to set tree transparency per map
- reset settings, too many changes to convert them[/list]

**Older changes**
- added fixtures rendering
- added tree rendering
- full rewrite of the bound-calculation
- full rewrite of the water-calculation
- UI improvements
- fixed wrong calculations
- optimized default settings
- rewrote bounds generator, now there will be no more flood fills
- optimized river rendering
- add lava and a texture to water[/list]

My special thanks go to Schaf and Metty who helped me out a lot!
