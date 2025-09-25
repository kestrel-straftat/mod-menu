# Mod Menu

This mod adds a mods tab to the settings menu, which allows you to configure your mods ingame.

<img src="https://files.catbox.moe/j5nqm8.png" width="720" alt="The mod menu interface">

## Notes for mod developers

All types usable in BepInEx 5.4.21 ConfigEntries are supported.

This mod works much the same as BepInEx ConfigManager in that how options appear is defined by their acceptable values -
an `AcceptableValueRange` creates a slider, an `AcceptableValueList` creates a dropdown, etc. Known issues include:

- There is not currently an api to customise your mod's options (though this is planned!)
- `[Flags]` enums are not currently supported

## Manual Installation Instructions

_**(you should probably just use a mod manager like [r2modman](https://thunderstore.io/c/straftat/p/ebkr/r2modman/) or [gale](https://thunderstore.io/c/straftat/p/Kesomannen/GaleModManager/) though)**_

- Download and install [Bepinex 5.4.21](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.21) <br><small>(if you have no idea what the versions mean try BepInEx_x64_5.4.21.0 and it might work. maybe)</small>
- Once BepInEx is installed, extract the zip into the BepInEx/plugins folder in the game's root directory.

have fun :3

<img src ="https://files.catbox.moe/zqb82p.png" width="250" alt="kity">
