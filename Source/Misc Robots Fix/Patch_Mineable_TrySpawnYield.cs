using HarmonyLib;
using RimWorld;
using System;
using System.Reflection;
using Verse;

namespace Misc_Robots_Fix
{
    class MineablePatch
    {

        [HarmonyPatch(typeof(Mineable))]
        [HarmonyPatch("TrySpawnYield")]
        static class TrySpawnYield
        {
            private static Pawn tempPawn;

            static TrySpawnYield()
            {
                tempPawn = new Pawn();
                tempPawn.GetType().GetField("factionInt", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(tempPawn, Faction.OfPlayer);
                tempPawn.def = humanlikeDef();
            }

            private static ThingDef humanlikeDef()
            {
                RaceProperties humanlikeRace = new RaceProperties();
                humanlikeRace.intelligence = Intelligence.Humanlike;
                ThingDef def = new ThingDef();
                def.race = humanlikeRace;
                return def;
            }

            public static bool Prefix(MethodBase __originalMethod, Mineable __instance, Map map, float yieldChance, bool moteOnWaste, Pawn pawn)
            {
                if (pawn != null)
                {
                    if (!pawn.IsColonist)
                    {
                        if (pawn.ThingID.StartsWith("RPP_Bot_"))
                        {
                            __originalMethod.Invoke(__instance, new object[] { map, yieldChance, moteOnWaste, tempPawn });
                            return false;
                        }                        
                    }
                }
                return true;
            }
        }
    }
}
