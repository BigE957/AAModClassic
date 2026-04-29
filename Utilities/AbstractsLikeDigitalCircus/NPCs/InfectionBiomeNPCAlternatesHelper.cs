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
    public static class InfectionBiomeNPCAlternatesHelper
    {
        public static void SetProperFramingForBiome_Horizontal(this NPC npc, int biomeType)
        {
            npc.frame.Width = TextureAssets.Npc[npc.type].Value.Width / 6;
            npc.frame.X = npc.frame.Width * biomeType;
        }
    }

    public abstract class BiomeConvertableNPC : ModNPC
    {
        public struct BiomeData(string name = "Default", byte priority = 0, Func<Player, bool> isActive = null)
        {
            public string Name = name;
            public byte Priority = priority;
            public Func<Player, bool> IsActive = isActive ?? ((_) => true);
        }

        public static readonly List<BiomeData> Biomes =
        [
            new(),
            new("Corruption", 1, (p) => p.ZoneCorrupt),
            new("Crimson", 1, (p) => p.ZoneCrimson),
            new("Inferno", 2, (p) => p.GetModPlayer<AAPlayer>().ZoneMire),
            new("Mire", 2, (p) => p.GetModPlayer<AAPlayer>().ZoneInferno),
            new("Void", 2, (p) => p.GetModPlayer<AAPlayer>().ZoneVoid),
            new("Hallow", 3, (p) => p.ZoneHallow)
        ];

        public static readonly Dictionary<int, Dictionary<string, Asset<Texture2D>>> BiomeTextures = [];

        public static void AddCrossModBiome(string name, byte priority, Func<Player, bool> isActive) => Biomes.Add(new(name, priority, isActive));

        public static void AddCrossModConvertableNPCTextures(int type, string biome, Asset<Texture2D> texture) => BiomeTextures[type].Add(biome, texture);

        public abstract string AssetPath { get; }
        
        public string BiomeType = "Default";

        public Texture2D GetCurrentTexture() => BiomeTextures[Type][BiomeType].Value;

        public override void Load()
        {
            BiomeTextures.Add(Type, []);
            foreach (var biome in Biomes)
            {
                string name;
                if (biome.Name == "Default")
                    name = Name;
                else
                    name = Name + "_" + biome.Name;

                if (!ModContent.RequestIfExists<Texture2D>(AssetPath + biome.Name + "/" + name, out Asset<Texture2D> texture))
                    texture = ModContent.Request<Texture2D>(AssetPath + "Default/" + name);
                BiomeTextures[Type].Add(biome.Name, texture);
            }
        }

        public override void OnSpawn(IEntitySource source)
        {
            Player p = Main.player[NPC.target];
            BiomeData myBiome = new();
            foreach (var biome in Biomes)
            {
                if (biome.Priority > myBiome.Priority && biome.IsActive.Invoke(p))
                    myBiome = biome;
            }
            BiomeType = myBiome.Name;
        }
    }
}
