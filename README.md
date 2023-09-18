# SQLiteUnity
Unity package for [SQLite](https://sqlite.org/).

Based on
- [precompiled SQLite binaries](https://sqlite.org/download.html) for Windows, Linux, macOS, Android
- `Mono.Data.Sqlite.dll`
  - can be found in the installation of the Unity Editor, for example `UnityEditor\2022.3.9f1\Editor\Data\MonoBleedingEdge\lib\mono\4.5\Mono.Data.Sqlite.dll`.

## Installation
- You can add a dependency to your `Packages/manifest.json` using a [Git URL](https://docs.unity3d.com/Manual/upm-git.html) in the following form:
  `"com.achimmihca.sqliteunity": "https://github.com/achimmihca/SQLiteUnity.git?path=SQLiteUnity/Packages/com.achimmihca.sqliteunity#v1.0.0"`
  - Note that `#v1.0.0` can be used to specify a tag or commit hash.

## How to use
See the [unit test](https://github.com/achimmihca/SQLiteUnity/blob/main/SQLiteUnity/Packages/com.achimmihca.sqliteunity/Tests/Editor/SQLiteTest.cs) for an example how to use SQLite in Unity.
