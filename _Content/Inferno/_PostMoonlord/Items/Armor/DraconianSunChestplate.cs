using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Armor;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    [AutoloadEquipGlow(EquipType.Body)]
    public class DraconianSunChestplate : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.DraconianSun";
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Draconian Sun Dao");
			/* Tooltip.SetDefault(@"'The blazing fury of the Inferno rests in this armor'"); */
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 20;
			Item.value = 3000000;
			Item.defense = 50;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Melee) += .25f;
			AddEffect(new EnduranceEffect(0.023f));
			AddEffect(new MaxLifeEffect(25));
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>(), 20);
			recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<KindledChestplate>(), 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
			recipe.Register();
		}
	}
}