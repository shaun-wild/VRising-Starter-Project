# V-Rising Starter Project

A super simple bare-bones project with [Vampire Command Framework](https://github.com/decaprime/VampireCommandFramework/) and [Wetstone](https://github.com/molenzwiebel/Wetstone) pre-setup.

## Getting started

### Rename project

- Rename `VRisingStarterProject.csproj` to your project name.
- perform a project-wide search and replace 
for `VRisingStarterProject` and replace it with your project name.
- Rename the root folder of this project to your project name.

### Setup Auto-Configuration

This project is automatically configured to use VCF on the server and Wetstone on the client, you can decide
which of these you would like to use by modifying `YourProjectName.csproj`.

1. Set the `<ModType>` property, this can either be `Server` or `Client`.
2. Update the `<VRisingBasePath>` property, this should point to your V-Rising base path.

### Final Steps

Update this README to better suit your project, if you're open sourcing then make sure to include a nice README!

## Contributing

If you would like to modify this starter project, then fork this repository and open a pull-request.

Made with ♥ by Shaun#3052.

Join the V-Rising [modding discord](https://vrisingmods.com/discord)? 

