using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace TempGen
{
    class SpecialWorldgenWorld : ModWorld
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            var config = ModContent.GetInstance<Config>();

            foreach(string str in config.blackList)
            {
                if (tasks.Any(n => n.Name == str))
                    tasks.Remove(tasks.FirstOrDefault(n => n.Name == str));
            }
        }
    }
}
