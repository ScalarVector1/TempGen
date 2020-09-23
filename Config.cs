using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace TempGen
{
    public enum WorldSize
    {
        small,
        medium,
        large,
        custom
    }

    public enum Evil
    {
        corruption,
        crimson,
        random
    }

    class Config : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("World Size")]
        [Tooltip("The size of your quick-generated world")]
        public WorldSize size;

        [Label("World Evil")]
        [Tooltip("The evil of your quick-generated world")]
        public Evil evil;

        [Label("Expert")]
        [Tooltip("If your quick-generated world should be in expert mode")]
        public bool expert;

        [Label("Seed")]
        [Tooltip("The seed your world will generate with. leave blank for a random seed.")]
        public string seed = "";

        [Label("Custom World Width")]
        [Tooltip("The width of your world if you use a custom size. Dont set it to something stupid and expect the game to not crash.")]
        [Range(0, int.MaxValue)]
        public int customWidth = 1000;

        [Label("Custom World Height")]
        [Tooltip("The height of your world if you use a custom size. Dont set it to something stupid and expect the game to not crash.")]
        [Range(0, int.MaxValue)]
        public int customHeight = 1000;

        [Label("Blacklisted Passes")]
        [Tooltip("Add the name of a generation pass here to prevent it from running in your quick world. Examples: `Traps`, `Lakes`, `Starlight River Vitric Desert`")]
        public List<string> blackList = new List<string>();
    }
}
