using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System;
using AAModClassic.Globals;

namespace AAModClassic.Items.Boss.Zero
{
    public class EventHorizon : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Event Horizon");
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 54;
            Item.damage = 400;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2f;
            Item.UseSound = SoundID.Item116;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.shoot = ModContent.ProjectileType<EventHorizon>();
            Item.shootSpeed = 22f;
            Item.rare = ItemRarityID.Cyan; AARarity = 13;
        }


        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
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
			recipe.AddIngredient(null, "ApocalyptitePlate", 5);
			recipe.AddIngredient(null, "UnstableSingularity", 5);
			recipe.AddIngredient(ItemID.SolarEruption);
	        recipe.AddTile(null, "ACS");
	        recipe.Register();
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
            float ai3 = (Main.rand.NextFloat() - 0.75f) * 0.7853982f;
            float spread = 45f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt((velocity.X * velocity.X) + (velocity.Y * velocity.Y));
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            offsetAngle = startAngle + (deltaAngle * 1);
            Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), ModContent.ProjectileType<EventHorizon>(), damage, knockback, player.whoAmI, 0.0f, ai3);
            return false;
        }
	}
}
