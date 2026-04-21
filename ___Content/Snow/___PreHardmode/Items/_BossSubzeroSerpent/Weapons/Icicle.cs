using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Weapons
{
    public class Icicle : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Icicle");
            // Tooltip.SetDefault("Casts crystals that shatter in pieces.");
        }

        public override void SetDefaults()
        {
            Item.damage = 23;    
            Item.DamageType = DamageClass.Magic;
            Item.width = 24;
            Item.height = 28; 
            Item.useTime = 17;  
            Item.useAnimation = 17;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; 
            Item.knockBack = 1;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.mana = 9;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Icicle_IceSpike>();
            Item.shootSpeed = 9f;
        }
    }
}
