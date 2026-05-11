using AAModClassic.Globals;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu
{
    public class RealityBar : ModItem
    {
        private static Asset<Texture2D> glowmask;
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Reality Bar");
            //Tooltip.SetDefault("Raw, interdimensional energy");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 6));
            if (Main.netMode != NetmodeID.Server)
                glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Cthulhu;
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 26;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(1, 0, 0, 0);
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, AAColor.Cthulhu2.ToVector3() * 0.55f * Main.essScale);
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            return false;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            var frame = Item.GetFrame(whoAmI);
            var position = Item.Center - Main.screenPosition;
            var origin = frame.Size() / 2f;
            spriteBatch.Draw(TextureAssets.Item[Type].Value, position, frame, lightColor, rotation, origin, scale, SpriteEffects.None, 0);
            spriteBatch.Draw(glowmask.Value, position, frame, lightColor, rotation, origin, scale, SpriteEffects.None, 0);
        }

        //TODO: Riftstone literally doesnt generate and also is just recolored dungeon brick so no im not porting it. bitch.
        /*
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Riftstone>(), 5);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
        */
    }
}
