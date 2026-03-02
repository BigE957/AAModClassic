using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Serpent
{
    public class BlizzardBuster : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blizzard Breaker");
        }
        public override void SetDefaults()
		{
            Item.damage = 26;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.width = 30;
            Item.height = 30;
			Item.useTime = 26;
			Item.useAnimation = 26;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
			Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 3;
			Item.shootSpeed = 10f;
			Item.shoot = Mod.Find<ModProjectile>("BB").Type;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
        }

        public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
