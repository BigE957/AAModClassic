using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Ammo
{
    public class DaybreakArrow : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Daybreak Arrow");
			/* Tooltip.SetDefault(@"Scorches its target with the heat of the blazing sun
Inflicts Daybroken
Non-consumable"); */
		}

		public override void SetDefaults()
		{
			Item.damage = 23;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 14;
			Item.height = 40;
			Item.consumable = false;
			Item.knockBack = 7f;
			Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.shoot = ModContent.ProjectileType<DaybreakArrow_Proj>();
			Item.shootSpeed = 3f;
			Item.ammo = AmmoID.Arrow;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(1);
			recipe.AddIngredient(ItemID.MoonlordArrow, 999);
            recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 1);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
			recipe.Register();
		}
	}
}
