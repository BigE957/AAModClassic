using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic;
using AAModClassic.Items.Materials;
using AAModClassic.Items.Boss.Broodmother;
using AAModClassic.Items.Blocks;


namespace AAModClassic.Items.Armor.Dynaskull
{
    [AutoloadEquip(EquipType.Head)]
	public class Dynaskull : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dynaskull");
			// Tooltip.SetDefault("20% decreased ammo consumption");

		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 28;
			Item.value = 90000;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 7;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.ammoCost80 = true ;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DynaskullRibguard>() && legs.type == ModContent.ItemType<DynaskullGreaves>();
		}

		public override void UpdateArmorSet(Player player)
		{

            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.DynaskullBonus");
            
			player.GetModPlayer<AAPlayer>().DynaskullSet = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.FossilHelm, 1);
            recipe.AddIngredient(ModContent.ItemType<DynaskullOre>(), 15);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 5);
            recipe.AddIngredient(ItemID.Coral, 5);
            recipe.AddIngredient(ModContent.ItemType<BroodScale>(), 5);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}