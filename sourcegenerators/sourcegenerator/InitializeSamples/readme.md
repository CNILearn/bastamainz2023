# Code Generator Initializer

> Scenario: solutions need customer-specific packages that need to be initialized (e.g. configured with the DI container). With the current solution, the customer-specific configuration is loaded using reflection. From about 80 services configured with the DI container before the reflection-based initialization, after using the reflection-based intialization, more than 500 services are configured. This reflection-based approach takes startup time, and also delays finding issues. If libraries are missing, this is not detected during compilation time.

The described approach can be changed by

* adding the NuGet packages at compile time to the project
* using a source generator 

## Adding NuGet Packages at Compile Time

The sample solution contains a project *InitializeLib* that contains the `IInitialize` interface.

Prepare a folder with Nuget Packages that contains classes implementing the `IInitialize` interface (see the projects *One*, and *Two* in the solution). The `IInitialize` interface is declared in the *InitializeLib* project.

Add NuGet packages to the `customer1` folder.

Run the `addpackages.ps1` PowerShell script to add the NuGet packages to the client project.

## Using a Source Generator

### Install the CreateJsonInitalizer dotnet tool

The CreateJsonInitializer is a dotnet tool that uses reflection to find all the classes implementing the `IInitialize` interface. It then creates a json file with the name `results.json` containing all the types found in the directory to.

> dotnet tool install -g CreateJsonInitalizer --prerelase
