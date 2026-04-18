using AAModClassic.___Content.Terrarium.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Terrarium.__Hardmode.NPCs
{
    public class TerraDeadshot_Deadshot : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.WoodenArrowHostile);
            Projectile.hostile = true;
            Projectile.friendly = false;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Deadshot");
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<Terrablaze_Buff>(), 300);
        }
    }
}
