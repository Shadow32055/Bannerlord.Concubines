using Concubines.Extensions;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.GameComponents;

namespace Concubines.Patches {
    [HarmonyPatch(typeof(MarriageOfferCampaignBehavior), "ConsiderMarriageForPlayerClanMember")]
    internal class MarriageOfferPatch {
        [HarmonyPrefix]
        private static bool Prefix(ref bool __result, Hero playerClanHero) {
            if (playerClanHero.ConcubineOf() != null) {
                __result = false;
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(DefaultMarriageModel), nameof(DefaultMarriageModel.IsSuitableForMarriage))]
    internal class MarriageModelPatch {
        [HarmonyPrefix]
        private static bool Prefix(ref bool __result, Hero maidenOrSuitor) {
            if (maidenOrSuitor.ConcubineOf() != null) {
                __result = false;
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(MarriageAction), "ApplyInternal")]
    internal class MarriagePatch {
        [HarmonyPrefix]
        private static bool Prefix(Hero firstHero, Hero secondHero) {
            if (firstHero.ConcubineOf() != null || secondHero.ConcubineOf() != null) {
                return false;
            }
            return true;
        }
    }
}
