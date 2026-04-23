using AAModClassic._Content.Underground.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee.Gem   //where is located
{
    public class Poppy : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Poppy");
            // Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {

            Item.damage = 32;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 44;              
            Item.height = 44;             
            Item.useTime = 20;          
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;        
            Item.knockBack = 3;      
            Item.value = 5000;        
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;       
            Item.autoReuse = true;   
            Item.useTurn = true;
        }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.Ruby, 1);
            recipe.AddIngredient(ItemID.Sapphire, 1);
            recipe.AddIngredient(ItemID.Emerald, 1);
            recipe.AddIngredient(ItemID.Topaz, 1);
            recipe.AddIngredient(ItemID.Amber, 1);
            recipe.AddIngredient(ItemID.Diamond, 1);
            recipe.AddIngredient(ItemID.Amethyst, 1);
            recipe.AddIngredient(ModContent.ItemType<Prism>(), 10);
            recipe.AddRecipeGroup("AAModClassic:Gold", 12);		
            recipe.AddTile(TileID.Anvils);   
            recipe.Register();

        }
    }
}
