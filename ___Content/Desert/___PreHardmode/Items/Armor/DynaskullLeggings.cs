using AAModClassic.___Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic.___Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Items.Blocks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.___Content.Desert.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class DynaskullLeggings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dynaskull Greaves");
            // Tooltip.SetDefault("12% Increased ranged critical chance");

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
			player.GetCritChance(DamageClass.Ranged) += 12;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FossilPants, 1);
            recipe.AddIngredient(ModContent.ItemType<DynaskullOre>(), 15);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 6);
            recipe.AddIngredient(ItemID.Coral, 6);
            recipe.AddIngredient(ModContent.ItemType<ScorchedScale>(), 6);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}