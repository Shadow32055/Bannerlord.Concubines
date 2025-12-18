using BetterCore.Utils;
using Concubines.Extensions;
using Concubines.Models;
using HarmonyLib;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace Concubines.Patches {
    [HarmonyPatch(typeof(ConversationHelper), nameof(ConversationHelper.GetHeroRelationToHeroTextShort))]
    internal class EncyclopediaLabelPatch {
        [HarmonyPostfix]
        private static void Postfix(ref string __result, Hero queriedHero, Hero baseHero, bool uppercaseFirst) {
            Hero? concubineTo = queriedHero.ConcubineOf();
            if (concubineTo != null && concubineTo == baseHero) {
                __result = new TextObject("{=EncyclopediaConcubineTag}Concubine").ToString();
                return;
            }

            ConcubineList? data = queriedHero.IsParamour();
            if (data != null && data.Concubines.Keys.Contains(baseHero)) {
                __result = new TextObject("{=EncyclopediaParamourTag}Paramour").ToString();
                return;
            }
        }
    }

    internal class EncyclopediaPagePatch {
        public static void Postfix(EncyclopediaHeroPageVM __instance) {
			try {

				Hero hero = (Hero)AccessTools.Field(typeof(EncyclopediaHeroPageVM), "_hero").GetValue(__instance);
				MBBindingList<EncyclopediaFamilyMemberVM> family = (MBBindingList<EncyclopediaFamilyMemberVM>)AccessTools.Field(typeof(EncyclopediaHeroPageVM), "_family").GetValue(__instance);

				if (hero == null || family == null) {
					return;
				}

				//NotifyHelper.WriteMessage("EncyclopediaPagePatch processing hero: " + hero.Name, MsgType.Alert);

				Hero? concubineOf = hero.ConcubineOf();
				if (concubineOf != null && !family.Any(x => x.Hero == concubineOf)) {
					family.Add(new EncyclopediaFamilyMemberVM(concubineOf, hero));
				}

				ConcubineList? data = hero.IsParamour();
				if (data != null) {
					foreach (Hero concubine in data.Concubines.Keys) {
						if (concubine != null && !family.Any(x => x.Hero == concubine)) {
							family.Add(new EncyclopediaFamilyMemberVM(concubine, hero));
						}
					}
				}
			}
			catch (System.Exception ex) {
				NotifyHelper.WriteError("EncyclopediaPagePatch", "Exception in Postfix: " + ex.Message + " | " + ex.StackTrace);
			}
		}
    }
}
