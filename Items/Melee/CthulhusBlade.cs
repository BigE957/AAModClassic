using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
namespace AAMod.Items.Melee  //where is located
{
    public class CthulhusBlade : BaseAAItem
    {
        
        public override void SetDefaults()
        {

            Item.damage = 23;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 48;              
            Item.height = 52;             
            Item.useTime = 22;          
            Item.useAnimation = 22;     
            Item.useStyle = 1;        
            Item.knockBack = 7;      
            Item.value = 19000;        
            Item.rare = 3;
            Item.UseSound = SoundID.Item1;                  
            Item.autoReuse = true;   
            Item.useTurn = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cthulhu's Blade");
            // Tooltip.SetDefault("");
        }
    }
}
