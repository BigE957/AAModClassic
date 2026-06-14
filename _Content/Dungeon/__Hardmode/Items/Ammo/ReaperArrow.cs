using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Dungeon.__Hardmode.Items.Ammo
{
    public class ReaperArrow : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Ammo";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Reaper Arrow");
			// Tooltip.SetDefault("This arrow can shoot through walls");
		}

		public override void SetDefaults()
		{
			Item.damage = 16;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 14;
			Item.height = 48;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			Item.knockBack = 7f;
			Item.value = 100;
			Item.rare = ItemRarityID.LightPurple;
			Item.shoot = ModContent.ProjectileType<ReaperArrow_Proj>();   //The projectile shoot when your weapon using this ammo
			Item.shootSpeed = 2f;                  //The speed of the projectile
			Item.ammo = AmmoID.Arrow;              //The ammo class this ammo belongs to.
		}
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(50);
			recipe.AddIngredient(ItemID.BoneArrow, 50);
			recipe.AddIngredient(ItemID.Ectoplasm, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
