using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Misc._PostMoonlord.Items.Weapons
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
			Item.shoot = ModContent.ProjectileType<AncientArcanum_Quazar>();
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
