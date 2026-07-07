using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon
{
    public class ShenDoragon_ChaosFireballSpread : ShenDoragon_ChaosFireballAbstract
    {
        public override void SetDefaults()
        { 
            base.SetDefaults();

            Projectile.timeLeft = 240;
        }

        public override void AI()
        {
            if (--Projectile.ai[0] == 0)
            {
                Projectile.netUpdate = true;
                Projectile.velocity = Vector2.Zero;
            }
            if (--Projectile.ai[1] == 0)
            {
                Projectile.netUpdate = true;
                Player target = Main.player[Player.FindClosest(Projectile.position, Projectile.width, Projectile.height)];
                Projectile.velocity = Projectile.DirectionTo(target.Center + target.velocity * 30) * 30;
            }
        }
    }
}