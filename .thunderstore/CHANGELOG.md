### v1.2.0

#### Main
- Added a search bar to the mod list and options list
- Adjusted the widths of the mod list, option list and info panels slightly

#### API
- Added support for setting info panel contents via the API
  - For an example of this, see [`Assets/Scripts/Plugin.cs`](https://github.com/kestrel-straftat/mod-menu/blob/master/Assets/Scripts/Plugin.cs) as per usual
  - TLDR: `OptionListContext` now exposes `SetInfoPanelContents`, `ClearInfoPanelContents` and `ShowInfoPanelResetButton`
  - You can show content in the info panel on hover by adding a listener to the `OnItemHovered` event on your `OptionListItem`

#### Other
- Realized ive been doing version numbers wrong this whole time. oops

### v1.1.5

- Fixed slider options not initializing correctly if their bounds did not include 0

### v1.1.4

- Fixed the mod menu not initializing properly in a rare case

### v1.1.3

- Added an API for mod developers to add items to and customise their mod's settings page!
  - see the readme for a quick walkthrough of its usage
- The mod now functions properly on platforms using vulkan (i.e. linux native)

### v1.0.3

- Added a shake animation to options when setting them fails

### v1.0.2

- Minor code refactoring
- For mod developers: Mod menu now supports custom implementations of `ConfigEntryBase`. if you use those. for some reason.

### v1.0.1

- Fixed the default mod description and icon sometimes not loading correctly

### v1.0.0

- Initial release