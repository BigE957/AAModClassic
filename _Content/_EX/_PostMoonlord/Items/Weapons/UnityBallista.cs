using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Terra.__Hardmode.Items.Ammo;
using AAModClassic._Content.Terra.__Hardmode.Items.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class UnityBallista : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Ranged";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Unity Ballista");
            /* Tooltip.SetDefault(@"Replaces Arrows with Terra Arrows
Shoots 3 waves of 3 arrows on single use
Terra Ballista EX"); */
        }

	    public override void SetDefaults()
	    {
	        Item.damage = 215;
	        Item.crit += 25;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 50;
	        Item.height = 34;
	        Item.useTime = 3;
	        Item.reuseDelay = 15;
	        Item.useAnimation = 9;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 3f;
	        Item.value = 500000;
	        Item.rare = ItemRarityID.Purple;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = ProjectileID.PurificationPowder;
	        Item.shootSpeed = 16f;
	        Item.useAmmo = 40;
	    }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			Vector2 perturbedSpeed2 = velocity.RotatedByRandom(MathHelper.PiOver4 / 4f);
			Vector2 perturbedSpeed3 = velocity.RotatedByRandom(MathHelper.PiOver4 / 4f);
			Projectile.NewProjectile(source, position, perturbedSpeed2, ModContent.ProjectileType<TerraArrow_Proj>(), damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<TerraArrow_Proj>(), damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position, perturbedSpeed3, ModContent.ProjectileType<TerraArrow_Proj>(), damage, knockback, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ModContent.ItemType<TerraBallista>());
			recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
	}
}