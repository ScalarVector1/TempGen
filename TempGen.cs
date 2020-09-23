using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using Terraria.IO;
using System;
using System.IO;

namespace TempGen
{
	public class TempGen : Mod
	{
        bool genning;

        public override void Load()
        {
            On.Terraria.Main.UpdateMenu += UpdateMe;
            On.Terraria.WorldGen.PlacePot += PlacePotRight;
        }

        private bool PlacePotRight(On.Terraria.WorldGen.orig_PlacePot orig, int x, int y, ushort type, int style)
        {
            if (genning)
                return false;

            else
                return orig(x, y, type, style);
        }

        private void UpdateMe(On.Terraria.Main.orig_UpdateMenu orig)
        {
            if (Main.mouseMiddle && !genning)
            {
                genning = true;
                PutMeInAWorld();
            }
            
            if(genning && !WorldGen.gen && Main.menuMode != 888)
            {
                using (FileStream f = File.Create(ModLoader.ModPath + "/TempWorld"))
                {
                    BinaryWriter w = new BinaryWriter(f);
                    WorldFile.SaveWorld_Version2(w);
                }

                var temp = new PlayerFileData(ModLoader.ModPath + "/TempPlayer", false);
                temp.Name = "Temporary Player";
                temp.Player = new Player();
                temp.Player.name = "Temporary Player";
                temp.Metadata = FileMetadata.FromCurrentSettings(FileType.Player);

                Main.player[0] = temp.Player;

                Main.ActivePlayerFileData = temp;

                WorldGen.playWorld();
                genning = false;
            }

            orig();
        }

        public void PutMeInAWorld()
        {
            var config = ModContent.GetInstance<Config>();

            WorldGen.gen = true;
            WorldFileData temp = new WorldFileData(ModLoader.ModPath + "/TempWorld", false);
            temp.SetAsActive();

            if (config.size == WorldSize.small)
            {
                temp.SetWorldSize(4200, 1200);
                Main.maxTilesX = 4200;
                Main.maxTilesY = 1200;
                Main.tile = new Tile[4200, 1200];
            }

            if (config.size == WorldSize.medium)
            {
                temp.SetWorldSize(6400, 1800);
                Main.maxTilesX = 6400;
                Main.maxTilesY = 1800;
                Main.tile = new Tile[6400, 1800];
            }

            if (config.size == WorldSize.large)
            {
                temp.SetWorldSize(8400, 2400);
                Main.maxTilesX = 8400;
                Main.maxTilesY = 2400;
                Main.tile = new Tile[8400, 2400];
            }

            if (config.size == WorldSize.custom)
            {
                temp.SetWorldSize(config.customWidth, config.customHeight);
                Main.maxTilesX = config.customWidth;
                Main.maxTilesY = config.customHeight;
                Main.tile = new Tile[config.customWidth, config.customHeight];
            }

            if (config.evil == Evil.corruption) temp.HasCrimson = false;
            if (config.evil == Evil.crimson) temp.HasCrimson = true;

            temp.IsExpertMode = config.expert;
            Main.expertMode = config.expert;

            if (config.seed == "" || config.seed is null) temp.SetSeedToRandom();
            else temp.SetSeed(config.seed);

            Main.ActiveWorldFileData = temp;

            WorldGen.CreateNewWorld();
        }
	}
}