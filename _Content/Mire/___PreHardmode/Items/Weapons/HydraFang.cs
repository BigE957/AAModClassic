using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Weapons
{
	public class HydraFang : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Ranged";
		public override void SetDefaults()
		{
			Item.damage = 13;
			Item.DamageType = DamageClass.Ranged;
			Item.noUseGraphic = true;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.width = 28;
			Item.height = 34;
			Item.useTime = 17;
			Item.useAnimation = 17;
			Item.shoot = ModContent.ProjectileType<HydraFang_Proj>();
			Item.shootSpeed = 16f;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 4;
			Item.value = Item.sellPrice(0, 0, 1, 0);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item19;
			Item.autoReuse = true;
			Item.crit = 10;
            Item.noMelee = true;
        }

		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Hydra Fang");
            // Tooltip.SetDefault("Pierces up to 3 enemies");
            Item.ResearchUnlockCount = 99;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(99);
			recipe.AddIngredient(ModContent.ItemType<AbyssiumBar>());
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
