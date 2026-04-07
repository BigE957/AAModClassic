using AAModClassic;
using AAModClassic.Globals;
using AAModClassic.Items.Materials;
using AAModClassic.Tiles.Crafters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Tools
{
    public class RadiumPickaxe : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.damage = 90;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 4;
            Item.useAnimation = 12;
            Item.pick = 235;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1;
            Item.value = 10;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Starminer");
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RadiumBar>(), 20);
            recipe.AddIngredient(ModContent.ItemType<Stardust>(), 5);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
    }
}
