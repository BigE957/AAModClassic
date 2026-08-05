using AAModClassic._Content.Desert.___PreHardmode.NPCs.__BossDesertDjinn;
using AAModClassic.Music;
using AAModClassic.Particles;
using AAModClassic.Particles.Types;
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

namespace AAModClassic._Unofficial.Desert.NPCs._BossDesertDjinn
{
    [AutoloadBossHead]
    public class DesertDjinn_Unofficial : ModNPC
    {
        public override string BossHeadTexture => "AAModClassic/_Content/Desert/___PreHardmode/NPCs/__BossDesertDjinn/DesertDjinn_Head_Boss";

        public int Exhaustion = 0;
        public static int ExhaustionCap => Main.masterMode ? 8 : Main.expertMode ? 7 : 5;

        public enum DjinnState
        {
            Spawn,
            RecoverFlex,
            GrandSlam,
            TwisterPunch,
            SubmergedUppercut,
            Dive
        }

        public DjinnState CurrentState { get => (DjinnState)NPC.ai[0]; set => NPC.ai[0] = (float)value; }
        public ref float Timer => ref NPC.ai[1];
        public bool AttackFlag = false;

        public Player Target => Main.player[NPC.target];

        private int FrameX = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Desert Djinn");
            Main.npcFrameCount[NPC.type] = 9;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(ModContent.NPCType<DesertDjinn>());
            Music = MusicManagementSystem.MusicSlots["Djinn"];
        }

        public override void AI()
        {
            switch (CurrentState)
            {
                case DjinnState.Spawn:
                    NPC.TargetClosest();
                    NPC.velocity = ((Target.Center - Vector2.UnitY * 96f) - NPC.Center) / 90f;
                    if(Timer > 120)
                    {
                        CurrentState = DjinnState.GrandSlam;
                        Timer = 0;
                        return;
                    }
                    break;
                case DjinnState.RecoverFlex:
                    break;
                case DjinnState.GrandSlam:
                    float gravity = 0.6f;
                    if(Timer == 0)
                    {
                        NPC.TargetClosest();
                        Vector2 targetPos = Target.Center + Target.velocity * 4f;
                        if (MathUtils.TryGetLaunchVelocity(targetPos - NPC.Center, 12f, gravity, out Vector2 velocity))
                        {
                            NPC.velocity = velocity;
                            FrameX = 4;
                        }
                        else
                        {
                            CurrentState = DjinnState.Spawn;
                            Timer--;
                        }
                    }
                    else
                    {
                        if (!AttackFlag)
                        {
                            NPC.velocity.Y += gravity;

                            if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                            {
                                NPC.velocity *= new Vector2(-0.75f, -0.5f);
                                AttackFlag = true;
                                Timer = 0;
                            }
                            else if(Collision.SolidCollision(NPC.position + NPC.velocity, NPC.width, NPC.height))
                            {
                                SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact, NPC.Center);

                                for(int i = -3; i <= 3; i++)
                                {
                                    Point s = NPC.Center.ToTileCoordinates() - new Point(-i * NPC.direction, 8);
                                    Point g = CollisionUtils.FindSurfaceBelow(s, true);
                                    WorldGen.KillTile(g.X, g.Y, effectOnly: true);
                                }

                                Point start = NPC.Center.ToTileCoordinates() - new Point(-2 * NPC.direction, 8);
                                Point ground = CollisionUtils.FindSurfaceBelow(start, true);
                                ParticleSystem.SpawnParticle(new GroundWave(ground, 8, 24, NPC.direction == 1, 108, 2, 16), DrawLayer.AfterPlayers);

                                start = NPC.Center.ToTileCoordinates() - new Point(2 * NPC.direction, 8);
                                ground = CollisionUtils.FindSurfaceBelow(start, true);
                                ParticleSystem.SpawnParticle(new GroundWave(ground, 2, 10, NPC.direction != 1, 24, 3, 16), DrawLayer.AfterPlayers);
                            }
                        }
                        else
                        {
                            NPC.velocity *= new Vector2(0.925f, 0.85f);

                            if (Timer > 30 || (MathF.Abs(NPC.velocity.X) < 0.01f && MathF.Abs(NPC.velocity.Y) < 0.01f))
                            {
                                FrameX = 0;
                                CurrentState = DjinnState.Spawn;
                                Timer = 0;
                                AttackFlag = false;
                                return;
                            }
                        }
                    }
                    break;
                case DjinnState.TwisterPunch:
                    break;
                case DjinnState.SubmergedUppercut:
                    break;
                case DjinnState.Dive:
                    break;
            }

            Timer++;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            NPC.position.X = NPC.position.X + NPC.width / 2;
            NPC.position.Y = NPC.position.Y + NPC.height / 2;
            NPC.position.X = NPC.position.X - NPC.width / 2;
            NPC.position.Y = NPC.position.Y - NPC.height / 2;
            int dust = ModContent.DustType<Dusts.SandDust>();
            for (int Loop = 0; Loop < 5; Loop++)
            {
                int d = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust, 0f, 0f, 0);
                Main.dust[d].velocity.Y = hit.HitDirection * 0.1F;
                Main.dust[d].noGravity = false;
            }
            if (NPC.life <= 0)
            {
                if (!Main.dedServ)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("DjinnGore1").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("DjinnGore2").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("DjinnGore3").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("DjinnGore4").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("DjinnGore5").Type, 1f);
                }
                for (int Loop = 0; Loop < 60; Loop++)
                {
                    int d = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust, 0f, 0f, 0);
                    Main.dust[d].velocity.X *= 0f;
                    Main.dust[d].noGravity = false;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Width = TextureAssets.Npc[NPC.type].Width() / 6;
            NPC.frame.X = FrameX * NPC.frame.Width;
            NPC.frameCounter++;

            if (NPC.frameCounter > 5)
            {
                NPC.frame.Y += frameHeight;
                NPC.frameCounter = 0;
            }

            int frameCap = FrameX switch
            {
                0 => 6,
                1 => 6,
                2 => 4,
                3 => 4,
                4 => 1,
                5 => 9,
                _ => 1
            };

            if (NPC.frame.Y / frameHeight >= frameCap)
                NPC.frame.Y = 0;

        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            NPC.spriteDirection = NPC.direction;

            if (!Target.ZoneDesert)
                DrawingUtils.DrawAfterimageWithVelocity(spriteBatch, texture, NPC.Center - Main.screenPosition, NPC.velocity, 7, NPC.frame, Color.Goldenrod, NPC.scale, [NPC.rotation], NPC.frame.Size() * 0.5f, NPC.SpriteEffectDirection());

            spriteBatch.Draw(texture, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() / 2f, NPC.scale, NPC.SpriteEffectDirection(), 0);

            return false;
        }
    }
}
