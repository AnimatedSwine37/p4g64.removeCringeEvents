using p4g64.removeCringeEvents.Template.Configuration;
using System.ComponentModel;

namespace p4g64.removeCringeEvents.Configuration;
public class Config : Configurable<Config>
{
    [DisplayName("Remove Camping Trip")]
    [Description("If enabled the entire camping trip will not happen.")]
    [DefaultValue(true)]
    public bool RemoveCamping { get; set; } = true;

    [DisplayName("Remove Hotsprings")]
    [Description("If enabled the events where the group go to the hotsprings will not happen.")]
    [DefaultValue(true)]
    public bool RemoveHotsprings { get; set; } = true;

    [DisplayName("Remove Group Date Cafe")]
    [Description("If enabled the group date cafe events during the culture festival will not happen.")]
    [DefaultValue(true)]
    public bool RemoveGroupDate { get; set; } = true;

    [DisplayName("Remove Cross-Dressing Pageant")]
    [Description("If enabled the cross-dressing pageant during the culture festival will not happen.")]
    [DefaultValue(true)]
    public bool RemoveCrossDressing { get; set; } = true;

    [DisplayName("Debug Mode")]
    [Description("Logs additional information to the console that is useful for debugging.")]
    [DefaultValue(false)]
    public bool DebugEnabled { get; set; } = false;
}

/// <summary>
/// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
/// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
/// </summary>
public class ConfiguratorMixin : ConfiguratorMixinBase
{
    // 
}