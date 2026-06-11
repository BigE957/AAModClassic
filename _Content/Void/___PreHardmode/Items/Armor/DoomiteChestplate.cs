using AAModClassic._Content.Desert.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class DoomiteChestplate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Doomite Plate");
            // Tooltip.SetDefault(@"+1 Minion slot");
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 20;
			Item.rare = ItemRarityID.LightRed;
            Item.defense = 7;
            Item.value = 9000;
		}

        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DarkDoomiteChestplate>());
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 10);
            recipe.AddIngredient(ItemID.Coral, 8);
            recipe.AddIngredient(ModContent.ItemType<DynaskullFossil>(), 16);
            recipe.AddIngredient(ModContent.ItemType<ScorchedScale>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}