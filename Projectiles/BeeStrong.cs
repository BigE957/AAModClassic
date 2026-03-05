using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class BeeStrong : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.GiantBee);
            Main.projFrames[Projectile.type] = 4;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.defense < 300 && !target.boss)
            {
                modifiers.FlatBonusDamage += target.defense * 2;
            }
        }
    }
}
