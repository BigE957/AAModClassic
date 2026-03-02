using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic;
using AAModClassic.Globals;

namespace AAModClassic.Items.Materials
{
    public class Discordium : BaseAAItem
    {
        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Texture2D[] glowMasks = new Texture2D[TextureAssets.GlowMask.Value.Length + 1];
                for (int i = 0; i < TextureAssets.GlowMask.Value.Length; i++)
                {
                    glowMasks[i] = TextureAssets.GlowMask[i].Value;
                }
                glowMasks[glowMasks.Length - 1] = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                TextureAssets.GlowMask.Value = glowMasks;
            }
            // DisplayName.SetDefault("Discordium");
            // Tooltip.SetDefault("The World Chaoses melded together into a single, powerful bar");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 9));
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity14;
                }
            }
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;
            Item.maxStack = 999;
            Item.value = 10000;
            Item.rare = ItemRarityID.Purple;
            Item.glowMask = customGlowMask;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DaybreakIncinerite", 1);
            recipe.AddIngredient(null, "EventideAbyssium", 1);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }


        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Magenta.ToVector3() * 0.55f * Main.essScale);
        }
    }
}