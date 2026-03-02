using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Tools
{
    public class OceanAxe : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 12;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 44;
            Item.height = 40;

            Item.useTime = 12;
            Item.useAnimation = 20;
            Item.axe = 10;    //pickaxe power
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3;
            Item.value = 10;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Coral Axe");
            // Tooltip.SetDefault("the axe made from the Ocean");
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Coral, 15); 
            recipe.AddTile(TileID.WorkBenches);   
            recipe.Register();
        }
    }
}
