using System;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.NPCs._BossZero
{
    [AutoloadBossHead]
    public class Taser : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gigataser");
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 70;
            NPC.damage = 40;
            NPC.defense = 90;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCHit4;
            NPC.lifeMax = 30000;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0.0f;
            NPC.buffImmune[20] = true;
            NPC.buffImmune[24] = true;
            NPC.buffImmune[39] = true;
            NPC.lavaImmune = true;
            NPC.netAlways = true;
            NPC.noGravity = true;
            NPC.knockBackResist = 0;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.damage = (int)(NPC.damage * .7f);
            NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<Zero>()))
            {
                return false;
            }
            return true;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            bool flag = NPC.life <= 0 || (!NPC.active && NPC.AnyNPCs(ModContent.NPCType<Zero>()));
            if (flag && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int ind = NPC.NewNPC(NPC.GetSource_Death(), (int)(NPC.position.X + (double)(NPC.width / 2)), (int)NPC.position.Y + (NPC.height / 2), ModContent.NPCType<TeslaHand>(), NPC.whoAmI, NPC.ai[0], NPC.ai[1], NPC.ai[2], NPC.ai[3], NPC.target);
                Main.npc[ind].Center = NPC.Center;
                Main.npc[ind].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()));
                Main.npc[ind].velocity *= 8f;
                Main.npc[ind].netUpdate2 = true; Main.npc[ind].netUpdate = true;
            }
        }


        public int body = -1;
        public float rotValue = -1f;
        public Vector2 pos;

        public override void AI()
        {
            NPC.TargetClosest();
            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(NPC.Center, ModContent.NPCType<Zero>(), 1000, null);
                if (npcID >= 0) body = npcID;
            }

            if (body == -1) return;

            NPC zero = Main.npc[body];
            if (zero == null || zero.life <= 0 || !zero.active || zero.type != ModContent.NPCType<Zero>()) { NPC.active = false; return; }

            for (int m = NPC.oldPos.Length - 1; m > 0; m--)
            {
                NPC.oldPos[m] = NPC.oldPos[m - 1];
            }
            NPC.oldPos[0] = NPC.position;

            int probeNumber = ((Zero)zero.ModNPC).WeaponCount;
            if (rotValue == -1f) rotValue = NPC.ai[0] % probeNumber * ((float)Math.PI * 2f / probeNumber);
            rotValue += Main.expertMode ? .05f : 0f;
            while (rotValue > (float)Math.PI * 2f) rotValue -= (float)Math.PI * 2f;
            NPC.Center = BaseUtility.RotateVector(zero.Center, zero.Center + new Vector2(((Zero)zero.ModNPC).Distance, 0f), rotValue);

            if (Main.netMode != NetmodeID.MultiplayerClient) { NPC.ai[2]++; }

            Player player = Main.player[zero.target];

            int aiTimerFire = Main.expertMode ? 120 : 180;

            if (NPC.ai[2] == aiTimerFire)
            {
                NPC.ai[2] = 0;
                if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.Center, player.width, player.height))
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
                        for (int i = 0; i < 3; i++)
                        {
                            Vector2 vector83 = Vector2.Normalize(vector82.RotatedByRandom(0.78539818525314331)) * 20f;
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector83.X , vector83.Y, ModContent.ProjectileType<ZeroShock>(), NPC.damage / 2, 0f, Main.myPlayer, vector82.ToRotation(), ai);
                        }
                    }
                }
            }

            float NewRotation = NPC.AngleTo(Main.player[NPC.target].Center) - 1.57f;
            NPC.rotation = NPC.rotation.AngleLerp(NewRotation, 1f / 30f);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = TextureAssets.Npc[NPC.type].Value;
            Texture2D glowTex = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/TaserZ").Value;
            BaseDrawing.DrawAfterimage(spriteBatch, tex, 0, NPC, 1, 1, 6, true, 0, 0, Color.DarkRed, NPC.frame);
            BaseDrawing.DrawTexture(spriteBatch, tex, 0, NPC, drawColor);
            BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, NPC, AAColor.COLOR_WHITEFADE1);
            return false;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }
    }
}