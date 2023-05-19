
#  Welcome to SharpEdif!

By Kostya

[Discord](https://www.discord.com/invite/wsH3KNtvvJ)
| Table of Contents |Description  |
|--|--|
| [What is SharpEdif?](https://github.com/CTFAK/SharpEdif#what-is-sharpedif) | A short description of what SharpEdif is and what it's used for. |
| [Installation and Usage](https://github.com/CTFAK/SharpEdif#installation-and-usage)  | How to clone, code, compile, and release an extension using SharpEdif.  |
| [To-Do List](https://github.com/CTFAK/SharpEdif#to-do-list) | What needs to be done to mark SharpEdif as stable. |
| [Full Credits](https://github.com/CTFAK/SharpEdif#full-credits) | Everyone who helped make SharpEdif a reality. |

#  What is SharpEdif?

SharpEdif is a Clickteam Fusion 2.5 Unicode Extension SDK created for use with C#. Developed by Kostya, SharpEdif makes it possible to code your own fully operational Clickteam Fusion 2.5 extensions in C# to be used with with the Clickteam Fusion 2.5 game engine.

With SharpEdif's attribution and custom build system, it's easy for anyone to make an extension compatible with Clickteam Fusion 2.5 with little knowledge of C# or the Edif SDKs.

#  Installation and Usage

##  Dependencies

SharpEdif requires the [.NET 4.7.2 Development Kit](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net472).
SharpEdif's Builder requires [.NET 7.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0).

After running the installer, you may proceed.

##  Cloning the repo with Visual Studio 2022

Make sure you have [Visual Studio 2022](https://visualstudio.microsoft.com/) installed and open.

On the GitHub branch, click `Code` and copy the HTTPS URL.

In Visual Studio 2022, under `Get started`, click `Clone a repository`, then paste the HTTPS URL from earlier. Input your desired path and press `Clone`.

##  Creating a simple extension with SharpEdif

Once you have SharpEdif loaded into Visual Studio 2022, in your solution browser, under SharpEdif, go into Extension.cs.

From here you can change the extension's name, author, copyright, description, and website. But under those you can see 1 example Condition, 2 example Actions, and 1 example Expression.

Once you've modified the template to your hearts content, you can move onto compiling.

##  Compiling your extension

Right click the SharpEdif.Builder solution on the right and press `Build Solution` or do it through the keybind `Control + Shift + B`, and do the same for the SharpEdif solution. Then, right click the solution once again and press `Open Folder in File Explorer`.

In the File Explorer go to the `CompiledExtension`folder, from there you should find your extension with the `.mfx` file extension. Copy this extension into your `Clickteam Fusion/Extensions/Unicode` folder and, you should be able to use your extension without problems!

##  Releasing your extension

Clickteam Fusion extensions also have a secondary build option called "Runtime" which needs to be released alongside the one we compiled earlier.

To do this, change the build type to `Runtime`, and then compile your extension again. In the `CompiledExtension` folder, copy the new `.mfx` file into your `Clickteam Fusion/Data/Runtime/Unicode` folder.

Now, for release you're going to create a new folder anywhere you'd like (Desktop, CompiledExtension folder, etc.), then inside that folder, you're going to create the file structure shown below.

 - Extensions
	 - Unicode
		 - YourNormalExtension.mfx
 - Data
	 - Runtime
		 - Unicode
			 - YourRuntimeExtension.mfx

Zip that folder, and it's ready to be released for anybody's use!

#  To-Do List

|%| Task |Description
|--|--|--|
| 50% | Properties | Allow editing of the extension's properties panel. |
| 20% | Implement All Structures | Implement all missing/empty structures from the original SDK. 
| 0% | Parameter Names | Allowing naming of parameters. |
| 0% | Condition Events | Implement fire condition events (The green conditions). |
| 0% | Rendering API | Expose the Rendering API. ||

#  Full Credits

|Name| Credit for... |
|--|--|
| [Clickteam](https://www.clickteam.com/) | Creation of the original SDK and Clickteam Fusion 2.5. |
| [Kostya](https://github.com/1987kostya1) | Developer of SharpEdif. |
| [Yunivers](https://github.com/AITYunivers) | Minor help implementing structures, and testing. |
| [SortaCore](https://github.com/SortaCore) | Developer of DarkEdif, which was used for a small amount of reference. |

SharpEdif is licensed under [GNU Affero General Public License v3.0](https://github.com/CTFAK/SharpEdif/blob/master/LICENSE).

Last Updated May 19th, 2023.
