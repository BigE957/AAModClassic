using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System;
using AAModClassic.Globals;

namespace AAModClassic.Items.Throwing
{
    public class DarkmatterChakram : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Darkmatter Spinblade");
        }
        public override void SetDefaults()
		{
            Item.damage = 65;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 30;
            Item.height = 30;
	        Item.useTime = 16;
	        Item.useAnimation = 16;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Swing;
	        Item.knockBack = 0;
	        Item.value = 100000;
	        Item.rare = ItemRarityID.Purple;
	        Item.shootSpeed = 12f;
	        Item.shoot = Mod.Find<ModProjectile>("DMC").Type;
	        Item.UseSound = SoundID.Item1;
	        Item.autoReuse = true;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 50f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
            double deltaAngle = spread / 12f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                if(i == 1) continue;
                offsetAngle = startAngle + (deltaAngle * i);
                int proj = Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Mod.Find<ModProjectile>("DMCE").Type, damage, knockBack, Item.playerIndexTheItemIsReservedFor);
                Main.projectile[proj].ranged = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
                Main.projectile[proj].DamageType = DamageClass.Magic;
            }
            return true;
        }

        public override bool CanUseItem(Player player) 
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DarkEnergy", 5);
            recipe.AddIngredient(null, "DarkMatter", 15);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
		}
    }
}
