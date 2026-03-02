using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace AAModClassic.NPCs.Bosses.Anubis.Forsaken
{
    public class SunSummon : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sun Summon");
        }

        public override void SetDefaults()
        {
            Projectile.width = 98;
            Projectile.height = 98;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            for (int num468 = 0; num468 < 5; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, 0, 0, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 0, default, 2f);
                Main.dust[num469].noGravity = true;
            }

            Projectile.damage = 0;
            Projectile.knockBack = 0;

            Projectile.ai[1] = Projectile.velocity.Length();

            Projectile.velocity = Projectile.velocity.RotatedBy(Projectile.ai[1] / (2 * Math.PI * Projectile.ai[0] * ++Projectile.localAI[0]));

            Projectile.ai[0]++;

            if (Projectile.ai[0] > 60)
            {
                Projectile.Kill();
            }
        }

        bool HitTile = false;

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            HitTile = true;
            return base.OnTileCollide(oldVelocity);
        }

        public override void OnKill(int timeLeft)
        {
            int MinionType = ModContent.NPCType<ForsakenSun>();

            if (!HitTile)
            {
                int Minion = NPC.NewNPC((int)Projectile.Center.X, (int)Projectile.Center.Y, MinionType, 0);
                Main.npc[Minion].netUpdate = true;
            }
        }
    }
}
