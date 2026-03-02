using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using AAModClassic;
using AAModClassic.Globals;

namespace AAModClassic.Items.Blocks
{
    public class ChaosCrucible : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Chaos Crucible");
            // Tooltip.SetDefault(@"Even chaos requires a steady hand and a gigantic forge to work with");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 13;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 1000000;
            Item.createTile = Mod.Find<ModTile>("ChaosCrucible").Type;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Mod.GetTexture("Glowmasks/" + GetType().Name + "Bloc_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(null, "DaybreakIncinerite", 15);
                recipe.AddIngredient(null, "EventideAbyssium", 15);
                recipe.Register();
            }
        }
    }
}
