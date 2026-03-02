using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class TeslaHand : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Broken Zero Weapon");
            // Tooltip.SetDefault("Violently attempting to zap you");
        }

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 4;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2.5f;
            Item.autoReuse = true;
            Item.reuseDelay = 15;
            Item.useAnimation = 12;
            Item.shootSpeed = 16f;
            Item.width = 36;
            Item.height = 42;
            Item.damage = 240;
            Item.UseSound = Mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Glitch");
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.shoot = Mod.Find<ModProjectile>("Teslashock").Type;
            Item.rare = 9; AARarity = 13;
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

        // How can I make the shots appear out of the muzzle exactly?
        // Also, when I do this, how do I prevent shooting through tiles?
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			return true;
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "ApocalyptitePlate", 5);
			recipe.AddIngredient(null, "UnstableSingularity", 5);
	        recipe.AddTile(null, "ACS");
	        recipe.Register();
		}
	}
}
