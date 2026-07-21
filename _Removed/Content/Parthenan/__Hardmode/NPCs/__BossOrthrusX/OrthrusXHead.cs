using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;
using AAModClassic.Utilities;
using AAModClassic.UI.World;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossOrthrusX
{
    [AutoloadBossHead]
    public class OrthrusXHead : ModNPC
    {
        public float[] internalAI = new float[2];
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(internalAI[0]);
            writer.Write(internalAI[1]);
            writer.Write(reticalIndex);
            writer.Write(redHead);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            internalAI[0] = reader.ReadSingle();
            internalAI[1] = reader.ReadSingle();
            reticalIndex = reader.ReadInt16();
            redHead = reader.ReadBoolean();
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Orthrus X");
            Main.npcFrameCount[NPC.type] = 2;
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.lifeMax = 22000;
            NPC.width = 46;
            NPC.height = 46;
            NPC.damage = 40;
            NPC.npcSlots = 0;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            Music = MusicManagementSystem.MusicSlots["Siege"];
            NPC.dontCountMe = true;
            NPC.noTileCollide = true;
            NPC.boss = false;
            NPC.noGravity = true;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override void OnKill()
        {
            if (!Main.dedServ)
            {
                if (redHead)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OrthrusHeadGoreR").Type, 1f);
                }
                else
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OrthrusHeadGoreB").Type, 1f);
                }
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OrthrusHeadGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OrthrusHeadGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OrthrusHeadGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OrthrusHeadGore4").Type, 1f);
            }
            Body?.NPC.StrikeInstantKill();
        }

        public OrthrusXBody Body => BodyNPC != null && BodyNPC.ModNPC is OrthrusXBody body ? body : null;
        public NPC BodyNPC => Main.npc[(int)NPC.ai[0]];
        public OrthrusXHead_OrthrusReticle Reticle { 
            get 
            {
                return reticalIndex == -1 || Main.npc[reticalIndex].ModNPC is not OrthrusXHead_OrthrusReticle ? null : 
                       Main.npc[reticalIndex].ModNPC as OrthrusXHead_OrthrusReticle;
            } 
            set 
            {
                reticalIndex = (short)value.NPC.whoAmI;
            } 
        }
        private short reticalIndex = -1;
        public bool redHead = false;

        public const int distFromBodyX = 60; //how far from the body to centeralize the movement points. (X coord)
        public const int distFromBodyY = 90; //how far from the body to centeralize the movement points. (Y coord)
        public const int movementVariance = 40; //how far from the center point to move.

        public override void AI()
        {
            NPC.TargetClosest();
            
            if (BodyNPC == null || !BodyNPC.active)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) //force a kill to prevent 'ghosting'
                {
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
                return;
            }

            NPC.realLife = BodyNPC.whoAmI;
            NPC.timeLeft = 100;

            Player targetPlayer = Main.player[NPC.target];
            if (!targetPlayer.active || targetPlayer.dead || Main.dayTime) //fleeing
            {
                if (NPC.position.Y + NPC.velocity.Y <= 0f && Main.netMode != NetmodeID.MultiplayerClient) 
                { 
                    NPC.active = false; 
                    NPC.netUpdate = true; 
                }
                return;
            }

            if (NPC.ai[1] == OrthrusXBody.AISTATE_TURRET)
            {
                NPC.TargetClosest();
                if (targetPlayer == null || !targetPlayer.active || targetPlayer.dead) 
                    targetPlayer = null; //deliberately set to null

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.localAI[1]++;
                    internalAI[0]++;
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && !redHead && (Reticle == null || Reticle.NPC.type != ModContent.NPCType<OrthrusXHead_OrthrusReticle>() || Reticle.NPC.active == false) && internalAI[0] % 300 >= 150)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            NPC npc = NPC.NewNPCDirect(NPC.GetSource_FromThis(), (int)targetPlayer.Center.X, (int)targetPlayer.Center.Y, ModContent.NPCType<OrthrusXHead_OrthrusReticle>(), 0);
                            Reticle = npc.ModNPC as OrthrusXHead_OrthrusReticle;
                            Reticle.NPC.netUpdate = true;
                            Reticle.NPC.ai[0] = NPC.whoAmI;
                            Reticle.NPC.target = NPC.target;
                        }
                        NPC.netUpdate = true;
                    }

                    if (targetPlayer != null)
                    {
                        Vector2 dir = Vector2.Normalize(targetPlayer.Center - NPC.Center);
                        if (Reticle != null)
                            dir = Vector2.Normalize(Reticle.NPC.Center - NPC.Center);

                        if (redHead)
                        {
                            dir *= 12f;
                            if (internalAI[0] % 10 == 0)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, dir.X, dir.Y, ModContent.ProjectileType<OrthrusXHead_Spark>(), 20, 0f, -1);
                            }
                        }
                        else
                        {
                            if (internalAI[0] % 300 == 0)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, dir.X, dir.Y, ModContent.ProjectileType<OrthrusXHead_ShockingBreath>(), 20, 0f, -1);
                            }
                        }
                    }
                   
                    if (NPC.localAI[1] >= 200) //pick random spot to move head to
                    {
                        NPC.localAI[1] = 0;
                        NPC.ai[2] = Main.rand.Next(-movementVariance, movementVariance);
                        NPC.ai[3] = Main.rand.Next(-movementVariance, movementVariance);
                        NPC.netUpdate = true;
                    }
                }
                Vector2 nextTarget = BodyNPC.Center + new Vector2(redHead ? -distFromBodyX : distFromBodyX, -distFromBodyY) + new Vector2(NPC.ai[2], NPC.ai[3]);
                if (Vector2.Distance(nextTarget, NPC.Center) < 40f)
                {
                    NPC.velocity *= 0.9f;
                    if (Math.Abs(NPC.velocity.X) < 0.05f) NPC.velocity.X = 0f;
                    if (Math.Abs(NPC.velocity.Y) < 0.05f) NPC.velocity.Y = 0f;
                }
                else
                {
                    NPC.velocity = Vector2.Normalize(nextTarget - NPC.Center);
                    NPC.velocity *= 5f;
                }
                NPC.position += BodyNPC.oldPos[0] - BodyNPC.position;
                NPC.position += BodyNPC.velocity;
            }
            else
            {
                NPC.velocity = default;
                NPC.position += BodyNPC.velocity;
            }
            NPC.position += Body.NPC.position - Body.NPC.oldPosition;
            NPC.rotation = 1.57f;
            NPC.spriteDirection = -1;
            BaseDrawing.AddLight(NPC.Center, redHead ? new Color(255, 84, 84) : new Color(48, 232, 232));
        }


        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public float moveSpeed = 16f; 
        public void MoveToPoint(Vector2 point)
        {
            float velMultiplier = 1f;
            Vector2 dist = point - NPC.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            NPC.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            NPC.velocity *= moveSpeed;
            NPC.velocity *= velMultiplier;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            return false;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }

        public override bool PreKill()
        {
            return true;
        }
    }
}
