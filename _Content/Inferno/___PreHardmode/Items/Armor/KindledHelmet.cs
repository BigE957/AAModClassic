using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class KindledHelmet : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Kindled";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Kindled Kabuto");
			// Tooltip.SetDefault(@"'Forged in the flames of the blazing sun'");
        }

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 20;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.defense = 7;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
            return body.type == ModContent.ItemType<KindledChestplate>() && legs.type == ModContent.ItemType<KindledLeggings>();
        }

        public override void RegisterEquipStats()
        {
            AddSetEffect(new EnduranceEffect(0.02f));
            AddSetEffect<MagmaStoneEffect>();
            AddSetEffect(new EmitLightFromPlayerEffect(AAColor.Lantern.R / 255, AAColor.Lantern.G / 255 * 0.95f, AAColor.Lantern.B / 255 * 0.8f));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<IncineriteBar>(), 15);
            recipe.AddIngredient(ModContent.ItemType<ScorchedScale>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
	}
}