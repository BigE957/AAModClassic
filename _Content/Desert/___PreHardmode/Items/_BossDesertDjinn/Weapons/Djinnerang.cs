using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.___PreHardmode.Items._BossDesertDjinn.Weapons
{
    public class Djinnerang : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Melee";

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
			Item.shoot = ModContent.ProjectileType<Djinnerang_Proj>();
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.value = 50000;
        }

    

        public override bool CanUseItem(Player player) 
        {
            foreach (Projectile p in Main.ActiveProjectiles)
            {
                if (p.owner == Main.myPlayer && p.type == Item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
