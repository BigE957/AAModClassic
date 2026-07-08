using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert._PostMoonlord.NPCs.__BossAnubisA
{
    public class AnubisA_CrookOfTheSoulJudge : ModProjectile
	{
        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 3600;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }

        public int master = -1;

		public override void AI()
		{
            if (master >= 0 && (Main.npc[master] == null || !Main.npc[master].active || Main.npc[master].type != ModContent.NPCType<AnubisA>())) master = -1;
            if (master == -1)
            {
                master = BaseAI.GetNPC(Projectile.Center, ModContent.NPCType<AnubisA>(), -1, null);
                if (master == -1) master = -2;
            }
            if (master == -1) { return; }
			if (master < 0 || !Main.npc[master].active || Main.npc[master].life <= 0) { Projectile.Kill(); return; }

            if (Main.rand.NextBool(2))
            {
                int dustnumber = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 200, default, 0.5f);
                Main.dust[dustnumber].velocity *= 0.3f;
            }

            for (int m = Projectile.oldPos.Length - 1; m > 0; m--)
            {
                Projectile.oldPos[m] = Projectile.oldPos[m - 1];
            }
            Projectile.oldPos[0] = Projectile.position;

            BaseAI.AIBoomerang(Projectile, ref Projectile.ai, Main.npc[master].position, Main.npc[master].width, Main.npc[master].height, true, 40, 45, 10f, 1f, true);

            ReflectProjectiles(Projectile.Hitbox);
        }

        public static void ReflectProjectiles(Rectangle myRect)
        {
            foreach (Projectile p in Main.ActiveProjectiles)
            {
                if (p.CanBeReflected())
                {
                    Rectangle hitbox = p.Hitbox;
                    if (myRect.Intersects(hitbox))
                    {
                        SoundEngine.PlaySound(SoundID.NPCHit4, p.position);
                        for (int j = 0; j < 3; j++)
                        {
                            int num = Dust.NewDust(p.position, p.width, p.height, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 0, default, 1f);
                            Main.dust[num].velocity *= 0.3f;
                        }
                        p.hostile = true;
                        p.friendly = false;
                        Vector2 vector = Main.player[p.owner].Center - p.Center;
                        vector.Normalize();
                        vector *= p.oldVelocity.Length();
                        Vector2 reflectvelocity = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                        reflectvelocity.Normalize();
                        reflectvelocity *= vector.Length();
                        reflectvelocity += vector * 20f;
                        reflectvelocity.Normalize();
                        reflectvelocity *= vector.Length();
                        p.damage /= 2;
                        p.penetrate = 1;
                        p.GetGlobalProjectile<AAGlobalProjectile>().reflectvelocity = reflectvelocity;
                        p.GetGlobalProjectile<AAGlobalProjectile>().isReflecting = true;
                    }
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height, 0, 0);

            BaseDrawing.DrawAfterimage(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, 2f, 1f, 5, true, 0f, 0f, lightColor);

            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 1, frame, lightColor, true);
            return false;
        }
    }
}