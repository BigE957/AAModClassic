using AAModClassic.___Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic.___Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Bunny._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class ChampionLeggings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Champion' Greaves");
            /* Tooltip.SetDefault(@"50% increased movement speed
15% increased damage
The armor of a champion feared across the land"); */
        }

		public override void SetDefaults()
		{
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.defense = 30;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 14;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity14;
                }
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += .15f;
            player.moveSpeed += .5f;
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += .5f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HoppingHoodlumLeggings>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ChampionPlate>(), 10);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}