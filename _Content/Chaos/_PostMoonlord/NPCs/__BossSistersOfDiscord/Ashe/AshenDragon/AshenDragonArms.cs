using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossSistersOfDiscord.Ashe.AshenDragon
{
    public class AshenDragonArms : AshenDragonHead
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ashen Dragon");
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.dontCountMe = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                NPC.position.X = NPC.Center.X;
                NPC.position.Y = NPC.Center.Y;
                NPC.width = 44;
                NPC.height = 78;
                NPC.position.X = NPC.Center.X;
                NPC.position.Y = NPC.Center.Y;

                int dust1 = ModContent.DustType<Dusts.AkumaDust>();
                Dust.NewDust(NPC.position, NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;

                int dust2 = ModContent.DustType<Dusts.AkumaDust>();
                Dust.NewDust(NPC.position, NPC.width, NPC.height, dust2, 0f, 0f, 0);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }

        public override bool PreAI()
        {
            if (NPC.ai[3] > 0)
            {
                NPC.realLife = (int)NPC.ai[3];
            }

            if (NPC.target < 0 || NPC.target == byte.MaxValue || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
            }

            if (Main.player[NPC.target].dead && NPC.timeLeft > 300)
            {
                NPC.timeLeft = 300;
            }

            NPC.direction = NPC.velocity.X < 0f ? -1 : 1;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!Main.npc[(int)NPC.ai[1]].active)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
            }

            if (Main.npc[(int)NPC.ai[1]].alpha < 128)
            {
                if (NPC.alpha != 0)
                {
                    for (int num934 = 0; num934 < 2; num934++)
                    {
                        int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, default, 2f);
                        Main.dust[num935].noGravity = false;
                        Main.dust[num935].noLight = false;
                    }
                }

                NPC.alpha -= 42;
                if (NPC.alpha < 0)
                {
                    NPC.alpha = 0;
                }
            }


            if (NPC.ai[1] < (double)Main.npc.Length)
            {
                float dirX = Main.npc[(int)NPC.ai[1]].Center.X - NPC.Center.X;
                float dirY = Main.npc[(int)NPC.ai[1]].Center.Y - NPC.Center.Y;
                NPC.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;

                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - NPC.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;
                NPC.direction = dirX < 0f ? 1 : -1;
                NPC.position.X += posX;
                NPC.position.Y += posY;
            }

            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, new Color(Color.White.R, Color.White.G, Color.White.B, 100), NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.SpriteEffectDirection(), 0);
            return false;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[NPC.target];
            if (player.vortexStealthActive && projectile.CountsAsClass(DamageClass.Ranged))
            {
                modifiers.TargetDamageMultiplier /= 2f;
                modifiers.DisableCrit();
            }

            if (projectile.penetrate == -1 && !projectile.minion)
            {
                modifiers.TargetDamageMultiplier *= 0.2f;
            }
            else if (projectile.penetrate > 1) //TODO: was >= 1 but that seemed too strict
            {
                projectile.damage *= (int).2;
            }
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
            NPC.damage = (int)(NPC.damage * 0.8f);
        }
    }
}