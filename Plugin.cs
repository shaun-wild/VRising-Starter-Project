using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using Unity.Entities;
using Exception = System.Exception;

#if VCF
using VampireCommandFramework;
using VampireCommandFramework.Breadstone;
#endif

#if HARMONY
using HarmonyLib;
#endif

namespace VRisingStarterProject;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]

#if VCF
[BepInDependency("gg.deca.VampireCommandFramework")]
[Reloadable]
#endif

#if WETSTONE
[BepInDependency("xyz.molenzwiebel.wetstone")]
[Wetstone.API.Reloadable]
#endif

public class Plugin : BasePlugin
{
    public static ManualLogSource Logger;
    private static World _serverWorld;
    private static EntityManager _em => Server.EntityManager;

#if HARMONY
    private Harmony _harmony;
#endif

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
#if VCF
        CommandRegistry.RegisterAll();
#endif
        Logger = Log;
        Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
#if HARMONY
        _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
#endif
    }

    public override bool Unload()
    {
#if VCF
        CommandRegistry.UnregisterAssembly();
#endif
#if HARMONY
                _harmony.UnpatchSelf();
#endif
        return true;
    }

    private static World GetWorld(string name)
    {
        foreach (var world in World.s_AllWorlds)
            if (world.Name == name)
                return world;

        return null;
    }

#if VCF
    [Command("test")]
    public void TestCommand(ChatCommandContext ctx) {
        ctx.Reply("This is a test!");
    }
#endif
}