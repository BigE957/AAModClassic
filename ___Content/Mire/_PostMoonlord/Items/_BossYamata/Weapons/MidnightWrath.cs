using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System.Collections.Generic;
using AAModClassic.Globals;
using AAModClassic;

namespace AAModClassic.___Content.Mire._PostMoonlord.Items._BossYamata.Weapons
{
    public class MidnightWrath : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 130;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 20;
            Item.height = 20;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<MidnightWrath_Proj>();
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Cyan; AARarity = 13;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Midnight's Wrath");
            // Tooltip.SetDefault("Non-consumable");
        }


        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13; ;
                }
            }
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
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
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "EventideAbyssium", 5);
            recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(null, "DarkmatterKunai", 999);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
    }
}
