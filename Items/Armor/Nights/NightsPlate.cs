using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Nights
{
    [AutoloadEquip(EquipType.Body)]
    public class NightsPlate : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Night's Plate");
            // Tooltip.SetDefault("9% increased melee speed");

        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.value = 90000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Melee) += 0.09f;
        }
        public override void AddRecipes()
        {
            { 
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ShadowScalemail, 1);
            recipe.AddIngredient(ItemID.JungleSpores, 8);
            recipe.AddIngredient(ItemID.Bone, 8);
            recipe.AddIngredient(null, "DevilSilk", 8);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
            }
        }
    }
}