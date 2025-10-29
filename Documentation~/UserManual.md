> [!TIP]
> <a href="/Documentation~/UserManual.pdf">View this manual as PDF</a>

<h1 align="center">Auto Define Symbols - User Manual</h1>
<p align="right">v1.2.0</p>

### Contents
1. [Introduction](#introduction)
   - [Overview](#overview)
   - [Features](#features)
   - [Requirements](#requirements)
2. [Getting Started](#getting-started)
   - [Installation](#installation)
   - [Basic Setup](#basic-setup)
   - [Quick Start Guide](#quick-start-guide)
3. [Usage](#usage)
   - [Package-Based Rules](#package-based-rules)
   - [Using the Symbols in Code](#using-the-symbols-in-code)
4. [Advanced Usage](#advanced-usage)
   - [Creating Custom Rules](#creating-custom-rules)
   - [Best Practices](#best-practices)
5. [Troubleshooting](#troubleshooting)
   - [Common Issues](#common-issues)
   - [FAQ](#faq)

---
## Introduction
### Overview

Auto Define Symbols is a Unity development tool that automatically manages compiler directives based on your project's context. It simplifies conditional compilation by automatically defining symbols when certain conditions are met, such as when specific packages are present in your project.

### Features

- **Automatic Symbol Management**: Define symbols automatically based on project configuration
- **Package Detection**: Automatically detect and create symbols for installed Unity packages
- **Extensible Rule System**: Built with an interface to support custom rule types
- **Editor-Only**: No runtime overhead in builds
- **Simple Integration**: Works out of the box with minimal setup

### Requirements
- Unity 2019.3 or later

## Getting Started
### Installation

#### Via Package Manager (Recommended)

1. In Unity, open the Package Manager (Window > Package Manager)
2. Click the "+" button in the top-left corner
3. Select "Add package from git URL..."
4. Enter: `https://github.com/CRE-Tools/autodefinesymbols.git`
5. Click "Add"

#### Manual Installation

1. Clone or download this repository
2. Copy the `com.pucpr.autodefinesymbols` folder to your project's `Packages` folder
3. The package will be automatically detected and imported

### Basic Setup

1. After installation, the package will automatically create a settings file at:
   ```
   Assets/SymbolSettings/SymbolSettings.asset
   ```

### Quick Start Guide

1. Select the `SymbolSettings` asset in your project
2. In the Inspector, you'll see the available rule types:

**Rule Type: Package Rule**:
- Add your package names to the Package Rules list to automatically generate symbols for them if they are installed
- Use the symbols in your code with `#if` directives

## Usage
### Package-Based Rules

The package comes with built-in support for package-based symbol rules. When you add a package name to the configuration, it will automatically create a define symbol, if the package is installed, in the format:

```
PACKAGE_[PACKAGE_NAME_IN_UPPERCASE]
```

For example, if you add `com.unity.textmeshpro` to your package rules it will create the symbol `PACKAGE_COM.UNITY.TEXTMESHPRO` when the package is detected as installed in the project.

### Using the Symbols in Code

You can use the defined symbols in your code with standard C# preprocessor directives:

```csharp
#if PACKAGE_COM_UNITY_TEXTMESHPRO
    // This code will only compile if TextMeshPro is in the project
    using TMPro;
    
    public class MyTextHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text textComponent;
        // ...
    }
#else
    // Fallback code when TextMeshPro is not available
    public class MyTextHandler : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Text textComponent;
        // ...
    }
#endif
```

## Advanced Usage
### Creating Custom Rules

You can extend the system by creating custom rule types:

1. Create a new class that implements `ISymbolRule`
2. Implement the required methods to determine when your symbol should be defined
3. Register your rule in the `SO_SymbolConfig` class

Example custom rule implementation:

```csharp
using System.Collections.Generic;
using UnityEngine;

namespace YourNamespace
{
    [CreateAssetMenu(menuName = "Symbol Rules/Platform Rule")]
    public class PlatformSymbolRule : ScriptableObject, ISymbolRule
    {
        public RuntimePlatform targetPlatform;
        public string symbolName;

        public ConditionalSymbolData[] GetSymbolsAndConditionals()
        {
            return new[]
            {
                new ConditionalSymbolData(
                    symbolName,
                    Application.platform == targetPlatform
                )
            };
        }
    }
}
```

### Best Practices

1. **Use Clear Naming Conventions**: Make your symbols descriptive and follow the UPPERCASE_WITH_UNDERSCORES convention if you are implementing custom rules
2. **Group Related Symbols**: Keep related symbols together in your configuration
4. **Test Different Configurations**: Verify your code works with different symbol combinations
5. **Use Fallbacks**: Always provide fallback code for when symbols are not defined

## Troubleshooting
### Common Issues

1. **Symbols not updating**
   - Ensure the symbol reference is correct in symbol settings
   - Try clicking "Force Recompile" in the Symbol Settings inspector
   - Ensure the package is properly imported
   - Check for any compilation errors that might prevent the script from running

2. **Symbols not defined in build**
   - The package only affects the Unity Editor. For build-time symbols, use Player Settings

3. **Settings file not found**
   - The settings file should be at `Assets/SymbolSettings/SymbolSettings.asset`
   - If missing, close and reopen Unity or force recompile the project

### FAQ

**Q: Does this affect build size or performance?**
A: No, the package only runs in the Unity Editor and doesn't include any runtime code in builds.

**Q: Can I use this with my existing #define directives?**
A: Yes, this works alongside your existing directives.

**Q: How do I check if a symbol is defined in code?**
A: Use the standard C# preprocessor directives: `#if SYMBOL_NAME` and `#endif`

**Q: Can I use this with Unity Cloud Build?**
A: Yes, the package works with Unity Cloud Build. Just make sure to commit your SymbolSettings asset.
