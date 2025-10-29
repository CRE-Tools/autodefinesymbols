<p align="center">
    <img width="100" height="100" src="/Documentation~/logos/1024x.png" align="center" />
</p>

<h1 align="center">UNITY - Auto Define Symbols</h1>

[About](#about) | [How to Install](#how-to-install) | <a href="/Documentation~/UserManual.md">Documentation</a> | <a href="/Documentation~/CONTRIBUTING.md">Contributing</a>

## About

AutoDefineSymbols is a utility for Unity projects that automatically manages Define Symbols based on customizable prebuild rules. It streamlines conditional compilation by ensuring that the correct symbols are always defined according to your project's context. The tool currently supports package-based rules and is designed with an interface for easy addition of more rule types in the future.

### Features
- Automatically defines and updates Unity Define Symbols before build time.
- Package Rule: Adds or removes symbols based on the presence of specific Unity packages.
- Extensible Rule System: Built with an interface to support additional rule types in future releases.
- Integrates seamlessly into Unity project workflows.
- Configuration via a simple prebuild rule context file.

## How to Install

- Unity -> Window -> Package Manager  
- Click "+" at the top left corner  
- Add package from git URL  
- Insert `https://github.com/CRE-Tools/autodefinesymbols.git`
- Add  
- Done

> [!NOTE]
> For bug reports and feature requests, please open an issue in the repository.

[Copyright (c) 2025 Centro de Realidade estendida - PUCPR](LICENSE.md)
