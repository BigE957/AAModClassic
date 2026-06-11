using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items._BossEmperorFishron.Weapons
{
    public class EmperorFlairon : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Emperor Flairon");
            // Tooltip.SetDefault("Lets loose an armada of homing bubbles");
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Flairon);
            Item.damage = 175;
            Item.rare = ItemRarityID.Purple;
            Item.shoot = ModContent.ProjectileType<EmperorFlairon_Holdout>();
            Item.channel = true;
        }



        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Flairon);
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
    }
}