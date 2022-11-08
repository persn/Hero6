# [Build Instructions](https://github.com/LateStartStudio/Hero6/blob/master/docs/BUILD-INSTRUCTIONS.md)

## Prerequisites

* [git](https://git-scm.com/)
* [git-lfs](https://git-lfs.github.com/)
* [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet) ([Get the version listed here](https://github.com/LateStartStudio/Hero6/blob/master/src/global.json))

## Instructions

### Clone the project

If you have git and git-lfs installed and configured, simply cloning the project should be enough.

`git clone --recurse-submodules https://github.com/LateStartStudio/Hero6`

### Install dotnet tools

`dotnet tool restore`

You can then run any dotnet tools according to official [documentation](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools).

### Pre-build Hero6 Engine

The asset builder needs this.

`dotnet build ./src/Hero6.Engine.Desktop`

### Building, running, etc. the game

Hero6 is built using standard [dotnet CLI commands](https://docs.microsoft.com/en-us/dotnet/core/tools/?tabs=netcore2x).

#### Run game

`dotnet run --project ./src/Hero6.Desktop/Hero6.Desktop.csproj`

#### Run unit tests

`dotnet test ./src/Hero6.sln`

## Other things to know

### .editorconfig

Make sure your IDE/text editor is compatible with [.editorconfig](https://editorconfig.org/) to make sure you're using the same text editor rules as everyone else contributing.

### Editing nuget packages

We use [Nuget CPM](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management) for nuget packages.

### Editing MonoGame Content project

[Refer to the documentation](https://docs.monogame.net/articles/tools/tools.html)

### Unit test coverage

If you want to see unit test coverage run the script `./src/coverage.sh`, it will generate reports, to see open `./src/.coverage/html/index.html`.
