using AAModClassic._Content._EX._PostMoonlord.Items.Accessories;
using AAModClassic.Assets;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content._Dev.__Hardmode.Items.Weapons
{
    public class AleisterStaff_InvokedDamage : ModProjectile
    {
        public override string Texture => AssetDirectory.General.Nothing;

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.alpha = 255;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 3;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.DarkBlue;
        }
        private int time = 0;
        public override void AI()
        {
            time += 1;
            if (time >= 60)
            {
                if (Projectile.ai[1] == 0f)
                {
                    Projectile.friendly = true;
                    int num568 = (int)Projectile.ai[0];
                    if (!Main.npc[num568].active)
                    {
                        int[] array2 = new int[200];
                        int num569 = 0;
                        for (int num570 = 0; num570 < 200; num570++)
                        {
                            if (Main.npc[num570].CanBeChasedBy(this, true))
                            {
                                float num571 = Math.Abs(Main.npc[num570].position.X + Main.npc[num570].width / 2 - Projectile.position.X + Projectile.width / 2) + Math.Abs(Main.npc[num570].position.Y + Main.npc[num570].height / 2 - Projectile.position.Y + Projectile.height / 2);
                                if (num571 < 800f)
                                {
                                    array2[num569] = num570;
                                    num569++;
                                }
                            }
                        }
                        if (num569 == 0)
                        {
                            Projectile.Kill();
                            return;
                        }
                        num568 = array2[Main.rand.Next(num569)];
                        Projectile.ai[0] = num568;
                    }
                    float num572 = 4f;
                    Vector2 vector44 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
                    float num573 = Main.npc[num568].Center.X - vector44.X;
                    float num574 = Main.npc[num568].Center.Y - vector44.Y;
                    float num575 = (float)Math.Sqrt(num573 * num573 + num574 * num574);
                    num575 = num572 / num575;
                    num573 *= num575;
                    num574 *= num575;
                    int num576 = 30;
                    Projectile.velocity.X = (Projectile.velocity.X * (num576 - 1) + num573) / num576;
                    Projectile.velocity.Y = (Projectile.velocity.Y * (num576 - 1) + num574) / num576;
                }
                else
                {

                    int num492 = (int)Projectile.ai[0];
                    float num493 = 4f;
                    Vector2 vector39 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
                    float num494 = Main.player[num492].Center.X - vector39.X;
                    float num495 = Main.player[num492].Center.Y - vector39.Y;
                    float num496 = (float)Math.Sqrt(num494 * num494 + num495 * num495);
                    if (num496 < 50f && Projectile.position.X < Main.player[num492].position.X + Main.player[num492].width && Projectile.position.X + Projectile.width > Main.player[num492].position.X && Projectile.position.Y < Main.player[num492].position.Y + Main.player[num492].height && Projectile.position.Y + Projectile.height > Main.player[num492].position.Y)
                    {
                        if (Projectile.owner == Main.myPlayer)
                        {
                            Player player = Main.player[num492];
                            player.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().CaligulaSoul.Add((int)Projectile.ai[1]);
                            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.DarkGray, Language.GetTextValue("Mods.AAModClassic.Common.CaligulaSoul"), false, false);
                        }
                        Projectile.Kill();
                    }
                    num496 = num493 / num496;
                    num494 *= num496;
                    num495 *= num496;
                    Projectile.velocity.X = (Projectile.velocity.X * 15f + num494) / 16f;
                    Projectile.velocity.Y = (Projectile.velocity.Y * 15f + num495) / 16f;
                }
            }
            for (int num577 = 0; num577 < 5; num577++)
            {
                float num578 = Projectile.velocity.X * 0.2f * num577;
                float num579 = -(Projectile.velocity.Y * 0.2f) * num577;
                int num580 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.SpectreStaff, 0f, 0f, 20, Color.DarkBlue, 2f);
                Main.dust[num580].noGravity = true;
                Main.dust[num580].velocity *= 0f;
                Main.dust[num580].position.X = Main.dust[num580].position.X - num578;
                Main.dust[num580].position.Y = Main.dust[num580].position.Y - num579;
            }
            return;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            double Realdamage = modifiers.GetDamage(Projectile.damage, false);

            Main.player[Main.myPlayer].dpsDamage += (int)Realdamage;
            bool crit = true;
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
        }
    }
}