## Build only:
- Run the `Stage` pipeline. The results will be placed in `/ThunderKit/Staging/` (relative to proj root)

## Build and launch game, without a mod manager:
- Set the second step of the `RebuildAndLaunch` pipeline to use the `Deploy` pipeline
- Run the `RebuildAndLaunch` pipeline
    
## Build and launch game, with a mod manager (or any setup with a nonstandard BepInEx location):
- Create a PathReference named `ManagerBepInExDir` containing the path to your BepInEx directory
- Set the second step of the `RebuildAndLaunch` pipeline to use the `DeployWithManager` pipeline
- Run the `RebuildAndLaunch` pipeline