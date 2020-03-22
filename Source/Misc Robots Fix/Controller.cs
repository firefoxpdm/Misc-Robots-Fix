using HarmonyLib;
using System.Reflection;
using Verse;

namespace Misc_Robots_Fix
{
    public class Controller : Mod
    {
        public Controller(ModContentPack content) : base(content)
        {
            Harmony harmony = new Harmony("com.firefox.miscrobotsfix");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
