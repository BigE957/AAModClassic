using AAModClassic._Content.Snow.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class AbyssalChestplate : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Abyssal";
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Abyssal Gi");
			/* Tooltip.SetDefault(@"'Weightless as shadow itself'"); */
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 7;
		}

        public override void RegisterEquipStats()
        {
            AddEffect(new MovementSpeedEffect(0.40f));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DepthChestplate>(), 1);
            recipe.AddIngredient(ModContent.ItemType<RelicBar>(), 8);
            recipe.AddIngredient(ItemID.Coral, 8);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 8);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}