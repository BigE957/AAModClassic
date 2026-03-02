using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class GuardianNight : BaseAAItem
    {
        
        public override void SetDefaults()
        {

            Item.damage = 174;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 78;              
            Item.height = 78;             
            Item.useTime = 26;          
            Item.useAnimation = 26;     
            Item.useStyle = 1;        
            Item.knockBack = 4;      
            Item.value = 20;        
            Item.rare = 7;
            Item.UseSound = SoundID.Item1;       
            Item.autoReuse = true;   
            Item.useTurn = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Guardian of the Depths");
            // Tooltip.SetDefault("");
        }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();      
			recipe.AddIngredient(null, "DeepAbyssium", 10);
			recipe.AddIngredient(ItemID.Ectoplasm, 15);
            recipe.AddTile(TileID.MythrilAnvil);   
            recipe.Register();

        }
    }
}
