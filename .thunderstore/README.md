# Mod Menu

This mod adds a mods tab to the settings menu, which allows you to configure your mods ingame.

<img src="https://files.catbox.moe/j5nqm8.png" width="720" alt="The mod menu interface">

## For mod developers - notes and API reference 

All types usable in BepInEx 5.4.21 ConfigEntries are supported.

This mod works much the same as BepInEx ConfigManager in that how options appear is defined by their acceptable values -
an `AcceptableValueRange` creates a slider, an `AcceptableValueList` creates a dropdown, etc. Known issues include:

- `[Flags]` enums are not currently supported

### API reference

To get started, download the package as a zip and take `ModMenu.dll` and `ModMenu.xml` from the downloaded files. Reference
`ModMenu.dll` in your project, add `using ModMenu.Api` to files in which you wish to use the API and you should be good to go!

For example usage of the API, see [`Assets/Scripts/Plugin.cs` in the mod's source code](https://github.com/kestrel-straftat/mod-menu/blob/master/Assets/Scripts/Plugin.cs).

#### Hiding config entries

Calling `ModMenuCustomisation.HideEntry` with one of your mod's config entries will prevent ModMenu from generating an
option for it in your mod's page.

#### Explicitly setting mod description/icon

ModMenu attempts to get mod icons and descriptions from Thunderstore metadata. If it fails (i.e. your mod was installed
manually), your mod will get a default description and icon. The API allows you to set the description and icon so they will
always be available through `ModMenuCustomisation.SetPluginIcon` and `ModMenuCustomisation.SetPluginDescription`. Metadata set
through these also overrides any Thunderstore metadata that does get found.

#### Custom content builders

To add content to your mod page, the API allows you to provide an action that will be invoked while ModMenu is constructing
the page. To do this, call `ModMenuCustomisation.RegisterContentBuilder`. The action takes an `OptionListContext`, which provides
methods for easily constructing many premade option list items.

Below is a small example, you can find a full example in ModMenu's source code as mentioned above.

```csharp
ModMenuCustomisation.RegisterContentBuilder((OptionListContext context) => {
    context.AppendTextBox("A text box");

    bool checkboxValue = false;
    // second parameter is a getter, third parameter is a setter
    // these can do anything you want really - you aren't limited to
    // just mirroring a value.
    context.AppendCheckbox("A checkbox", () => checkboxValue,
        value => {
            checkboxValue = value;
        });

    context.PrependHeader("Prepending stuff (appears at the start of the list)");

    context.InsertHeader(5, "Inserting stuff (appears at the specified index in the list)");

    // you can also get the root transform to do whatever you like with
    Transform rootTransformOfList = context.Root;
});
```

Something worth keeping an eye on is the fact that the content builder _will only be run once_. ModMenu keeps track of the
options that have been instantiated and which mod they're for and only enables/disables them as needed to improve performance.

## Manual Installation Instructions

_**(you should probably just use a mod manager like [r2modman](https://thunderstore.io/c/straftat/p/ebkr/r2modman/) or [gale](https://thunderstore.io/c/straftat/p/Kesomannen/GaleModManager/) though)**_

- Download and install [Bepinex 5.4.21](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.21) <br><small>(if you have no idea what the versions mean try BepInEx_x64_5.4.21.0 and it might work. maybe)</small>
- Once BepInEx is installed, extract the zip into the BepInEx/plugins folder in the game's root directory.

have fun :3

<img src ="https://files.catbox.moe/zqb82p.png" width="250" alt="kity">
