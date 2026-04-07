using AAModClassic.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Armor.Deathly
{
    [AutoloadEquip(EquipType.Legs)]
	public class DeathlyGreaves : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Deathly Greaves");
            // Tooltip.SetDefault("9% Increased ranged damage");
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = 90000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 7;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.09f;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.NecroGreaves, 1);
                recipe.AddIngredient(ItemID.JungleSpores, 6);
                recipe.AddIngredient(ItemID.ShadowScale, 6);
                recipe.AddIngredient(ModContent.ItemType<DevilSilk>(), 6);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.NecroGreaves, 1);
                recipe.AddIngredient(ItemID.JungleSpores, 6);
                recipe.AddIngredient(ItemID.TissueSample, 6);
                recipe.AddIngredient(ModContent.ItemType<DevilSilk>(), 6);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
        }
    }
}