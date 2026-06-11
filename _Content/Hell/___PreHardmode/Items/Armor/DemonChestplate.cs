using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Hell.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class DemonChestplate : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Demon Garb");
            /* Tooltip.SetDefault(@"9% Increased Minion damage
+2 minion slots"); */
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 22;
            Item.value = 9000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.09f;
            player.maxMinions += 2;

        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<ImpChestplate>(), 1);
                recipe.AddIngredient(ItemID.Bone, 8);
                recipe.AddIngredient(ItemID.JungleSpores, 8);
                recipe.AddIngredient(ItemID.ShadowScale, 8);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<ImpChestplate>(), 1);
                recipe.AddIngredient(ItemID.Bone, 8);
                recipe.AddIngredient(ItemID.JungleSpores, 8);
                recipe.AddIngredient(ItemID.TissueSample, 8);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
        }
    }
}