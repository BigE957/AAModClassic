using AAModClassic._Content.Desert.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Desert.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class DynaskullLeggings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Dynaskull";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dynaskull Greaves");
        }

		public override void SetDefaults()
		{
            Item.width = 30;
			Item.height = 28;
			Item.value = 90000;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 7;
		}

        public override void RegisterEquipEffects()
        {
            damageMap.GetCritChance(DamageClass.Ranged) += 12;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FossilPants, 1);
            recipe.AddIngredient(ModContent.ItemType<DynaskullFossil>(), 15);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 6);
            recipe.AddIngredient(ItemID.Coral, 6);
            recipe.AddIngredient(ModContent.ItemType<ScorchedScale>(), 6);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}