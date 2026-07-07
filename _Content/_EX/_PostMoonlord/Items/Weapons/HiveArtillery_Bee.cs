using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class HiveArtillery_Bee : ModProjectile
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
                modifiers.FlatBonusDamage += target.defense * 2;
            }
        }
    }
}
