using AAModClassic._Content.Madness.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Madness.___PreHardmode.Items.Weapons
{
	public class MadnessKnife : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Ranged";
		public override void SetDefaults()
		{
			Item.damage = 13;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 14;
			Item.height = 32;
			Item.noUseGraphic = true;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.shoot = ModContent.ProjectileType<MadnessKnife_Proj>();
			Item.shootSpeed = 12f;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 2;
			Item.value = Item.sellPrice(0, 0, 0, 25);
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.noMelee = true;
        }

		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Madness Knife");
            Item.ResearchUnlockCount = 99;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(150);
			recipe.AddIngredient(ModContent.ItemType<MadnessFragment>());
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
