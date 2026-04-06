using AAModClassic;
using AAModClassic.___Content.Mire._Hardmode.Items.Materials;
using AAModClassic.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Magic
{
	public class Toxifang : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.damage = 30;                        
			Item.DamageType = DamageClass.Magic;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = Item.sellPrice(0, 3, 0, 0);
			Item.rare = ItemRarityID.LightRed;
			Item.mana = 10;
			Item.UseSound = SoundID.Item21;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.Toxifang>();
			Item.shootSpeed = 8f;
		}   

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Toxifang");
			// Tooltip.SetDefault("Shoots Toxic Fangs");
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 perturbedSpeed = (velocity * 2).RotatedByRandom(MathHelper.ToRadians(8));
			velocity = perturbedSpeed;
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			return false;
		}
		
		public override void AddRecipes()  
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SpellTome);
			recipe.AddIngredient(ModContent.ItemType<HydraToxin>(), 20);
			recipe.AddIngredient(ModContent.ItemType<SoulOfSpite>(), 15);
			recipe.AddTile(TileID.Bookcases);
			recipe.Register();
		}
	}
}
