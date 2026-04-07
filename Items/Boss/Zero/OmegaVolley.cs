using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Items.Materials;

namespace AAModClassic.Items.Boss.Zero
{
    public class OmegaVolley : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.autoReuse = true;
			Item.useAnimation = 2;
			Item.useTime = 5;
            Item.reuseDelay = 2;
			Item.width = 72;
			Item.height = 34;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.useAmmo = AmmoID.Bullet;
			Item.UseSound = SoundID.Item41;
			Item.damage = 85;
			Item.shootSpeed = 32f;
			Item.noMelee = true;
			Item.value = Item.sellPrice(0, 30, 0, 0);
			Item.rare = ItemRarityID.Purple;
			Item.knockBack = 3f;
			Item.DamageType = DamageClass.Ranged;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Omega Volley");
			/* Tooltip.SetDefault(@"Shoots an insanely accurate volley of sonic bullets quickly
Every ten shots, it can shoot two extra bullets.
33% chance to not consume ammo"); */
        }

		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= .77;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, -2);
		}

		private int extraammocount = 0;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num117 = 0.314159274f * 1.3f;
            int num118 = 3;
            Vector2 vector7 = velocity;
            vector7.Normalize();
            vector7 *= 20f;
            bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector7, 0, 0);
            for (int num119 = 0; num119 < num118; num119++)
            {
                float num120 = num119 - (num118 - 1f) / 2f;
                Vector2 value9 = vector7.RotatedBy(num117 * num120, default);
                if (!flag11)
                {
                    value9 -= vector7;
                }
                int num121 = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), vector2.X + 0.5f * value9.X, vector2.Y + 0.5f * value9.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
                Main.projectile[num121].noDropItem = true;
            }

			extraammocount ++;

			if(extraammocount >= 10)
			{
				num118 = 2;
				num117 *= 2;
				for (int num119 = 0; num119 < num118; num119++)
				{
					float num120 = num119 - (num118 - 1f) / 2f;
					Vector2 value9 = vector7.RotatedBy(num117 * num120, default);
					if (!flag11)
					{
						value9 -= vector7;
					}
					int num121 = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), vector2.X + value9.X, vector2.Y + value9.Y, velocity.X, velocity.Y, ModContent.ProjectileType<OmegaVolleyExtraAmmo>(), damage, knockback, player.whoAmI, 0.0f, 0.0f);
					Main.projectile[num121].noDropItem = true;
				}
				extraammocount = 0;
			}

            return false;

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
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 5);
			recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 5);
			recipe.AddIngredient(ItemID.ChainGun);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
			recipe.Register();
		}
	}
}
