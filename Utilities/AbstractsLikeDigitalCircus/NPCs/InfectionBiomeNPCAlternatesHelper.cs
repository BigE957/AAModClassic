using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs
{
    public abstract class BiomeConvertableNPC : ModNPC
    {
        public struct BiomeData(string name = "Default", byte priority = 0, Func<Player, bool> isActive = null)
        {
            public string Name = name;
            public byte Priority = priority;
            public Func<Player, bool> IsActive = isActive ?? ((_) => true);
        }

        public static readonly List<BiomeData> Biomes = [];

        public static readonly Dictionary<int, Dictionary<string, Asset<Texture2D>>> BiomeTextures = [];

        public static void AddCrossModBiome(string name, byte priority, Func<Player, bool> isActive) => Biomes.Add(new(name, priority, isActive));

        public static void AddCrossModConvertableNPCTextures(int type, string biome, Asset<Texture2D> texture) => BiomeTextures[type].Add(biome, texture);

        public abstract string AssetPath { get; }

        public virtual bool SeperateBiomeFolders => false;
        
        public string BiomeType = "Default";

        public Texture2D GetCurrentTexture() => BiomeTextures[Type][BiomeType].Value;

        public override void Load()
        {
            if (Biomes.Count == 0)
            {
                Biomes.Add(new("Default"));
                Biomes.Add(new("Corruption", 1, (p) => p.ZoneCorrupt));
                Biomes.Add(new("Crimson", 1, (p) => p.ZoneCrimson));
                Biomes.Add(new("Inferno", 2, (p) => p.GetModPlayer<AAPlayer>().ZoneMire));
                Biomes.Add(new("Mire", 2, (p) => p.GetModPlayer<AAPlayer>().ZoneInferno));
                Biomes.Add(new("Void", 2, (p) => p.GetModPlayer<AAPlayer>().ZoneVoid));
                Biomes.Add(new("Hallow", 3, (p) => p.ZoneHallow));
            }
        }

        public override void SetStaticDefaults()
        {
            BiomeTextures.Add(Type, []);
            foreach (var biome in Biomes)
            {
                string name;
                if (biome.Name == "Default")
                    name = Name;
                else
                    name = Name + "_" + biome.Name;

                Asset<Texture2D> texture;
                if (SeperateBiomeFolders)
                {
                    if (!ModContent.RequestIfExists<Texture2D>(AssetPath + biome.Name + "/" + name, out texture))
                        texture = ModContent.Request<Texture2D>(AssetPath + "Default/" + Name);
                }
                else
                {
                    if (!ModContent.RequestIfExists<Texture2D>(AssetPath + name, out texture))
                        texture = ModContent.Request<Texture2D>(AssetPath + Name);
                }
                BiomeTextures[Type].Add(biome.Name, texture);
            }
        }

        public override void SetDefaults()
        {
            SetBiome();
        }

        public void SetBiome()
        {
            BiomeData myBiome = new("Default");
            int index = NPC.FindClosestPlayer();
            if (index == -1)
                return;
            Player p = Main.player[index];
            foreach (var biome in Biomes)
            {
                if (biome.Priority > myBiome.Priority && biome.IsActive.Invoke(p))
                {
                    Main.NewText(biome.Name);
                    myBiome = biome;
                }
            }
            BiomeType = myBiome.Name;
        }
    }
}
