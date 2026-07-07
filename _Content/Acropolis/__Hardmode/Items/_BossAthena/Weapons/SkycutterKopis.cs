using AAModClassic._Content.Acropolis.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Weapons
{
    public class SkycutterKopis : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Melee";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Skycutter Kopis");
			// Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
			Item.damage = 70;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 40;
			Item.height = 50;
            Item.useTime = 26;
			Item.useAnimation = 26;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 4;
			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<SkycutterKopis_Skyblade>();
            Item.shootSpeed = 10;
		}

		public override void AddRecipes()
		{
			Recipe recipe;
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SilverBroadsword, 1);
			recipe.AddIngredient(ModContent.ItemType<GoddessFeather>(), 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.TungstenBroadsword, 1);
			recipe.AddIngredient(ModContent.ItemType<GoddessFeather>(), 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
