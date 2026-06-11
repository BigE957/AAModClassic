using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Globals;
using ReLogic.Content;
using AAModClassic.Tiles.Crafters;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Materials
{
    public class DiscordiumBar : BaseAAItem
    {
        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Asset<Texture2D>[] glowMasks = new Asset<Texture2D>[TextureAssets.GlowMask.Length + 1];
                for (int i = 0; i < TextureAssets.GlowMask.Length; i++)
                {
                    glowMasks[i] = TextureAssets.GlowMask[i];
                }
                glowMasks[glowMasks.Length - 1] = ModContent.Request<Texture2D>(Texture + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                TextureAssets.GlowMask = glowMasks;
            }
            // DisplayName.SetDefault("Discordium");
            // Tooltip.SetDefault("The World Chaoses melded together into a single, powerful bar");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 9));
        }

        

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = 10000;
            Item.rare = ItemRarityID.Purple;
            Item.glowMask = customGlowMask;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>(), 1);
            recipe.AddIngredient(ModContent.ItemType<EventideAbyssiumBar>(), 1);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }


        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Magenta.ToVector3() * 0.55f * Main.essScale);
        }
    }
}