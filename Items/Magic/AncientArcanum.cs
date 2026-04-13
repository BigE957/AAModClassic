using AAModClassic.___Content.Stars._PostMoonlord.Items;
using AAModClassic.Tiles.Crafters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Magic
{
    public class AncientArcanum : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ancient Arcanum");
			// Tooltip.SetDefault("Releases a homing miniature quazar that explodes upon hitting an enemy");
		}

		public override void SetDefaults()
		{
			Item.mana = 35;
			Item.damage = 195;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 9f;
			Item.shoot = ModContent.ProjectileType<Projectiles.AncientArcanum>();
			Item.width = 26;
			Item.height = 28;
			Item.UseSound = SoundID.Item117;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.autoReuse = true;
			Item.noMelee = true;
			Item.knockBack = 8f;
			Item.rare = ItemRarityID.Purple;
			Item.value = Item.sellPrice(0, 30, 0, 0);
			Item.DamageType = DamageClass.Magic;
			Item.glowMask = 194;
			Item.noUseGraphic = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.NebulaArcanum);
            recipe.AddIngredient(ModContent.ItemType<RadiantPhoton>(), 10);
            recipe.AddIngredient(ModContent.ItemType<DarkEnergy>(), 10);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
			recipe.Register();
		}
	}
}
