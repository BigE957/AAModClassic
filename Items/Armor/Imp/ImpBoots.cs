using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Imp
{
    [AutoloadEquip(EquipType.Legs)]
	public class ImpBoots : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Imp Boots");
            // Tooltip.SetDefault("7% Increased Minion damage");
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = 7000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 4;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.07f;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(null, "DevilSilk", 5);
                recipe.AddTile(TileID.Loom);
                recipe.Register();
            }
        }
    }
}