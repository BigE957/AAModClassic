using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Armor.Deathly
{
    [AutoloadEquip(EquipType.Body)]
    public class DeathlyRibguard : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Deathly Ribguard");
            // Tooltip.SetDefault("9% Increased ranged damage");
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 34;
            Item.value = 90000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.09f;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.NecroBreastplate, 1);
                recipe.AddIngredient(ItemID.JungleSpores, 8);
                recipe.AddIngredient(ItemID.ShadowScale, 8);
                recipe.AddIngredient(null, "DevilSilk", 8);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.NecroBreastplate, 1);
                recipe.AddIngredient(ItemID.JungleSpores, 8);
                recipe.AddIngredient(ItemID.TissueSample, 8);
                recipe.AddIngredient(null, "DevilSilk", 8);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
        }
    }
}