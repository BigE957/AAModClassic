using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Djinn
{
    public class SandLamp : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sand Lamp");
        }

        public override void SetDefaults()
        {

            Item.damage = 24;                        
            Item.DamageType = DamageClass.Magic;            
            Item.width = 24;
            Item.height = 28;
            Item.useTime = 15;
            Item.useAnimation = 18;
            Item.useStyle = 5;    
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 3;
            Item.mana = 7;          
            Item.UseSound = SoundID.Item21;      
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("SandSpray").Type; 
            Item.shootSpeed = 9f; 
        }
        
    }
}
