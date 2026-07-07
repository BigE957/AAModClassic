using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Parthenan.__Hardmode.Items.Weapons   //where is located
{
    public class RomulusTazesaber : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        public override void SetDefaults()
        {
            Item.damage = 100;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 58;              
            Item.height = 58;             
            Item.useTime = 17;          
            Item.useAnimation = 17;     
            Item.useStyle = ItemUseStyleID.Swing;        
            Item.knockBack = 0;      
            Item.value = 10000;        
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item15;       
            Item.autoReuse = true;   
            Item.useTurn = true; 
        }

        public override void SetStaticDefaults()
        {
          // DisplayName.SetDefault("Romulus' Tazesaber");
          // Tooltip.SetDefault("A fulgarian Tazesaber stolen from a respected hero.");
        }
    }
}
