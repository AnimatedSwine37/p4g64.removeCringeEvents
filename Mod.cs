using BF.File.Emulator.Interfaces;
using CriFs.V2.Hook.Interfaces;
using p4g64.removeCringeEvents.Configuration;
using p4g64.removeCringeEvents.NuGet.templates.defaultPlus;
using p4g64.removeCringeEvents.Template;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using System.IO;

namespace p4g64.removeCringeEvents;
/// <summary>
/// Your mod logic goes here.
/// </summary>
public class Mod : ModBase // <= Do not Remove.
{
    /// <summary>
    /// Provides access to the mod loader API.
    /// </summary>
    private readonly IModLoader _modLoader;

    /// <summary>
    /// Provides access to the Reloaded.Hooks API.
    /// </summary>
    /// <remarks>This is null if you remove dependency on Reloaded.SharedLib.Hooks in your mod.</remarks>
    private readonly IReloadedHooks? _hooks;

    /// <summary>
    /// Provides access to the Reloaded logger.
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    /// Entry point into the mod, instance that created this class.
    /// </summary>
    private readonly IMod _owner;

    /// <summary>
    /// Provides access to this mod's configuration.
    /// </summary>
    private Config _configuration;

    /// <summary>
    /// The configuration of the currently executing mod.
    /// </summary>
    private readonly IModConfig _modConfig;

    public Mod(ModContext context)
    {
        _modLoader = context.ModLoader;
        _hooks = context.Hooks;
        _logger = context.Logger;
        _owner = context.Owner;
        _configuration = context.Configuration;
        _modConfig = context.ModConfig;

        var bfEmulatorController = _modLoader.GetController<IBfEmulator>();
        if (bfEmulatorController == null || !bfEmulatorController.TryGetTarget(out var bfEmulator))
        {
            Utils.LogError($"Unable to get controller for BF Emulator, stuff won't work :(");
            return;
        }

        var criController = _modLoader.GetController<ICriFsRedirectorApi>();
        if (criController == null || !criController.TryGetTarget(out var criFs))
        {
            Utils.LogError($"Unable to get controller for Cri-FS, stuff won't work :(");
            return;
        }
            
        var modDir = _modLoader.GetDirectoryForModId(_modConfig.ModId);

        if(_configuration.RemoveCamping)
        {
            bfEmulator.AddFile(Path.Combine(modDir, "BF", "Camping", "scheduler_06.flow"), "scheduler_06.flow");
            criFs.AddProbingPath(Path.Combine("CPK", "Camping"));
        }

        if(_configuration.RemoveHotsprings)
        {
            bfEmulator.AddFile(Path.Combine(modDir, "BF", "Hotsprings", "scheduler_10.flow"), "scheduler_10.flow");
        }

        if (_configuration.RemoveGroupDate)
        {
            bfEmulator.AddFile(Path.Combine(modDir, "BF", "GroupDate", "scheduler_10.flow"), "scheduler_10.flow");
        }

        if (_configuration.RemoveCrossDressing)
        {
            bfEmulator.AddFile(Path.Combine(modDir, "BF", "CrossDressing", "scheduler_10.flow"), "scheduler_10.flow");
        }

        if(_configuration.RemoveHotsprings || _configuration.RemoveGroupDate || _configuration.RemoveCrossDressing)
        {
            criFs.AddProbingPath(Path.Combine("CPK", "CultureFestival"));
        }
    }

    #region Standard Overrides
    public override void ConfigurationUpdated(Config configuration)
    {
        // Apply settings from configuration.
        // ... your code here.
        _configuration = configuration;
        _logger.WriteLine($"[{_modConfig.ModId}] Config Updated: Applying");
    }
    #endregion

    #region For Exports, Serialization etc.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Mod() { }
#pragma warning restore CS8618
    #endregion
}