using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Throwing
{
	public class Dracorang : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.LightDisc);
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.shootSpeed = 16f;
			Item.useTime = 20;
			Item.damage = 50;                            
			Item.value = 20;
			Item.rare = ItemRarityID.LightRed;
			Item.knockBack = 4;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useAnimation = 20;
			Item.shoot = Mod.Find<ModProjectile>("DracorangP").Type;
			Item.width = 22;
			Item.height = 32;
            Item.noMelee = true;
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("DracorangP").Type] < Item.stack)
			{
				return true;
			}
			return false;
		}
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dracorang");
			/* Tooltip.SetDefault(@"Leaves short living flame trail
Stacks up to 5"); */
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod.Find<ModItem>("RadiantIncinerite").Type, 3);
			recipe.AddIngredient(ItemID.LivingFireBlock, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
