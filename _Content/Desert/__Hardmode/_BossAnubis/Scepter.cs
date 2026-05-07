using AAModClassic._Unreleased.Content.Desert.__Hardmode.NPCs.__BossAnubis;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.UI.WorldGen;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.__Hardmode._BossAnubis
{
    public class Scepter : ModProjectile
	{
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 3600;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
        }

        public int master = -1;

		public override void AI()
		{
            if (master >= 0 && (Main.npc[master] == null || !Main.npc[master].active || Main.npc[master].type != ModContent.NPCType<Anubis>())) master = -1;
            if (master == -1)
            {
                if(WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased))
                    master = BaseAI.GetNPC(Projectile.Center, ModContent.NPCType<AnubisUnreleased>(), -1, null);
                else
                    master = BaseAI.GetNPC(Projectile.Center, ModContent.NPCType<Anubis>(), -1, null);
                if (master == -1) master = -2;
            }
            if (master == -1) { return; }
			if (master < 0 || !Main.npc[master].active || Main.npc[master].life <= 0) { Projectile.Kill(); return; }

            if (Main.rand.NextBool(2))
            {
                int dustnumber = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GoldCoin, 0f, 0f, 200, default, 0.5f);
                Main.dust[dustnumber].velocity *= 0.3f;
            }

            BaseAI.AIBoomerang(Projectile, ref Projectile.ai, Main.npc[master].position, Main.npc[master].width, Main.npc[master].height, true, 40, 45, 10f, 1f, true);

            ReflectProjectiles(Projectile.Hitbox);
        }

        public static void ReflectProjectiles(Rectangle myRect)
        {
            for (int i = 0; i < 1000; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].CanBeReflected())
                {
                    Rectangle hitbox = Main.projectile[i].Hitbox;
                    if (myRect.Intersects(hitbox))
                    {
                        SoundEngine.PlaySound(SoundID.NPCHit4, Main.projectile[i].position);
                        for (int j = 0; j < 3; j++)
                        {
                            int num = Dust.NewDust(Main.projectile[i].position, Main.projectile[i].width, Main.projectile[i].height, DustID.Gold, 0f, 0f, 0, default, 1f);
                            Main.dust[num].velocity *= 0.3f;
                        }
                        Main.projectile[i].hostile = true;
                        Main.projectile[i].friendly = false;
                        Vector2 vector = Main.player[Main.projectile[i].owner].Center - Main.projectile[i].Center;
                        vector.Normalize();
                        vector *= Main.projectile[i].oldVelocity.Length();
                        Vector2 reflectvelocity = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                        reflectvelocity.Normalize();
                        reflectvelocity *= vector.Length();
                        reflectvelocity += vector * 20f;
                        reflectvelocity.Normalize();
                        reflectvelocity *= vector.Length();
                        Main.projectile[i].damage /= 2;
                        Main.projectile[i].penetrate = 1;
                        Main.projectile[i].GetGlobalProjectile<AAGlobalProjectile>().reflectvelocity = reflectvelocity;
                        Main.projectile[i].GetGlobalProjectile<AAGlobalProjectile>().isReflecting = true;
                    }
                }
            }
        }
    }
}