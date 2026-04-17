using AAModClassic.___Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Items.Boss.Broodmother;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Void.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class DoomiteLeggings : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomite Greaves");
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
            recipe.AddIngredient(ModContent.ItemType<DarkDoomiteLeggings>());
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 6);
            recipe.AddIngredient(ItemID.Coral, 6);
            recipe.AddIngredient(ItemID.FossilOre, 6);
            recipe.AddIngredient(ModContent.ItemType<BroodScale>(), 6);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}