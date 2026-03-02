using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Djinn
{
    public class Djinnerang : BaseAAItem
	{

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Djinnerang");
            // Tooltip.SetDefault("");
        }

        public override void SetDefaults()
		{

            Item.damage = 30;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 30;
            Item.height = 30;
			Item.useTime = 12;
			Item.useAnimation = 12;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 0;
			Item.value = 8;
			Item.rare = ItemRarityID.LightPurple;
			Item.shootSpeed = 6f;
			Item.shoot = Mod.Find<ModProjectile>("Djinnerang").Type;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.value = 50000;
        }

    

        public override bool CanUseItem(Player player) 
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
