using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Underground.___PreHardmode.Items.Weapons   //where is located
{
    public class SapphireSaber : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 21;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 44;              
            Item.height = 44;               //Item Description
            Item.useTime = 17;          
            Item.useAnimation = 17;
            Item.useStyle = ItemUseStyleID.Swing;        
            Item.knockBack = 3;   
            Item.value = 1000;        
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;       
            Item.autoReuse = false;   
            Item.useTurn = true;               
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Sapphire Saber");
      // Tooltip.SetDefault("");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.Sapphire, 5);   
            recipe.AddRecipeGroup("AAModClassic:Silver", 12);
            recipe.AddTile(TileID.WorkBenches);   
            recipe.Register();

        }
    }
}
