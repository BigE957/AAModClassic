using System;
using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
{
    public class TerraBallista : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terra Ballista");
            // Tooltip.SetDefault("Replaces Arrows with Terra Arrows");
        }

	    public override void SetDefaults()
	    {
	        Item.damage = 70;
	        Item.crit += 25;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 50;
	        Item.height = 34;
	        Item.useTime = 15;
	        Item.useAnimation = 15;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 2.5f;
	        Item.value = 350000;
	        Item.rare = ItemRarityID.Lime;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = ProjectileID.PurificationPowder;
	        Item.shootSpeed = 16f;
	        Item.useAmmo = 40;
	    }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float baseSpeed = (float)Math.Sqrt((velocity.X * velocity.X) + (velocity.Y * velocity.Y));
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<TerraArrow>(), damage, knockback, player.whoAmI, 0f, 0f);
        
            return false;
        }

        public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "TrueDeathlyLongbow");
            recipe.AddIngredient(ItemID.HallowedRepeater);
            recipe.AddIngredient(null, "HeroShards", 1);
            recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}