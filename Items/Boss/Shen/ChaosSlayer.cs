using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic.Globals;

namespace AAModClassic.Items.Boss.Shen
{
    public class ChaosSlayer : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaos Slayer");
            /* Tooltip.SetDefault(@"Unleashes blades of chaos to smite your foes
blades go through tiles
'Shatter all sanity'"); */
        }

        public override void SetDefaults()
        {
            Item.width = 85;
            Item.height = 85;
            Item.value = Item.sellPrice(1, 50, 0, 0);
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.UseSound = SoundID.Item103;
            Item.damage = 400;
            Item.knockBack = 12;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.ChaosSlayerSword>();
			Item.shootSpeed = 5;
            Item.useTurn = true;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 14;
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
			Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, type, damage, knockback, player.whoAmI);
			for (int m = 0; m < 2; m++)
			{
				Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, m == 0 ? ModContent.ProjectileType<ChaosSlayerSwordRed>() : ModContent.ProjectileType<ChaosSlayerSwordBlue>(), damage, knockback, player.whoAmI);
			}
			return false;
		}

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ReignOfFire", 1);
            recipe.AddIngredient(null, "Hydraslayer", 1);
            recipe.AddIngredient(null, "ChaosScale", 5);
            recipe.AddIngredient(null, "Discordium", 5);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}