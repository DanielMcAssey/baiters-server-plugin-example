# Baiters Plugin Example
[![MyExamplePluginForBaiters](https://github.com/DanielMcAssey/baiters-server/actions/workflows/release.yml/badge.svg)](https://github.com/DanielMcAssey/baiters-server/actions/workflows/release.yml)

An example plugin for WebFishing Baiters Server, it shows you how to create it, its ready to go for publishing changes automatically to GitHub with GitHub Actions, and supports main, beta and alpha branches and releases


## Customisation
> [!NOTE]
> Below are the list of files you will likely want to change to make it your own

1. **src/MyExamplePlugin.cs** rename this file to your plugin name
2. **src/MyExamplePluginForBaiters.csproj** rename this file to your plugin name
3. **MyExamplePluginForBaiters.sln** rename this file to your plugin name, update the `csproj` file path to the one you changed in step 2
4. **.github/workflows/build.yaml** change the `PLUGIN_PROJECT` environment variable to the one you changed in step 2
5. **.github/workflows/release.yaml** change the `PLUGIN_PROJECT` environment variable to the one you changed in step 2, change the `PLUGIN_NAME` to your plugin name
6. **README.md** update this file and tell users how to they can use your plugin, I have included a boilerplate `Setup` section, if you want to use it, make sure you update the references, if you want to keep the status badge at the top, update the reference to your repository

Then you are ready to go! Use your favourite IDE, I recommend VS Code 


## Install Plugin
> [!IMPORTANT]
> If you want to use this plugin, you will need to enable plugins on Baiters Server if you havent already

1. Download the latest plugin `MyExamplePluginForBaiters-vX.X.X.zip` from the releases page
2. Unzip the plugin to a folder
3. Copy the contents of that folder to the Baiters Server `plugins` folder
4. Restart the Server, if the plugin was installed successfully, the server will list this plugin
