using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hell.___PreHardmode.Items.Weapons
{
	public class Fireball : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Ranged";
		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Ranged;
			Item.noUseGraphic = true;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.width = 16;
			Item.height = 16;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.shoot = ModContent.ProjectileType<Fireball_Proj>();
			Item.shootSpeed = 12f;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 4;
			Item.value = Item.sellPrice(0, 0, 1, 0);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.crit = 10;
            Item.noMelee = true;
        }

		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Fireball");
            // Tooltip.SetDefault("Even better than Mario's Fire Flower!");
            Item.ResearchUnlockCount = 99;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(99);
			recipe.AddIngredient(ItemID.HellstoneBar);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
