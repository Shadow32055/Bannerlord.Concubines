using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;

namespace Concubines.Settings {
    public class MCMSettings : AttributeGlobalSettings<MCMSettings> {


        // PLAYER

        [SettingPropertyInteger("Attraction Relation Needed", -200, 200, Order = 1, RequireRestart = false, HintText = "Lower number = easier to take concubines, bigger number = harder.")]
        [SettingPropertyGroup("Player")]
        public int AttractionRelationNeededPlayer { get; set; } = 80;

        // AI

        [SettingPropertyBool("Enable AI Concubines", Order = 1, RequireRestart = false, HintText = "Allow AI to be able to take concubines.")]
        [SettingPropertyGroup("AI")]
        public bool EnableAIConcubines { get; set; } = true;

        [SettingPropertyInteger("AI Attraction Relation Needed", -200, 200, Order = 2, RequireRestart = false, HintText = "Lower number = easier to take concubines, bigger number = harder.")]
        [SettingPropertyGroup("AI")]
        public int AttractionRelationNeededAI { get; set; } = 100;


        public override string Id { get { return base.GetType().Assembly.GetName().Name; } }
        public override string DisplayName { get { return base.GetType().Assembly.GetName().Name; } }
        public override string FolderName { get { return base.GetType().Assembly.GetName().Name; } }
        public override string FormatType { get; } = "xml";
        public bool LoadMCMConfigFile { get; set; } = true;
    }
}

