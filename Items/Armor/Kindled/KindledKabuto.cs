using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic.Items.Boss.Broodmother;
using AAModClassic.___Content.Inferno._PreHardmode.Items.Materials;


namespace AAModClassic.Items.Armor.Kindled
{
    [AutoloadEquip(EquipType.Head)]
	public class KindledKabuto : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Kindled Kabuto");
			// Tooltip.SetDefault(@"Forged in the flames of the blazing sun");
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
            return body.type == ModContent.ItemType<KindledDou>() && legs.type == ModContent.ItemType<KindledSuneate>();
        }

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.KindledKabutoBonus");
            player.endurance += .02f;
            player.GetModPlayer<AAPlayer>().kindledSet = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<IncineriteBar>(), 15);
            recipe.AddIngredient(ModContent.ItemType<BroodScale>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
	}
}