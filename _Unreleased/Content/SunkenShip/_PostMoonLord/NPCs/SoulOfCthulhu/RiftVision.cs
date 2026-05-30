using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu
{
    public class RiftVision : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rift Vision");
            NPCID.Sets.TrailCacheLength[NPC.type] = 5;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 60;
            NPC.height = 60;
            NPC.aiStyle = NPCAIStyleID.AncientVision;
            NPC.damage = 90;
            NPC.defense = 30;
            NPC.lifeMax = 8000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            AnimationType = NPCID.AncientCultistSquidhead;
            NPC.knockBackResist = 0f;
            NPC.alpha = 50;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override void AI()
        {
            if (NPC.alpha > 0)
            {
                NPC.alpha -= 30;
                if (NPC.alpha < 50)
                {
                    NPC.alpha = 50;
                }
            }
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            for (int num1275 = 0; num1275 < 200; num1275++)
            {
                if (num1275 != NPC.whoAmI && Main.npc[num1275].active && Main.npc[num1275].type == NPC.type)
                {
                    Vector2 value47 = Main.npc[num1275].Center - NPC.Center;
                    if (value47.Length() < 50f)
                    {
                        value47.Normalize();
                        if (value47.X == 0f && value47.Y == 0f)
                        {
                            if (num1275 > NPC.whoAmI)
                            {
                                value47.X = 1f;
                            }
                            else
                            {
                                value47.X = -1f;
                            }
                        }
                        value47 *= 0.4f;
                        NPC.velocity -= value47;
                        Main.npc[num1275].velocity += value47;
                    }
                }
            }

            float num1283 = 120f;
            if (NPC.localAI[0] < num1283)
            {
                if (NPC.localAI[0] == 0f)
                {
                    SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                    NPC.TargetClosest(true);
                    if (NPC.direction > 0)
                    {
                        NPC.velocity.X = NPC.velocity.X + 2f;
                    }
                    else
                    {
                        NPC.velocity.X = NPC.velocity.X - 2f;
                    }
                }
                NPC.localAI[0] += 1f;
                int num1284 = 10;
                for (int num1285 = 0; num1285 < 2; num1285++)
                {
                    int num1286 = Dust.NewDust(NPC.position - new Vector2(num1284), NPC.width + num1284 * 2, NPC.height + num1284 * 2, DustID.GoldFlame, 0f, 0f, 100, default, 2f);
                    Main.dust[num1286].noGravity = true;
                    Main.dust[num1286].noLight = true;
                }
            }

            if (NPC.ai[0] == 0f)
            {
                NPC.TargetClosest(true);
                NPC.ai[0] = 1f;
                NPC.ai[1] = NPC.direction;
            }
            else if (NPC.ai[0] == 1f)
            {
                NPC.TargetClosest(true);
                float num1287 = 0.3f;
                float num1288 = 7f;
                float num1289 = 4f;
                float num1290 = 660f;
                float num1291 = 4f;
                num1287 = 0.7f;
                num1288 = 14f;
                num1290 = 500f;
                num1289 = 6f;
                num1291 = 3f;
                NPC.velocity.X = NPC.velocity.X + NPC.ai[1] * num1287;
                if (NPC.velocity.X > num1288)
                {
                    NPC.velocity.X = num1288;
                }
                if (NPC.velocity.X < -num1288)
                {
                    NPC.velocity.X = -num1288;
                }
                float num1292 = Main.player[NPC.target].Center.Y - NPC.Center.Y;
                if (Math.Abs(num1292) > num1289)
                {
                    num1291 = 15f;
                }
                if (num1292 > num1289)
                {
                    num1292 = num1289;
                }
                else if (num1292 < -num1289)
                {
                    num1292 = -num1289;
                }
                NPC.velocity.Y = (NPC.velocity.Y * (num1291 - 1f) + num1292) / num1291;
                if (NPC.ai[1] > 0f && Main.player[NPC.target].Center.X - NPC.Center.X < -num1290 || NPC.ai[1] < 0f && Main.player[NPC.target].Center.X - NPC.Center.X > num1290)
                {
                    NPC.ai[0] = 2f;
                    NPC.ai[1] = 0f;
                    if (NPC.Center.Y + 20f > Main.player[NPC.target].Center.Y)
                    {
                        NPC.ai[1] = -1f;
                    }
                    else
                    {
                        NPC.ai[1] = 1f;
                    }
                }
            }
            else if (NPC.ai[0] == 2f)
            {
                float num1293 = 0.4f;
                float scaleFactor13 = 0.95f;
                float num1294 = 5f;
                num1293 = 0.3f;
                num1294 = 7f;
                scaleFactor13 = 0.9f;
                NPC.velocity.Y = NPC.velocity.Y + NPC.ai[1] * num1293;
                if (NPC.velocity.Length() > num1294)
                {
                    NPC.velocity *= scaleFactor13;
                }
                if (NPC.velocity.X > -1f && NPC.velocity.X < 1f)
                {
                    NPC.TargetClosest(true);
                    NPC.ai[0] = 3f;
                    NPC.ai[1] = NPC.direction;
                }
            }
            else if (NPC.ai[0] == 3f)
            {
                float num1295 = 0.4f;
                float num1296 = 0.2f;
                float num1297 = 5f;
                float scaleFactor14 = 0.95f;
                num1295 = 0.6f;
                num1296 = 0.3f;
                num1297 = 7f;
                scaleFactor14 = 0.9f;
                NPC.velocity.X = NPC.velocity.X + NPC.ai[1] * num1295;
                if (NPC.Center.Y > Main.player[NPC.target].Center.Y)
                {
                    NPC.velocity.Y = NPC.velocity.Y - num1296;
                }
                else
                {
                    NPC.velocity.Y = NPC.velocity.Y + num1296;
                }
                if (NPC.velocity.Length() > num1297)
                {
                    NPC.velocity *= scaleFactor14;
                }
                if (NPC.velocity.Y > -1f && NPC.velocity.Y < 1f)
                {
                    NPC.TargetClosest(true);
                    NPC.ai[0] = 0f;
                    NPC.ai[1] = NPC.direction;
                }
            }
            int num1298 = 10;
            for (int num1299 = 0; num1299 < 1; num1299++)
            {
                int num1300 = Dust.NewDust(NPC.position - new Vector2(num1298), NPC.width + num1298 * 2, NPC.height + num1298 * 2, DustID.GoldFlame, 0f, 0f, 100, default, 2f);
                Main.dust[num1300].noGravity = true;
                Main.dust[num1300].noLight = true;
            }
            return;
        }

        public override void OnKill()
        {
            for (int num468 = 0; num468 < 3; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(NPC.Center.X, NPC.Center.Y), NPC.width, 1, ModContent.DustType<Dusts.CthulhuDust>(), -NPC.velocity.X * 0.2f,
                    -NPC.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(NPC.Center.X, NPC.Center.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.CthulhuDust>(), -NPC.velocity.X * 0.2f,
                    -NPC.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.IsABestiaryIconDummy ? SpriteEffects.FlipVertically : NPC.SpriteEffectDirection(true), 0);
            return false;
        }
    }
}