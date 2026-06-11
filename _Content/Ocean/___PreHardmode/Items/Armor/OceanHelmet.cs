using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Ocean.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class OceanHelmet : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Ocean Helmet");
            /* Tooltip.SetDefault(@"Increases maximum mana by 20
You can breath in water
5% increased magic damage"); */
        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 2;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 20;
            player.GetDamage(DamageClass.Magic) += 0.05f;
            player.breath = player.breathMax -1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<OceanChestplate>() && legs.type == ModContent.ItemType<OceanLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.OceanHelmBonus");
            if (player.wet && !player.lavaWet && !player.honeyWet)
            {
                player.GetDamage(DamageClass.Magic) += 0.2f;
                player.manaCost *= 0.85f;
            }
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Coral, 3);
			recipe.AddIngredient(ItemID.Starfish, 2);
			recipe.AddIngredient(ItemID.Seashell);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}