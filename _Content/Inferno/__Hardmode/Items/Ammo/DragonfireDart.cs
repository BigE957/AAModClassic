using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Ammo
{
    public class DragonfireDart : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Ammo";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Dragonfire Dart");
		}

		public override void SetDefaults()
		{
			Item.shoot = ModContent.ProjectileType<DragonfireDart_Proj>();
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = Item.CommonMaxStack;
			Item.ammo = AmmoID.Dart;
			Item.damage = 11;
			Item.knockBack = 3f;
			Item.shootSpeed = 4f;
			Item.DamageType = DamageClass.Ranged;
			Item.rare = ItemRarityID.LightRed;
			Item.consumable = true;
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(100);
			recipe.AddIngredient(ModContent.ItemType<DragonFire>(), 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
