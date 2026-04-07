using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic.Globals;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero.Weapons
{
    public class Nova : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Nova");
            // Tooltip.SetDefault("Fires an explosive energy blast that causes an expanding explosion");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.shootSpeed = 10f;
            Item.knockBack = 0f;
            Item.width = 48;
            Item.height = 54;
            Item.damage = 390;
            Item.UseSound = SoundID.Item20;
            Item.shoot = ModContent.ProjectileType<Nova_NovaBurst>();
            Item.mana = 20;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.noUseGraphic = false;
            Item.autoReuse = true;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Mod.GetTexture("_Unreleased/Glowmasks/" + GetType().Name + "_Glow");
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

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.IZ;
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "VoidStar", 1);
            recipe.AddIngredient(null, "Infinitium", 12);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}
