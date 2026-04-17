using AAModClassic.___Content.Hell.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.___Content.Hell.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class ImpChestplate : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Imp Garb");
            // Tooltip.SetDefault("7% Increased Minion damage");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 22;
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
                recipe.AddIngredient(ModContent.ItemType<DevilSilk>(), 7);
                recipe.AddTile(TileID.Loom);
                recipe.Register();
            }
        }
    }
}