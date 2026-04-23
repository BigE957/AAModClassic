using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.___PreHardmode.Items._BossDesertDjinn.Weapons
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
            Item.useStyle = ItemUseStyleID.Shoot;    
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.mana = 7;          
            Item.UseSound = SoundID.Item21;      
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<SandLamp_Spray>(); 
            Item.shootSpeed = 9f; 
        }
        
    }
}
