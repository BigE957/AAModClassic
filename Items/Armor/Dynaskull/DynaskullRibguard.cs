using AAModClassic.___Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Items.Blocks;
using AAModClassic.Items.Boss.Broodmother;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Armor.Dynaskull
{
    [AutoloadEquip(EquipType.Body)]
	public class DynaskullRibguard : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Dynaskull Ribguard");
            // Tooltip.SetDefault("13% increased ranged damage");
        }

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.value = 90000;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Ranged) += 0.13f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FossilShirt, 1);
            recipe.AddIngredient(ModContent.ItemType<DynaskullOre>(), 15);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 8);
            recipe.AddIngredient(ItemID.Coral, 8);
            recipe.AddIngredient(ModContent.ItemType<BroodScale>(), 8);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}