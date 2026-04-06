using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using AAModClassic.Dusts;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityLeviathan
{
    public class LeviathanBubble : ModNPC
	{
        public bool HeadsSpawned = false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul of Cthulhu");
        }

        public override void SetDefaults()
        {
            NPC.width = 36;
            NPC.height = 36;
            NPC.aiStyle = -1;
            NPC.damage = 100;
            NPC.defense = 0;
            NPC.lifeMax = 1;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath3;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.alpha = 255;
        }

        public override void AI()
        {
            if (NPC.target == 255)
            {
                NPC.TargetClosest(true);
                NPC.ai[3] = Main.rand.Next(80, 121) / 100f;
                float scaleFactor = Main.rand.Next(165, 265) / 15f;
                NPC.velocity = Vector2.Normalize(Main.player[NPC.target].Center - NPC.Center + new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101))) * scaleFactor;
                NPC.netUpdate = true;
            }
            Vector2 vector122 = Vector2.Normalize(Main.player[NPC.target].Center - NPC.Center);
            NPC.velocity = (NPC.velocity * 40f + vector122 * 20f) / 41f;
            NPC.scale = NPC.ai[3];
            NPC.alpha -= 30;
            if (NPC.alpha < 50)
            {
                NPC.alpha = 50;
            }
            NPC.alpha = 50;
            NPC.velocity.X = (NPC.velocity.X * 50f + Main.windSpeedCurrent * 2f + Main.rand.Next(-10, 11) * 0.1f) / 51f;
            NPC.velocity.Y = (NPC.velocity.Y * 50f + -0.25f + Main.rand.Next(-10, 11) * 0.2f) / 51f;
            if (NPC.velocity.Y > 0f)
            {
                NPC.velocity.Y = NPC.velocity.Y - 0.04f;
            }
            if (NPC.ai[0] == 0f)
            {
                int num983 = 40;
                Rectangle rect = NPC.getRect();
                rect.X -= num983 + NPC.width / 2;
                rect.Y -= num983 + NPC.height / 2;
                rect.Width += num983 * 2;
                rect.Height += num983 * 2;
                for (int num984 = 0; num984 < 255; num984++)
                {
                    Player player2 = Main.player[num984];
                    if (player2.active && !player2.dead && rect.Intersects(player2.getRect()))
                    {
                        NPC.ai[0] = 1f;
                        NPC.ai[1] = 4f;
                        NPC.netUpdate = true;
                        break;
                    }
                }
            }
            if (NPC.ai[0] == 0f)
            {
                NPC.ai[1] += 1f;
                if (NPC.ai[1] >= 150f)
                {
                    NPC.ai[0] = 1f;
                    NPC.ai[1] = 4f;
                }
            }
            if (NPC.ai[0] == 1f)
            {
                NPC.ai[1] -= 1f;
                if (NPC.ai[1] <= 0f)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                    return;
                }
            }
            if (NPC.justHit || NPC.ai[0] == 1f)
            {
                NPC.dontTakeDamage = true;
                NPC.position = NPC.Center;
                NPC.width = NPC.height = 100;
                NPC.position = new Vector2(NPC.position.X - NPC.width / 2, NPC.position.Y - NPC.height / 2);
                if (NPC.timeLeft > 3)
                {
                    NPC.timeLeft = 3;
                    return;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            int num = 1;
            if (!Main.dedServ)
            {
                Main.instance.LoadNPC(NPC.type);
                if (TextureAssets.Npc[NPC.type].Value == null)
                {
                    return;
                }
                num = TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type];
            }
            NPC.frame.Y = num;
        }
        //TODOSOC
        /*
        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            bool flag = Main.netMode == 0;
            if (!NPC.active || NPC.life <= 0)
            {
                return false;
            }
            double num = (double)damage;
            int num2 = NPC.defense;
            if (num >= 1.0)
            {
                if (flag)
                {
                    NPC.PlayerInteraction(Main.myPlayer);
                }
                NPC.justHit = true;
                num = 0.0;
                NPC.ai[0] = 1f;
                NPC.ai[1] = 4f;
                NPC.dontTakeDamage = true;
            }
            return false;
        }
        */
        public override void HitEffect(NPC.HitInfo hit)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath3, NPC.position);
            if (NPC.life <= 0)
            {
                Vector2 arg_98DC_0 = NPC.Center;
                for (int num207 = 0; num207 < 60; num207++)
                {
                    int num208 = 25;
                    int num209 = Dust.NewDust(NPC.Center - Vector2.One * num208, num208 * 2, num208 * 2, ModContent.DustType<Dusts.CthulhuDust>(), 0f, 0f, 0, default, 1f);
                    Dust dust47 = Main.dust[num209];
                    Vector2 vector7 = Vector2.Normalize(dust47.position - NPC.Center);
                    dust47.position = NPC.Center + vector7 * 25f * NPC.scale;
                    if (num207 < 30)
                    {
                        dust47.velocity = vector7 * dust47.velocity.Length();
                    }
                    else
                    {
                        dust47.velocity = vector7 * Main.rand.Next(45, 91) / 10f;
                    }
                    dust47.color = Main.hslToRgb((float)(0.40000000596046448 + Main.rand.NextDouble() * 0.20000000298023224), 0.9f, 0.5f);
                    dust47.color = Color.Lerp(dust47.color, Color.White, 0.3f);
                    dust47.noGravity = true;
                    dust47.scale = 0.7f;
                }
            }
        }
    }
}