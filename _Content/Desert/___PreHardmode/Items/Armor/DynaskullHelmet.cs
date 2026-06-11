using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Desert.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Desert.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class DynaskullHelmet : BaseAAItem
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
			return body.type == ModContent.ItemType<DynaskullChestplate>() && legs.type == ModContent.ItemType<DynaskullLeggings>();
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
            recipe.AddIngredient(ModContent.ItemType<DynaskullFossil>(), 15);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 5);
            recipe.AddIngredient(ItemID.Coral, 5);
            recipe.AddIngredient(ModContent.ItemType<ScorchedScale>(), 5);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}