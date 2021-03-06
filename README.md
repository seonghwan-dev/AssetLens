# Asset Lens

## About **Asset Lens**
**Asset Lens** is a dependency tracking plugin for UnityEditor that provides additional information such as the number of usage.

This plugin is based on pre-cached complementary guid map to trace which asset has dependencies to specific asset. 
The pain point that mainly considered with Unity is that when we delete an asset, we don't know which asset is using it. 

## Requirements
- All assets must be serialized as force-text option in ProjectSetting/Editor

### Compatibility
We recommend the version `2021.2.0f1` or `latest` because of UI Toolkit(former UI Elements).  
But plug-in still work well in lower version.  
  
> Specifically tested semantic version is :  
  
<a href="unityhub://2019.4.32f1/"><img src="https://img.shields.io/badge/unity-2019.4f_LTS-blue.svg?logo=unity"/></a>
<a href="unityhub://2020.3.21f1/"><img src="https://img.shields.io/badge/unity-2020.3f_LTS-blue.svg?logo=unity"/></a>
<a href="unityhub://2021.1.27f1/"><img src="https://img.shields.io/badge/unity-2021.1f-blue.svg?logo=unity"/></a>
<a href="unityhub://2021.2.0f1/"><img src="https://img.shields.io/badge/unity-2021.2f-brightgreen.svg?logo=unity"/></a>
<a href="unityhub://2022.1.0a13/"><img src="https://img.shields.io/badge/unity-2022.1 alpha-red.svg?logo=unity"/></a>

<a href="https://codecov.io/gh/seonghwan-dev/AssetLens">
<img src="https://codecov.io/gh/seonghwan-dev/AssetLens/branch/main/graph/badge.svg?token=7ODSTUTX1G"/>
</a>
<a href="https://openupm.com/packages/com.calci.assetlens/">
<img src="https://img.shields.io/npm/v/com.calci.assetlens?label=openupm&registry_uri=https://package.openupm.com"/>
</a>
<a href="https://badge.fury.io/js/com.calci.assetlens">
<img src="https://badge.fury.io/js/com.calci.assetlens.svg" alt="npm version"/>
</a>

## Installation
<details><summary>Download with NPM (Unity Package Manager) </summary>
<p>

