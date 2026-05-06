using AAModClassic._Content.Desert.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Snow.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Ocean.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class AtlanteanHelmet : BaseAAItem
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
			return body.type == ModContent.ItemType<AtlanteanChestplate>() && legs.type == ModContent.ItemType<AtlanteanLeggings>();
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
            recipe.AddIngredient(ModContent.ItemType<OceanHelmet>());
            recipe.AddIngredient(ModContent.ItemType<HydraHide>(), 5);
            recipe.AddIngredient(ModContent.ItemType<RelicBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OceanHelmet>());
            recipe.AddIngredient(ModContent.ItemType<ScorchedScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DynaskullFossil>(), 10);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
	}
}