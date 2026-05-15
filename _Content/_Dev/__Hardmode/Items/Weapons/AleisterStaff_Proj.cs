using AAModClassic._Content._Dev.Invoker;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content._Dev.__Hardmode.Items.Weapons
{
    public class AleisterStaff_Proj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 41;
            Projectile.height = 41;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 6000;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.damage = 0;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.LightBlue;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, (Main.DiscoR - Projectile.alpha) * 0.8f / 255f, (Main.DiscoG - Projectile.alpha) * 0.4f / 255f, (Main.DiscoB - Projectile.alpha) * 0f / 255f);
            Player projOwner = Main.player[Projectile.owner];
            Projectile.direction = projOwner.direction;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(225f);

            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= MathHelper.ToRadians(90f);
            }

            if (Projectile.ai[0] == 0f)
            {
                float[] ai = Projectile.ai;
                int num2 = 1;
                float num3 = ai[num2];
                ai[num2] = num3 + 1f;
                if (Projectile.ai[1] >= 45f)
                {
                    Projectile.ai[1] = 45f;
                    if (Projectile.velocity.X < 0f)
                    {
                        Projectile.spriteDirection = -1;
                        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(225f);
                    }
                    else
                    {
                        Projectile.spriteDirection = 1;
                        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
                    }
                }
            }
            if (Projectile.ai[0] == 1f)
            {
                Projectile.tileCollide = false;
                int num6 = 15;
                bool flag = false;
                bool flag2 = false;
                float[] localAI = Projectile.localAI;
                int num7 = 0;
                float num8 = localAI[num7];
                localAI[num7] = num8 + 1f;
                if (Projectile.localAI[0] % 30f == 0f)
                {
                    flag2 = true;
                }
                int num9 = (int)Projectile.ai[1];
                if (Projectile.localAI[0] >= 60 * num6)
                {
                    flag = true;
                }
                else if (num9 < 0 || num9 >= 200)
                {
                    flag = true;
                }
                else if (Main.npc[num9].active && !Main.npc[num9].dontTakeDamage)
                {
                    Projectile.Center = Main.npc[num9].Center - Projectile.velocity * 2f;
                    Projectile.gfxOffY = Main.npc[num9].gfxOffY;
                    Projectile.alpha = Main.npc[num9].alpha;
                    if (flag2)
                    {
                        Main.npc[num9].HitEffect(0, 1.0);
                    }
                    if (Main.npc[num9].GetGlobalNPC<AleisterStaffGlobalNPC>().IsBeingBanished)
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = true;
                }
                if (flag)
                {
                    Projectile.Kill();
                }
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Rectangle rectangle = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);

            double Realdamage = modifiers.FinalDamage.Flat;

            if (Main.player[Projectile.owner].GetModPlayer<InvokerPlayer>().SpringInvoker)
            {
                if (target.realLife >= 0)
                {
                    if (Main.npc[target.realLife].StrikeNPC(modifiers.ToHitInfo(Projectile.damage * 0.1f, false, Projectile.knockBack)) < .01f * Projectile.damage && Realdamage < Main.npc[target.realLife].lifeMax * .01f)
                    {
                        Realdamage = Main.npc[target.realLife].lifeMax * .034f;
                    }
                }
                else
                {
                    if (target.StrikeNPC(modifiers.ToHitInfo(Projectile.damage * 0.1f, false, Projectile.knockBack)) < .01f * Projectile.damage)
                    {
                        Realdamage = target.lifeMax * .034f;
                    }
                }
            }

            Realdamage = Main.DamageVar((int)Realdamage);

            //Main.player[Main.myPlayer].dpsDamage += (int)Realdamage;
            Main.player[Projectile.owner].addDPS((int)Realdamage);
            bool crit = false;
            Color damagecolor = crit ? CombatText.DamagedHostileCrit : CombatText.DamagedHostile;
            CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), damagecolor, (int)Realdamage, false, false);

            if (!target.immortal)
            {
                if (target.realLife >= 0)
                {
                    Main.npc[target.realLife].life -= (int)Realdamage;
                    target.life = Main.npc[target.realLife].life;
                    target.lifeMax = Main.npc[target.realLife].lifeMax;
                }
                else
                {
                    target.life -= (int)Realdamage;
                }
            }

            /* 
			if(target.life <= Realdamage) target.life -= target.life;
			else target.life -= (int)Realdamage;

			if(target.realLife >= 0)
			{
				if(Main.npc[target.realLife].life <= Realdamage) Main.npc[target.realLife].life -= Main.npc[target.realLife].life;
				else Main.npc[target.realLife].life -= (int)Realdamage;
			}
			*/

            if (target.realLife >= 0)
            {
                Main.npc[target.realLife].checkDead();
            }
            else
            {
                target.checkDead();
            }

            if (Projectile.owner == Main.myPlayer)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && ((Projectile.friendly && (!Main.npc[i].friendly || Projectile.type == ProjectileID.RottenEgg || (Main.npc[i].type == NPCID.Guide && Projectile.owner < 255 && Main.player[Projectile.owner].killGuide) || (Main.npc[i].type == NPCID.Clothier && Projectile.owner < 255 && Main.player[Projectile.owner].killClothier))) || (Projectile.hostile && Main.npc[i].friendly && !Main.npc[i].dontTakeDamageFromHostiles)) && (Projectile.owner < 0 || Main.npc[i].immune[Projectile.owner] == 0 || Projectile.maxPenetrate == 1) && (Main.npc[i].noTileCollide || !Projectile.ownerHitCheck || Projectile.CanHitWithOwnBody(Main.npc[i])))
                    {
                        bool flag;
                        if (Main.npc[i].type == NPCID.SolarCrawltipedeTail)
                        {
                            Rectangle rect = Main.npc[i].getRect();
                            int num = 8;
                            rect.X -= num;
                            rect.Y -= num;
                            rect.Width += num * 2;
                            rect.Height += num * 2;
                            flag = Projectile.Colliding(rectangle, rect);
                        }
                        else
                        {
                            flag = Projectile.Colliding(rectangle, Main.npc[i].getRect());
                        }
                        if (flag)
                        {
                            if (Main.npc[i].reflectsProjectiles && Projectile.CanBeReflected())
                            {
                                Main.npc[i].ReflectProjectile(Projectile);
                                return;
                            }
                            Projectile.ai[0] = 1f;
                            Projectile.ai[1] = i;
                            Projectile.velocity = (Main.npc[i].Center - Projectile.Center) * 0.75f;
                            Projectile.netUpdate = true;
                            Projectile.StatusNPC(i);
                            Projectile.damage = 0;
                            Point[] array = new Point[10];
                            int num2 = 0;
                            for (int j = 0; j < 1000; j++)
                            {
                                if (j != Projectile.whoAmI && Main.projectile[j].active && Main.projectile[j].owner == Main.myPlayer && Main.projectile[j].type == Projectile.type && Main.projectile[j].ai[0] == 1f && Main.projectile[j].ai[1] == i)
                                {
                                    array[num2++] = new Point(j, Main.projectile[j].timeLeft);
                                    if (num2 >= array.Length)
                                    {
                                        break;
                                    }
                                }
                            }
                            if (num2 >= array.Length)
                            {
                                int num3 = 0;
                                for (int k = 1; k < array.Length; k++)
                                {
                                    if (array[k].Y < array[num3].Y)
                                    {
                                        num3 = k;
                                    }
                                }
                                Main.projectile[array[num3].X].Kill();
                            }
                        }
                    }
                }
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
            }
            return null;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<AleisterStaff_BeBanished>(), 3600);
        }
    }
}