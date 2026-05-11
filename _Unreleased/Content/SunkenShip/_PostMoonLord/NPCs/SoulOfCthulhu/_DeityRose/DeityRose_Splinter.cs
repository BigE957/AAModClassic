using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityRose
{
    public class DeityRose_Splinter : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Ei'Lor Splinter");
            Main.projFrames[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {      
            Projectile.CloneDefaults(ProjectileID.SeedPlantera);
            Projectile.scale = 1f;
            Projectile.alpha = 0;
        }
    }
}
