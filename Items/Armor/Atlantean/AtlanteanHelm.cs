using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Buffs;
using AAModClassic.Items.Armor.Ocean;
using AAModClassic.___Content.Mire._PreHardmode.Items.Materials;
using AAModClassic.Items.Boss.Broodmother;
using AAModClassic.Items.Materials;

namespace AAModClassic.Items.Armor.Atlantean
{
    [AutoloadEquip(EquipType.Head)]
	public class AtlanteanHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Atlantean Helmet");
            /* Tooltip.SetDefault(@"Decreases mana usage by 15%
Allows to breath underwater"); */
        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 6;
        }
		
		public override void UpdateEquip(Player player)
		{
			player.manaCost -= 0.15f;
            player.gills = true;
		}
		
		
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<AtlanteanPlate>() && legs.type == ModContent.ItemType<AtlanteanGreaves>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.AtlanteanBonus");
			if (player.wet)
			{
                player.AddBuff(ModContent.BuffType<Atlantean_Buff>(), 2);
			}
        }
		
		public override void AddRecipes()
        {
            Recipe recipe;
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OceanHelm>());
            recipe.AddIngredient(ModContent.ItemType<HydraHide>(), 5);
            recipe.AddIngredient(ModContent.ItemType<RelicBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OceanHelm>());
            recipe.AddIngredient(ModContent.ItemType<BroodScale>(), 5);
            recipe.AddIngredient(ItemID.FossilOre, 5);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
	}
}