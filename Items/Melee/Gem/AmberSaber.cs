using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee.Gem   //where is located
{
    public class AmberSaber : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 24;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 44;              
            Item.height = 44;               //Item Description
            Item.useTime = 20;          
            Item.useAnimation = 20;     
            Item.useStyle = ItemUseStyleID.Swing;        
            Item.knockBack = 5;      
            Item.value = 1000;        
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;       
            Item.autoReuse = false;   
            Item.useTurn = true;               
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Amber Saber");
      // Tooltip.SetDefault("");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.Amber, 5);   
            recipe.AddIngredient(ItemID.DesertFossil, 12);
            recipe.AddTile(TileID.Anvils);   
            recipe.Register();

        }
    }
}
