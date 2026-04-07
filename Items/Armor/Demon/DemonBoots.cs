using AAModClassic.Items.Armor.Imp;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Armor.Demon
{
    [AutoloadEquip(EquipType.Legs)]
	public class DemonBoots : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Demon Hoofs");
            // Tooltip.SetDefault("9% Increased Minion damage");
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = 9000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.09f;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<ImpBoots>(), 1);
                recipe.AddIngredient(ItemID.Bone, 6);
                recipe.AddIngredient(ItemID.JungleSpores, 6);
                recipe.AddIngredient(ItemID.ShadowScale, 6);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<ImpBoots>(), 1);
                recipe.AddIngredient(ItemID.Bone, 6);
                recipe.AddIngredient(ItemID.JungleSpores, 6);
                recipe.AddIngredient(ItemID.TissueSample, 6);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
        }
    }
}