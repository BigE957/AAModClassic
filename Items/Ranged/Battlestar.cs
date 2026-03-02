using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
{
    public class Battlestar : BaseAAItem
	{
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Battlestar");
			/* Tooltip.SetDefault("Turns bullets into chlorophyte bullets!"
				+ "\n32% chance not to consume ammo."); */
		}

		public override void SetDefaults()
		{
			Item.damage = 17;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 52;
			Item.height = 24;
			Item.useAnimation = 3;
			Item.useTime = 3;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 2;
			Item.value = Item.sellPrice(0, 14, 50, 0);
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item40;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Bullet;			
			Item.crit = 3;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_GUN; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.ShroomiteBar, 14);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

        public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= .32f;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (type == ProjectileID.Bullet) // or ProjectileID.WoodenArrowFriendly
			{
				type = ProjectileID.ChlorophyteBullet; // or ProjectileID.FireArrow;
			}
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(02));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true; // return true to allow tmodloader to call Projectile.NewProjectile as normal
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, 4);
		}
	}
}
