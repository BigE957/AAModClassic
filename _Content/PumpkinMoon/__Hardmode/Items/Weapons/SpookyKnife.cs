using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.PumpkinMoon.__Hardmode.Items.Weapons
{
	public class SpookyKnife : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Ranged";
		public override void SetDefaults()
		{
			Item.damage = 100;
			Item.DamageType = DamageClass.Ranged;
			Item.noUseGraphic = true;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.width = 14;
			Item.height = 38;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.shoot = ModContent.ProjectileType<SpookyKnife_Proj>();
			Item.shootSpeed = 14f;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 4;
			Item.value = Item.sellPrice(0, 0, 5, 0);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.crit = 15;
            Item.noMelee = true;
        }

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Spooky Knife");
			// Tooltip.SetDefault("Spreads Mourning Wood Embers on hit");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(99);
			recipe.AddIngredient(ItemID.SpookyWood, 10);
			recipe.AddTile(TileID.Sawmill);
			recipe.Register();
		}
	}
}
