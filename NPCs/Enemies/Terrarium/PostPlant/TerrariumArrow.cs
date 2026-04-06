using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Enemies.Terrarium.PostPlant
{
    public class TerrariumArrow : ModProjectile
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
            target.AddBuff(ModContent.BuffType<Buffs.Terrablaze_Buff>(), 300);
        }
    }
}
