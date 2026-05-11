using AAModClassic._Content._Dev.Invoker;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;
using AAModClassic._Content.Void._PostMoonlord.NPCs._BossZero.Protocol;
using AAModClassic.Buffs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content._Dev.__Hardmode.Items.Weapons
{
    public class AleisterStaff_InvokedRune : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 86;
            Projectile.height = 86;
            Projectile.hostile = false;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 200;
            Projectile.damage = 0;
        }

        private int count = 0;

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, (Main.DiscoR - Projectile.alpha) * 0.8f / 255f, (Main.DiscoG - Projectile.alpha) * 0.4f / 255f, (Main.DiscoB - Projectile.alpha) * 0f / 255f);

            if (count < 13)
            {
                Projectile.alpha -= 20;
            }
            else if (count >= 13)
            {
                Projectile.alpha += 3;
                if (Projectile.alpha >= 250)
                {
                    Projectile.Kill();
                }
            }

            count++;

            int numa = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.SpectreStaff, 0f, 0f, 30, Color.OrangeRed, 1.3f);
            Main.dust[numa].noGravity = true;
            Main.dust[numa].alpha++;

            Projectile.damage = 0;
            Projectile.netUpdate = true;

            if (Projectile.ai[0] == 1f)
            {
                int num9 = (int)Projectile.ai[1];
                if (Main.npc[num9].active)
                {
                    Projectile.velocity = (Main.npc[num9].Center - Projectile.Center) * 0.75f;
                    Projectile.StatusNPC(num9);
                    Projectile.Center = Main.npc[num9].Center - Projectile.velocity * 2f;
                    Projectile.gfxOffY = Main.npc[num9].gfxOffY;
                }
                else if (num9 < 0 || num9 >= 200)
                {
                    Projectile.Kill();
                }
                else
                {
                    Projectile.Kill();
                }

                if (!Main.npc[num9].active && Main.npc[num9].life <= 0)
                {
                    Projectile.Kill();
                }
            }
            else
            {
                int num9 = (int)Projectile.ai[1];
                if (Main.player[num9].active)
                {
                    Projectile.velocity = (Main.player[num9].Center - Projectile.Center) * 0.75f;
                    Projectile.Center = Main.player[num9].Center - Projectile.velocity * 2f;
                    Projectile.gfxOffY = Main.player[num9].gfxOffY;
                }
                else if (num9 < 0 || num9 >= 200)
                {
                    Projectile.Kill();
                }
                else
                {
                    Projectile.Kill();
                }

                if (!Main.player[num9].active)
                {
                    Projectile.Kill();
                }
            }


        }
    }
}