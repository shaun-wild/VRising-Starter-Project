using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppSystem;
using Unity.Entities;
using VampireCommandFramework;
using VampireCommandFramework.Breadstone;
using Exception = System.Exception;

namespace VRisingStarterProject;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
#if SERVER
[BepInDependency("gg.deca.VampireCommandFramework")]
[Reloadable]
#else
[BepInDependency("xyz.molenzwiebel.wetstone")]
[Wetstone.API.Reloadable]
#endif
public class Plugin : BasePlugin
{
    public static ManualLogSource Logger;
    private static World _serverWorld;
    private static EntityManager _em => Server.EntityManager;
    private Harmony _harmony;


    public static World Server
    {
        get
        {
            if (_serverWorld != null) return _serverWorld;
            _serverWorld = GetWorld("Server") ?? throw new Exception($"There is no Server world (yet). Did you install this mod on the Server?");
            return _serverWorld;
        }
    }

    public override void Load()
    {
#if SERVER
        CommandRegistry.RegisterAll();
        Log.LogInfo("Loaded Server plugin!");
#else
        Log.LogInfo("Loaded Client Plugin!");
#endif
        Logger = Log;
        Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
    }

    public override bool Unload()
    {
#if SERVER
        CommandRegistry.UnregisterAssembly();
#endif
        Log.LogInfo($"Unloading harmony: {_harmony}");
        _harmony.UnpatchSelf();
        return true;
    }

    private static World GetWorld(string name)
    {
        foreach (var world in World.s_AllWorlds)
            if (world.Name == name)
                return world;

        return null;
    }
    
    #if SERVER
    [Command("test")]
    public void TestCommand(ChatCommandContext ctx) {
        ctx.Reply("This is a test!");
    }
    #endif

    public Type GetType(System.Type type)
    {
        return Type.GetType(type.AssemblyQualifiedName);
    }
}