[![NPM](https://nodei.co/npm/com.calci.assetlens.png?compact=true)](https://npmjs.org/package/com.calci.assetlens)

Replace stable version at version definition in json `x.x.x`  
example) `"com.calci.assetlens": "0.4.2"`  
```json
{
    "dependencies": {
        "com.calci.assetlens": "x.x.x"
    }
}
```

```json
{
    "scopedRegistries": [
        {
            "name": "npm",
            "url": "https://registry.npmjs.org",
            "scopes": [
                "com.calci"
            ]
        }
    ]
}
```
</p>
</details>

<details><summary>Download with OpenUPM </summary>
<p>
  
[![openupm](https://img.shields.io/npm/v/com.calci.assetlens?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.calci.assetlens/)
```bash
openupm add com.calci.assetlens
```
</p>
  
</details>

<details>
<summary>GitHub Link</summary>
  
  <p>
    
```bash
https://github.com/seonghwan-dev/assetlens.git#upm
```
  </p>
  
</details>

## QuickStart
When you install plugin, indexing wizard will be shown.  

<p align="center">
<img width="512" alt="image" src="https://user-images.githubusercontent.com/79823287/147115098-4cbbf2bd-1a43-44ab-8447-ce2e88c2e25f.png">
</p>

1. Hit the bluish `generate` button and then wait for progress bar.  
2. When it finished, wizard will be close automatically. 
3. Select any asset you want to see which asset uses that.  
4. There is 2 options to show dependencies between assets.  
  4-1. hit `detail` button on top of the inspector. then editor window will be shown.  
  4-2. set mouse cursor to the asset in project view and hit right click and select context menu named `Find Reference In Project`.  
  
<details><summary>Show Option Description</summary>  
  
- `Indexes by GUID Regular Expression` : find dependencies by GUID Regex or EditorUtility.CollectDependencies.  
- `Trace scene object hierarchically` : WIP (not in feature currently)  
- `Inlcude subdirectories of Packages` : Include assets under Packages/ or not.  
- `Always open when you start a project` : if you want to see this wizard on startup again, disable this option.  
  </details>


## Fundamentals
- Create a cache file per a asset file, see also [RefData.cs]
- Detect asset changes from `AssetPostprocessor`, see also [AssetLensPostprocessor.cs]
- Detect an attempt to delete an asset from `AssetModificationProcessor`, see also [AssetLensModification.cs]

## Features
- Display asset usage count in inspector.
- Find References In Project

### Reference Viewer Window

|before initialize|after initialize|
|:---:|:---:|
|<img src="https://user-images.githubusercontent.com/79823287/134523257-28173dc7-4fd5-406e-8ac9-56b148debedb.png" width="460">|<img src="https://user-images.githubusercontent.com/79823287/134523437-166bf30b-ccdd-42ea-90ae-3084e0f013f6.png" width="460">|
|not available|available to trace dependencies|

#### Overview
<p align="center">
<img width="363" alt="image" src="https://user-images.githubusercontent.com/79823287/147112141-beeb3e1a-8959-4a0e-aef6-2c655d88168f.png">  
</p>

- `0.2.6` : Indexer version. This represent the which serializer indexes this asset.
- `(UnityEngine.GameObject)` : the type of selected asset. prefabs are displayed as GameObject.
- `Last Modified : 2021-12-22 PM 8:39:34` : last modified date time of asset. this information is not from cached data but file metadata.
- `Dependencies` : list up assets that this asset is using. `Cube 2` includes the material `MAT_Green` in MeshRenderer.
- `Used By` : list up assets that uses selected asset. if you delete the `Cube 2` then the instantiated prefab in `SampleScene` will be disconnected and displyed as missing.
---

### Inspector Lens
Displays the number of other resources using the selected asset.

<p align="center">
<img src="https://user-images.githubusercontent.com/79823287/139777116-25ed937e-2f69-421a-91a8-4ae426a311e4.png" width="460">
</p>

- Details : Open Reference Viewer as EditorWindow instantly.  
- Refresh : Reserialized cached reference data asset.  
- GUID : Displays the guid of selected asset. onClick events will copy guid to your clipboard.  

## Community
### Discord
[<p align="center"><img src="https://discordapp.com/api/guilds/889046470655893574/widget.png?style=banner2"></p>](https://discord.gg/h9WPFRNFBY)


## Roadmap
Not stable yet, but under development.

### Reference Viewer
[x] Reference View for Persistent Assets.  
[ ] Reference View for Scene Objects.  
[ ] Sortable Multi-Column viewer.  
[ ] Dependency graph for Scene Objects.  

### Inspector Lens.  
[x] Display how many assets are related to selected asset at the top of the inspector.  
[ ] Pop-up window of linked assets such as nested prefab inspector.  

### Safe Delete  
[x] Alert before the asset that is used by other asset will be deleted.  
[ ] (Experimental) Replace reference during delete asset. (Reference Replacer)  

### Build Lens  
[ ] Find the assets will be included in build  
[ ] Asset bundle, Addressable, Linked assets with scenes in build setting, resources.  

---
## Contributes
- Current Editor Version : `2021.2.0f1`    
- Fork and clone repository.  
- Edit sources and commit with conventional commits (prefer Commitizen)  
- Add unit test codes for new feature (Optional)
- Create PR.  

### Developer mode
Select menu in `Help/Asset Lens/Enter Debug Mode` or Add an scripting define symbol `DEBUG_ASSETLENS` at ProjectSettings/Player.

### Edit Languages
- Run `Tools/Asset Lens_DEV/Add New Language` to create a new localization profile.
- Run `Tools/Asset Lens_DEV/Update Language profiles` to add field after edit `Localize` class.

### Requirements
- commitizen - conventional commit log to generate changelog

### Tests
must be passed test in 2019.4, 2020.3, 2021.1, 2021.2

[RefData.cs]: https://github.com/seonghwan-dev/AssetLens/blob/main/Packages/com.calci.assetlens/Editor/Reference/Model/RefData.cs
[AssetLensPostprocessor.cs]: https://github.com/seonghwan-dev/AssetLens/blob/main/Packages/com.calci.assetlens/Editor/Reference/Callback/ReferencePostprocessor.cs
[AssetLensModification.cs]: https://github.com/seonghwan-dev/AssetLens/blob/main/Packages/com.calci.assetlens/Editor/Reference/Callback/ReferenceModification.cs
