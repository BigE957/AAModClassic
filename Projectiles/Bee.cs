using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Bee : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bee);
            Main.projFrames[Projectile.type] = 4;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.defense < 300 && !target.boss)
            {
                damage += target.defense * 2;
            }
        }
    }
}
