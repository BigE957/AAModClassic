using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic.NPCs.Bosses.Athena.Olympian
{
	public class OwlRuneCharged : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Charged Owl Rune");
            Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.alpha = 255;
            NPC.dontTakeDamage = true;
            NPC.lifeMax = 1;
            NPC.aiStyle = NPCAIStyleID.FaceClosestPlayer;
            NPC.damage = Main.expertMode ? 50 : 84;
            NPC.defense = Main.expertMode ? 1 : 1;
            NPC.knockBackResist = 0.2f;
            NPC.width = 82;
            NPC.height = 82;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.damage = 90;
        }

        public override void AI()
        {
            if (NPC.localAI[1] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item121, NPC.position);
                NPC.localAI[1] = 1f;
            }
            if (NPC.ai[0] < 180f)
            {
                NPC.alpha -= 5;
                if (NPC.alpha < 0)
                {
                    NPC.alpha = 0;
                }
            }
            else
            {
                NPC.alpha += 5;
                if (NPC.alpha > 255)
                {
                    NPC.alpha = 255;
                    NPC.active = false;
                    return;
                }
            }
            NPC.ai[0] += 1f;
            if (NPC.ai[0] % 30f == 0f && NPC.ai[0] < 180f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int[] array4 = new int[5];
                Vector2[] array5 = new Vector2[5];
                int num838 = 0;
                float num839 = 2000f;
                for (int num840 = 0; num840 < 255; num840++)
                {
                    if (Main.player[num840].active && !Main.player[num840].dead)
                    {
                        Vector2 center9 = Main.player[num840].Center;
                        float num841 = Vector2.Distance(center9, NPC.Center);
                        if (num841 < num839 && Collision.CanHit(NPC.Center, 1, 1, center9, 1, 1))
                        {
                            array4[num838] = num840;
                            array5[num838] = center9;
                            if (++num838 >= array5.Length)
                            {
                                break;
                            }
                        }
                    }
                }
                for (int num842 = 0; num842 < num838; num842++)
                {
                    Vector2 vector82 = array5[num842] - NPC.Center;
                    float ai = Main.rand.Next(100);
                    Vector2 vector83 = Vector2.Normalize(vector82.RotatedByRandom(0.78539818525314331)) * 14f;
                    Projectile.NewProjectile(NPC.Center.X, NPC.Center.Y, vector83.X, vector83.Y, ModContent.ProjectileType<AthenaShock>(), NPC.damage, 0f, Main.myPlayer, vector82.ToRotation(), ai);
                }
            }
            Lighting.AddLight(NPC.Center, 0f, 0.85f, 0.9f);
            if (NPC.alpha < 150 && NPC.ai[0] < 180f)
            {
                for (int num843 = 0; num843 < 1; num843++)
                {
                    float num844 = (float)Main.rand.NextDouble() * 1f - 0.5f;
                    if (num844 < -0.5f)
                    {
                        num844 = -0.5f;
                    }
                    if (num844 > 0.5f)
                    {
                        num844 = 0.5f;
                    }
                    Vector2 value47 = new Vector2(-NPC.width * 0.2f * NPC.scale, 0f).RotatedBy(num844 * 6.28318548f, default).RotatedBy(NPC.velocity.ToRotation(), default);
                    int num845 = Dust.NewDust(NPC.Center - Vector2.One * 5f, 10, 10, DustID.Electric, -NPC.velocity.X / 3f, -NPC.velocity.Y / 3f, 150, Color.Transparent, 0.7f);
                    Main.dust[num845].position = NPC.Center + value47;
                    Main.dust[num845].velocity = Vector2.Normalize(Main.dust[num845].position - NPC.Center) * 2f;
                    Main.dust[num845].noGravity = true;
                }
                for (int num846 = 0; num846 < 1; num846++)
                {
                    float num847 = (float)Main.rand.NextDouble() * 1f - 0.5f;
                    if (num847 < -0.5f)
                    {
                        num847 = -0.5f;
                    }
                    if (num847 > 0.5f)
                    {
                        num847 = 0.5f;
                    }
                    Vector2 value48 = new Vector2(-NPC.width * 0.6f * NPC.scale, 0f).RotatedBy(num847 * 6.28318548f, default).RotatedBy(NPC.velocity.ToRotation(), default);
                    int num848 = Dust.NewDust(NPC.Center - Vector2.One * 5f, 10, 10, DustID.Electric, -NPC.velocity.X / 3f, -NPC.velocity.Y / 3f, 150, Color.Transparent, 0.7f);
                    Main.dust[num848].velocity = Vector2.Zero;
                    Main.dust[num848].position = NPC.Center + value48;
                    Main.dust[num848].noGravity = true;
                }
                return;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (++NPC.frameCounter >= 4)
            {
                NPC.frame.Y += frameHeight;
                NPC.frameCounter = 0;
                if (NPC.frame.Y >= frameHeight * 3)
                {
                    NPC.frame.Y = 0;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            BaseDrawing.DrawTexture(sb, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 7, NPC.frame, NPC.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            return false;
        }
    }
}