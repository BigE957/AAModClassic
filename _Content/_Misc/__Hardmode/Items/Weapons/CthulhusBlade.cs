using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
namespace AAModClassic._Content._Misc.__Hardmode.Items.Weapons  //where is located
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
            Item.useStyle = ItemUseStyleID.Swing;        
            Item.knockBack = 7;      
            Item.value = 19000;        
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;                  
            Item.autoReuse = true;   
            Item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cthulhu's Blade");
            // Tooltip.SetDefault("");
        }
    }
}
