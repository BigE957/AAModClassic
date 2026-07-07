using AAModClassic._Content.Desert.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Snow.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Ocean.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class AtlanteanLeggings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Atlantean";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Atlantean Greaves");
            /* Tooltip.SetDefault(@"'It vibrates with the powers of Atlantis'"); */
        }

        public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
			Item.value = Item.sellPrice(0, 0, 5, 0);
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 6;
		}

        public override void RegisterEquipEffects()
        {
            damageMap.GetCritChance(DamageClass.Magic) += 10;
            AddEffect<FlipperEffect>();
            AddEffect<IgnoreWaterEffect>();
        }

        public override void AddRecipes()
		{
            Recipe recipe;
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OceanLeggings>());
            recipe.AddIngredient(ModContent.ItemType<HydraHide>(), 6);
            recipe.AddIngredient(ModContent.ItemType<RelicBar>(), 6);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 6);
            recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OceanLeggings>());
            recipe.AddIngredient(ModContent.ItemType<ScorchedScale>(), 6);
            recipe.AddIngredient(ModContent.ItemType<DynaskullFossil>(), 12);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 6);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();

        }
	}
}