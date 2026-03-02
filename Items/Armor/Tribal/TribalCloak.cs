using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Tribal
{
    [AutoloadEquip(EquipType.Body)]
    public class TribalCloak : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tribal Cloak");
            /* Tooltip.SetDefault(@"8% Increased magic critical chance
Increases Maximum Mana by 20"); */
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 24;
            Item.value = 90000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 20;
            player.GetCritChance(DamageClass.Magic) += 8;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.JungleShirt, 1);
                recipe.AddIngredient(ItemID.ShadowScale, 6);
                recipe.AddIngredient(ItemID.Bone, 6);
                recipe.AddIngredient(null, "DevilSilk", 6);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.JungleShirt, 1);
                recipe.AddIngredient(ItemID.TissueSample, 6);
                recipe.AddIngredient(ItemID.Bone, 6);
                recipe.AddIngredient(null, "DevilSilk", 6);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
        }
    }
}