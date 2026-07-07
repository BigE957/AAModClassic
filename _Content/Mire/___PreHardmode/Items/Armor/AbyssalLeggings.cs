using AAModClassic._Content.Snow.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class AbyssalLeggings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Abyssal";
		public override void SetStaticDefaults()
		{
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Abyssal Hakama");
            /* Tooltip.SetDefault(@"'Weightless as shadow itself'"); */
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 18;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 6;
		}

        public override void RegisterEquipEffects()
        {
            AddEffect(new MovementSpeedEffect(0.30f));
			AddEffect(new MaxRunSpeedEffect(0.30f));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DepthLeggings>(), 1);
            recipe.AddIngredient(ModContent.ItemType<RelicBar>(), 6);
            recipe.AddIngredient(ItemID.Coral, 6);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 6);
            recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}