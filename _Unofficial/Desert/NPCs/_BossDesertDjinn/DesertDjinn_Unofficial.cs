using AAModClassic._Content.Desert.___PreHardmode.NPCs.__BossDesertDjinn;
using AAModClassic.Music;
using AAModClassic.Particles;
using AAModClassic.Particles.Types;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Desert.NPCs._BossDesertDjinn
{
    [AutoloadBossHead]
    public class DesertDjinn_Unofficial : ModNPC
    {
        public override string BossHeadTexture => "AAModClassic/_Content/Desert/___PreHardmode/NPCs/__BossDesertDjinn/DesertDjinn_Head_Boss";

        public int Exhaustion = 0;
        public int ExhaustionCap 
        { 
            get 
            {
                if (Main.expertMode && Phase2) //P2 Expert+ (Infinite)
                    return int.MaxValue;

                if (Main.masterMode || Phase2) //P1 Master or P2 Normal
                    return 9;

                if (Main.expertMode) //P1 Expert
                    return 7;

                return 6;
            } 
        }

        public enum DjinnState
        {
            Spawn,
            PhaseSwitch,
            Defeat,
            RecoverFlex,
            GrandSlam,
            TwisterPunch,
            SubmergedUppercut,
            Dive,
            MudaMuda,
            CactusBaseball
        }

        public DjinnState CurrentState { get => (DjinnState)NPC.ai[0]; set => NPC.ai[0] = (float)value; }
        public DjinnState PreviousStartingState = DjinnState.Spawn;
        public ref float Time => ref NPC.ai[3];
        public bool AttackFlag = false;
        public bool AttemptFailedFlag = false;
        public Vector2 AttackVector = Vector2.Zero;
        public int AttackCounter = 0;
        public int AttackAmount = -1;
        public float AttackAngle = 0f;
        public bool Phase2 = false;

        #region Balancing Values
        // Recovery Flex
        public static int RecoverTime => Main.masterMode ? 180 : Main.expertMode ? 240 : 270;

        // Grand Slam
        public static int GrandSlamWaveDamage => 20;

        // Twister Punch
        public int TwisterDamage => 20;
        public int TwisterDelay => 30;
        public static float TwisterOffset => 96f;
        public int TwisterPunchDelay => Phase2 ? 48 : 60;
        public static float TwisterPunchOffset => 192f;
        public int TwisterPunchDuration => Phase2 ? 30 : 60;
        public float TwisterPunchSpeed => Phase2 ? 48f : 24f;
        public float TwisterPunchDecay => Phase2 ? 0.9f : 0.95f;

        // Submerged Uppercut
        public static int FallTime => 5;
        public int BurrowTime => Phase2 ? 25 : 55;
        public int HoldTime => Phase2 ? 20 : 30;
        public float UppercutSpeed => 24f;
        public float UppercutDecay => 0.9f;
        public static float UppercutRotationDecay => 0.92f;

        // Dive
        public static float GroundOffset => 360;
        public static float PlayerOffset => 240;
        public int DiveRepositionTime => 30;
        public int DiveTargettingTime => 10;
        public int DiveMercyTime => 30;
        public int DiveDelay => 20;
        public float DiveSpeed => Phase2 ? 48f : 32f;
        public static int DiveWaveDamage => 10;
        public int DiveStuckTime => Main.masterMode ? 10 : Main.expertMode ? 15 : 20;
        public static float DiveRotationDecay => 0.9f;
        public int DiveSegwayTime => 60;

        // Muda Muda
        public int MudaMudaRepositionTime => Phase2 ? 20 : 30;
        public int MudaMudaDelay => Phase2 ? (Main.masterMode ? 10 : 15) : 10;
        public int GetMudaMudaAmount() => Phase2 ? Main.rand.Next(2, 4) : (Main.rand.NextBool() ? 2 : 1);
        public static float MudaMudaOffset => 128f;
        public float MudaMudaSpeed => Phase2 ? 20f : 10f;

        public int MudaMudaDuration => Phase2 ? 25 : 40;
        #endregion

        public Player Target => Main.player[NPC.target];

        private int FrameX = 0;
        private Vector2 DrawOffset = Vector2.Zero;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Desert Djinn");
            Main.npcFrameCount[NPC.type] = 9;
            NPCID.Sets.TrailingMode[Type] = 3;
            NPCID.Sets.TrailCacheLength[Type] = 10;

            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(ModContent.NPCType<DesertDjinn>());
            Music = MusicManagementSystem.MusicSlots["Djinn"];
            NPC.npcSlots = 6f;
        }

        public override void AI()
        {
            NPC.hide = CurrentState == DjinnState.SubmergedUppercut;

            ManageSandstormffects();

            switch (CurrentState)
            {
                case DjinnState.Spawn:
                    NPC.TargetClosest();
                    NPC.velocity = ((Target.Center - Vector2.UnitY * 96f) - NPC.Center) / 90f;
                    if(Time > 120)
                    {
                        CurrentState = DjinnState.MudaMuda;
                        Time = 0;
                        Exhaustion++;
                        return;
                    }
                    break;
                case DjinnState.PhaseSwitch:
                    NPC.hide = true;

                    if (!AttackFlag)
                    {
                        FrameX = 0;
                        DrawOffset = Vector2.Zero;

                        if (Time == 0)
                            NPC.velocity = new Vector2(-4 * NPC.direction, -8f);
                        else
                            NPC.velocity.Y += 0.5f;

                        NPC.rotation = NPC.velocity.ToRotation() + MathHelper.Pi;
                        if (NPC.direction == -1)
                            NPC.rotation = MathHelper.WrapAngle(NPC.rotation + MathHelper.Pi);

                        if (NPC.velocity.Y > 0 && Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                        {
                            SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact, NPC.Center);
                            Time = 0;
                            AttackFlag = true;
                            NPC.Center += NPC.velocity + Vector2.UnitY * 24f;
                            NPC.velocity = Vector2.Zero;
                            NPC.dontTakeDamage = true;

                            for (int i = -3; i <= 3; i++)
                            {
                                Point s = NPC.Center.ToTileCoordinates() - new Point(-i * NPC.direction, 8);
                                Point g = CollisionUtils.FindSurfaceBelow(s);
                                WorldGen.KillTile(g.X, g.Y, effectOnly: true);
                            }

                            Point start = NPC.Center.ToTileCoordinates() - new Point(-2 * NPC.direction, 8);
                            Point ground = CollisionUtils.FindSurfaceBelow(start);
                            GroundWave particle = new(ground, 8, NPC.direction == 1, 24, 2, 16, 0.5f);
                            ParticleSystem.SpawnParticle(particle, DrawLayer.AfterPlayers);

                            start = NPC.Center.ToTileCoordinates() - new Point(2 * NPC.direction, 8);
                            ground = CollisionUtils.FindSurfaceBelow(start);
                            ParticleSystem.SpawnParticle(new GroundWave(ground, 8, NPC.direction != 1, 24, 2, 16, 0.5f), DrawLayer.AfterPlayers);

                            start = NPC.Center.ToTileCoordinates() - new Point(0, 8);
                            ground = CollisionUtils.FindSurfaceBelow(start);
                            for (int i = -4; i <= 4; i++)
                            {
                                Point spawnTile = CollisionUtils.FindSurfaceAround(ground + new Point(i, 0), true);
                                WorldGen.KillTile(spawnTile.X, spawnTile.Y, effectOnly: true);
                                if (Framing.GetTileSafely(spawnTile).TileType == TileID.Sand)
                                {
                                    Vector2 spawnPos = spawnTile.ToWorldCoordinates();
                                    for (int j = 0; j < 3; j++)
                                    {
                                        LargeDust d = new(spawnPos, new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-1, -1 - j)), new Color(212, 192, 100), new Color(212, 192, 100) * 0.5f, Main.rand.NextFloat(0.75f, 1.5f), 200, Main.rand.NextFloat(0.01f, 0.05f));
                                        ParticleSystem.SpawnParticle(d, DrawLayer.AfterPlayers);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //Count out
                        switch(Time)
                        {
                            case 60:
                                CombatText.NewText(NPC.Hitbox, Color.Goldenrod, "10!");
                                break;
                            case 120:
                                CombatText.NewText(NPC.Hitbox, Color.Goldenrod, "9!");
                                break;
                            case 180:
                                CombatText.NewText(NPC.Hitbox, Color.Goldenrod, "8!");
                                break;
                            case 240:
                                CombatText.NewText(NPC.Hitbox, Color.Goldenrod, "7!");
                                break;
                            case 300:
                                CombatText.NewText(NPC.Hitbox, Color.Goldenrod, "6!");
                                break;
                            case 360:
                                CombatText.NewText(NPC.Hitbox, Color.Goldenrod, "5!");
                                break;
                            case 420:
                                CombatText.NewText(NPC.Hitbox, Color.Goldenrod, "4!");
                                break;
                            case 480:
                                CombatText.NewText(NPC.Hitbox, Color.Goldenrod, "3-");
                                break;
                        }

                        if(Time >= 360)
                        {
                            if (Time >= 480)
                            {
                                DrawOffset = Vector2.Zero;
                                if (Time == 480)
                                {
                                    Phase2 = true;
                                    NPC.velocity = Vector2.UnitY * -48f;
                                    FrameX = 0;
                                }
                                else
                                {
                                    NPC.velocity *= 0.75f;
                                    NPC.rotation *= 0.9f;

                                    if (Time == 540)
                                    {
                                        Time = 30;
                                        AttackFlag = false;
                                        NPC.rotation = 0f;
                                        NPC.hide = false;
                                        FrameX = 0;
                                        CurrentState = DjinnState.RecoverFlex;
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                NPC.hide = true;
                                DrawOffset = Main.rand.NextVector2Circular(8, 8) * ((Time - 360) / 120f);
                            }
                        }
                        else
                            NPC.hide = true;
                    }
                    break;
                case DjinnState.RecoverFlex:
                    NPC.TargetClosest();

                    int trueRecoveryTime = Phase2 ? 120 : RecoverTime;

                    if (Time < trueRecoveryTime)
                    {
                        NPC.velocity *= 0.95f;
                        if(!Phase2 && Time <= 30f)
                            NPC.Center = Vector2.Lerp(NPC.Center, CollisionUtils.FindSurfaceBelow(NPC.Center.ToTileCoordinates(), true).ToWorldCoordinates(8f, -NPC.height), Time / 30f);
                    }
                    else
                        NPC.velocity = ((Target.Center - Vector2.UnitY * 96f) - NPC.Center) / 90f;

                    if (Time >= 30 && Time < trueRecoveryTime)
                    {               
                        if (Time == 30)
                        {
                            NPC.frameCounter = -1;
                            NPC.frame.Y = Main.rand.Next(3) * NPC.frame.Height * 3;
                        }
                        FrameX = 5;
                    }
                    else
                        FrameX = 0;

                    NPC.damage = 0;
                    if (Time >= trueRecoveryTime + 60)
                    {
                        if (NPC.life / (float)NPC.lifeMax <= 0.5f)
                            Phase2 = true;

                        NPC.damage = NPC.defDamage;
                        List<DjinnState> options = [DjinnState.GrandSlam, DjinnState.TwisterPunch, DjinnState.SubmergedUppercut, DjinnState.Dive, DjinnState.CactusBaseball];
                        options.Remove(PreviousStartingState);

                        CurrentState = options[Main.rand.Next(options.Count)];
                        PreviousStartingState = CurrentState;
                        AttackFlag = false;
                        Time = 0;
                        AttackCounter = 0;
                        Exhaustion = CurrentState == DjinnState.GrandSlam ? 2 : 1;
                        NPC.immortal = false;
                        NPC.dontTakeDamage = false;
                        NPC.netUpdate = true;
                        return;
                    }
                    break;
                case DjinnState.GrandSlam:
                    float gravity = 0.6f;
                    if(!AttackFlag && Time == 0)
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
                            CurrentState = DjinnState.Dive;
                            Exhaustion -= 1;
                            Time--;
                        }
                    }
                    else
                    {
                        if (!AttackFlag)
                        {
                            NPC.velocity.Y += gravity;

                            if(Time > 10 && NPC.velocity.Y > 0 && (NPC.Bottom.Y + NPC.velocity.Y) > Target.Bottom.Y && CollisionUtils.SurfaceCollision(NPC.position + NPC.velocity, NPC.width, NPC.height))
                            {
                                SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact, NPC.Center);
                                Time = 0;
                                AttackFlag = true;

                                for (int i = -3; i <= 3; i++)
                                {
                                    Point s = NPC.Center.ToTileCoordinates() - new Point(-i * NPC.direction, 8);
                                    Point g = CollisionUtils.FindSurfaceBelow(s);
                                    WorldGen.KillTile(g.X, g.Y, effectOnly: true);
                                }

                                Point start = NPC.Center.ToTileCoordinates() - new Point(-2 * NPC.direction, 8);
                                Point ground = CollisionUtils.FindSurfaceBelow(start);
                                GroundWave particle = new(ground, 32, NPC.direction == 1, 162, 1, 24, strong: true);
                                ParticleSystem.SpawnParticle(particle, DrawLayer.AfterPlayers);
                                int dir = (NPC.direction == 1 ? 1 : -1);

                                Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), ground.ToWorldCoordinates(), new Vector2(13.75f * dir, 0f), ModContent.ProjectileType<GroundwaveHurt>(), GrandSlamWaveDamage, 0f, ai1: 10, ai2: 150);
                                proj.timeLeft = particle.Lifetime - 12;
                                proj.position.Y -= 25;

                                start = NPC.Center.ToTileCoordinates() - new Point(2 * NPC.direction, 8);
                                ground = CollisionUtils.FindSurfaceBelow(start);
                                ParticleSystem.SpawnParticle(new GroundWave(ground, 10, NPC.direction != 1, 24, 3, 16, 0.5f), DrawLayer.AfterPlayers);

                                start = NPC.Center.ToTileCoordinates() - new Point(0, 8);
                                ground = CollisionUtils.FindSurfaceBelow(start);
                                for (int i = -6; i <= 6; i++)
                                {
                                    Point spawnTile = CollisionUtils.FindSurfaceAround(ground + new Point(i, 0), true);
                                    WorldGen.KillTile(spawnTile.X, spawnTile.Y, effectOnly: true);
                                    if (Framing.GetTileSafely(spawnTile).TileType == TileID.Sand)
                                    {
                                        Vector2 spawnPos = spawnTile.ToWorldCoordinates();
                                        for (int j = 0; j < 4; j++)
                                        {
                                            LargeDust d = new(spawnPos, new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, -3 -(j * 2))), new Color(212, 192, 100), new Color(212, 192, 100) * 0.5f, Main.rand.NextFloat(0.75f, 1.5f), 200, Main.rand.NextFloat(0.01f, 0.05f));
                                            ParticleSystem.SpawnParticle(d, DrawLayer.AfterPlayers);
                                        }
                                    }
                                }

                                bool tooFar = NPC.DistanceSQ(Target.Center) > 640000;
                                if (tooFar || (Exhaustion + 1 < ExhaustionCap && Collision.SolidCollision(NPC.position + NPC.velocity, NPC.width, NPC.height))) //800^2
                                {
                                    if (tooFar || Main.rand.NextBool())
                                    {
                                        Time = 0;
                                        CurrentState = DjinnState.SubmergedUppercut;
                                        AttackFlag = true;
                                        Exhaustion++;
                                    }
                                    else
                                        AttemptFailedFlag = true;
                                }
                                return;
                            }
                        }
                        else
                        {
                            if(Time == 0)
                                NPC.velocity *= new Vector2(-0.75f, -0.5f);
                            else
                                NPC.velocity *= new Vector2(0.925f, 0.85f);

                            if (Time > 30 || (MathF.Abs(NPC.velocity.X) < 0.01f && MathF.Abs(NPC.velocity.Y) < 0.01f))
                            {
                                Time = 0;
                                AttackFlag = false;
                                AttackCounter = 0;

                                if (Exhaustion + 1 < ExhaustionCap)
                                {
                                    if (!AttemptFailedFlag)
                                    {
                                        DjinnState[] options = [DjinnState.SubmergedUppercut, DjinnState.MudaMuda, DjinnState.CactusBaseball];
                                        CurrentState = options[Main.rand.Next(options.Length)];
                                    }
                                    else
                                        CurrentState = Main.rand.NextBool() ? DjinnState.CactusBaseball : DjinnState.MudaMuda;
                                    Exhaustion++;
                                }
                                else
                                {
                                    FrameX = 0;
                                    CurrentState = DjinnState.RecoverFlex;
                                }
                                AttemptFailedFlag = false;
                                return;
                            }
                        }
                    }
                    break;
                case DjinnState.TwisterPunch:
                    if(Time == TwisterDelay)
                    { 
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Target.Top - Vector2.UnitX * TwisterOffset, Vector2.Zero, ModContent.ProjectileType<DesertDjinn_Djinnado>(), TwisterDamage, 1f, ai2: TwisterPunchDelay);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Target.Top + Vector2.UnitX * TwisterOffset, Vector2.Zero, ModContent.ProjectileType<DesertDjinn_Djinnado>(), TwisterDamage, 1f, ai2: TwisterPunchDelay);
                    }
                    else if(Time < TwisterDelay)
                    {
                        if (Time == 0)
                            FrameX = 2;

                        Point hitPos = Point.Zero;
                        int mod = (int)Time % 4;
                        if (mod == 0)
                            hitPos = CollisionUtils.FindSurfaceBelow((Target.Center - Vector2.UnitX * TwisterOffset).ToTileCoordinates());
                        else if(mod == 2)
                            hitPos = CollisionUtils.FindSurfaceBelow((Target.Center + Vector2.UnitX * TwisterOffset).ToTileCoordinates());
                            
                        if(hitPos != Point.Zero)
                        {
                            int amt = Main.rand.Next(3, 5);
                            for (int i = 0; i < amt; i++)
                                Dust.NewDust(hitPos.ToWorldCoordinates(0, 0), 16, 16, DustID.Sandnado, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-12f, -4f), Scale: Main.rand.NextFloat(0.5f, 2f));
                        }
                    }

                    if (Time < TwisterDelay + TwisterPunchDelay)
                    {
                        NPC.TargetClosest();
                        int side = NPC.Center.X > Target.Center.X ? 1 : -1;
                        NPC.velocity.X = (Target.Center.X + (TwisterPunchOffset * side) - NPC.Center.X) / 10f;
                        float lerp = MathHelper.Clamp((Time - TwisterDelay) / (float)TwisterPunchDelay, 0f, 1f);
                        NPC.velocity.Y = (Target.Center.Y - NPC.Center.Y) * MathHelper.Lerp(Phase2 ? 0.2f : 0.1f, 0f, lerp);
                    }
                    else
                    {
                        int side = NPC.Center.X < Target.Center.X ? 1 : -1;
                        if (Time == TwisterDelay + TwisterPunchDelay)
                        {
                            SoundEngine.PlaySound(SoundID.Item1, NPC.Center);
                            NPC.velocity = Vector2.UnitX * TwisterPunchSpeed * side;
                            FrameX = 3;
                        }
                        else
                            NPC.velocity *= TwisterPunchDecay;
                    }

                    if (Time > TwisterDelay + TwisterPunchDelay + TwisterPunchDuration)
                    {
                        Time = 0;
                        AttackFlag = false;
                        AttackCounter = 0;

                        if (Main.expertMode && Phase2)
                        {
                            Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), Target.Center, Vector2.Zero, ProjectileID.SandnadoHostileMark, TwisterDamage, 1f).hostile = false;
                        }

                        if (Main.rand.NextBool() && Exhaustion + 2 < ExhaustionCap)
                        {
                            FrameX = 4;
                            CurrentState = DjinnState.GrandSlam;
                            Exhaustion += 2;
                        }
                        else if(Exhaustion + 1 < ExhaustionCap)
                        {
                            CurrentState = Main.rand.NextBool() ? DjinnState.CactusBaseball : DjinnState.SubmergedUppercut;
                            Exhaustion += 1;
                        }
                        else
                        {
                            FrameX = 0;
                            CurrentState = DjinnState.RecoverFlex;
                        }
                        return;
                    }
                    break;
                case DjinnState.SubmergedUppercut:
                    if(!AttackFlag)
                    {
                        if(Time == 0)
                        {
                            int side = NPC.Center.X > Target.Center.X ? 1 : -1;
                            NPC.velocity = new Vector2(4 * side, -8);
                            FrameX = 4;
                            NPC.TargetClosest();
                            NPC.direction *= -1;
                        }
                        else
                        {
                            NPC.velocity.Y += 0.6f;

                            if (NPC.velocity.Y > 0 && NPC.Bottom.Y > Target.Bottom.Y && Collision.SolidCollision(NPC.position + NPC.velocity, NPC.width, NPC.height))
                            {
                                SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact, NPC.Center);
                                Time = 0;
                                AttackFlag = true;

                                for (int i = -3; i <= 3; i++)
                                {
                                    Point s = NPC.Center.ToTileCoordinates() - new Point(-i * NPC.direction, 8);
                                    Point g = CollisionUtils.FindSurfaceBelow(s);
                                    WorldGen.KillTile(g.X, g.Y, effectOnly: true);
                                }

                                Point start = NPC.Center.ToTileCoordinates() - new Point(-2 * NPC.direction, 8);
                                Point ground = CollisionUtils.FindSurfaceBelow(start);
                                GroundWave particle = new(ground, 8, NPC.direction == 1, 24, 2, 16, 0.5f);
                                ParticleSystem.SpawnParticle(particle, DrawLayer.AfterPlayers);

                                start = NPC.Center.ToTileCoordinates() - new Point(2 * NPC.direction, 8);
                                ground = CollisionUtils.FindSurfaceBelow(start);
                                ParticleSystem.SpawnParticle(new GroundWave(ground, 8, NPC.direction != 1, 24, 2, 16, 0.5f), DrawLayer.AfterPlayers);

                                start = NPC.Center.ToTileCoordinates() - new Point(0, 8);
                                ground = CollisionUtils.FindSurfaceBelow(start);
                                for (int i = -4; i <= 4; i++)
                                {
                                    Point spawnTile = CollisionUtils.FindSurfaceAround(ground + new Point(i, 0), true);
                                    WorldGen.KillTile(spawnTile.X, spawnTile.Y, effectOnly: true);
                                    if (Framing.GetTileSafely(spawnTile).TileType == TileID.Sand)
                                    {
                                        Vector2 spawnPos = spawnTile.ToWorldCoordinates();
                                        for (int j = 0; j < 3; j++)
                                        {
                                            LargeDust d = new(spawnPos, new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-1, -1 - j)), new Color(212, 192, 100), new Color(212, 192, 100) * 0.5f, Main.rand.NextFloat(0.75f, 1.5f), 200, Main.rand.NextFloat(0.01f, 0.05f));
                                            ParticleSystem.SpawnParticle(d, DrawLayer.AfterPlayers);
                                        }
                                    }
                                }
                                return;
                            }
                        }
                    }
                    else
                    {
                        //Maintian velocity for a few frames to submerge
                        if(Time > FallTime)
                        {
                            if (Time < FallTime + BurrowTime)
                            {
                                NPC.Center = CollisionUtils.FindSurfaceBelow(Target.Center.ToTileCoordinates(), true).ToWorldCoordinates() + Vector2.UnitY * 196;
                                NPC.velocity = Vector2.Zero;

                                FrameX = 3;
                                NPC.rotation = MathHelper.PiOver2 * -NPC.direction;

                                Point surface = CollisionUtils.FindSurfaceBelow(NPC.Center.ToTileCoordinates(), true);
                                for(int i = -3; i <= 3; i++)
                                {
                                    if (Main.rand.NextFloat() < 0.9f)
                                        continue;

                                    Point p = CollisionUtils.FindSurfaceBelow(surface + new Point(i, 0));
                                    WorldGen.KillTile(p.X, p.Y, effectOnly: true);

                                    if (Framing.GetTileSafely(p).TileType == TileID.Sand)
                                    {
                                        LargeDust d = new(p.ToWorldCoordinates(), new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-1, -3)), new Color(212, 192, 100), new Color(212, 192, 100) * 0.5f, Main.rand.NextFloat(0.75f, 1.5f), 200, Main.rand.NextFloat(0.01f, 0.05f));
                                        ParticleSystem.SpawnParticle(d, DrawLayer.AfterPlayers);
                                    }
                                }
                            }
                            else if (Time > FallTime + BurrowTime + HoldTime)
                            {
                                if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                                {
                                    NPC.velocity = Vector2.UnitY * -UppercutSpeed;
                                    Point surface = CollisionUtils.FindSurfaceBelow(NPC.Center.ToTileCoordinates(), true);
                                    for (int i = -3; i <= 3; i++)
                                    {
                                        if (Main.rand.NextFloat() < 0.9f)
                                            continue;

                                        Point p = CollisionUtils.FindSurfaceBelow(surface + new Point(i, 0));
                                        WorldGen.KillTile(p.X, p.Y, effectOnly: true);
                                    }

                                    if (!Collision.SolidCollision(NPC.position + NPC.velocity, NPC.width, NPC.height))
                                    {
                                        SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact, NPC.Center);
                                        for (int i = -3; i <= 3; i++)
                                        {                                
                                            Point s = NPC.Center.ToTileCoordinates() - new Point(-i * NPC.direction, 8);
                                            Point g = CollisionUtils.FindSurfaceBelow(s, true);
                                            WorldGen.KillTile(g.X, g.Y, effectOnly: true);
                                        }

                                        Point start = NPC.Center.ToTileCoordinates() - new Point(-2 * NPC.direction, 8);
                                        Point ground = CollisionUtils.FindSurfaceBelow(start, true);
                                        GroundWave particle = new(ground, 8, NPC.direction == 1, 24, 2, 16, 0.5f);
                                        ParticleSystem.SpawnParticle(particle, DrawLayer.AfterPlayers);

                                        start = NPC.Center.ToTileCoordinates() - new Point(2 * NPC.direction, 8);
                                        ground = CollisionUtils.FindSurfaceBelow(start, true);
                                        ParticleSystem.SpawnParticle(new GroundWave(ground, 8, NPC.direction != 1, 24, 2, 16, 0.5f), DrawLayer.AfterPlayers);

                                        start = NPC.Center.ToTileCoordinates() - new Point(0, 8);
                                        ground = CollisionUtils.FindSurfaceBelow(start);
                                        for (int i = -4; i <= 4; i++)
                                        {
                                            Point spawnTile = CollisionUtils.FindSurfaceAround(ground + new Point(i, 0), true);
                                            WorldGen.KillTile(spawnTile.X, spawnTile.Y, effectOnly: true);
                                            if (Framing.GetTileSafely(spawnTile).TileType == TileID.Sand)
                                            {
                                                Vector2 spawnPos = spawnTile.ToWorldCoordinates();
                                                for (int j = 0; j < 5; j++)
                                                {
                                                    LargeDust d = new(spawnPos, new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-4, -5 -(j * 3))), new Color(212, 192, 100), new Color(212, 192, 100) * 0.5f, Main.rand.NextFloat(0.75f, 1.5f), 200, Main.rand.NextFloat(0.01f, 0.05f));
                                                    ParticleSystem.SpawnParticle(d, DrawLayer.AfterPlayers);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (NPC.Center.Y > Target.Bottom.Y + 16)
                                        NPC.velocity = Vector2.UnitY * -UppercutSpeed;
                                    else
                                        NPC.velocity *= UppercutDecay;

                                    NPC.rotation *= UppercutRotationDecay;
                                    if (Math.Abs(NPC.rotation) < 0.1f)
                                    {
                                        FrameX = 0;
                                        NPC.TargetClosest();

                                        if (Exhaustion + 1 < ExhaustionCap)
                                        {
                                            Time = 0;
                                            NPC.rotation = 0f;

                                            CurrentState = Main.rand.NextBool(3) ? DjinnState.MudaMuda : DjinnState.Dive;
                                            if (CurrentState == DjinnState.Dive)
                                                AttackFlag = true;
                                            Exhaustion += 1;
                                            return;
                                        }
                                    }

                                    if (Time > 240 || (MathF.Abs(NPC.velocity.X) < 0.01f && MathF.Abs(NPC.velocity.Y) < 0.01f))
                                    {
                                        Time = 0;
                                        AttackFlag = false;
                                        NPC.rotation = 0f;
                                        FrameX = 0;
                                        CurrentState = DjinnState.RecoverFlex;
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                NPC.TargetClosest(); //Turn towards nearest player for uppercut
                                NPC.rotation = MathHelper.PiOver2 * -NPC.direction;

                                Point surface = CollisionUtils.FindSurfaceBelow(NPC.Center.ToTileCoordinates(), true);
                                for (int i = -3; i <= 3; i++)
                                {
                                    if (Main.rand.NextFloat() < 0.9f)
                                        continue;

                                    Point p = CollisionUtils.FindSurfaceBelow(surface + new Point(i, 0));
                                    WorldGen.KillTile(p.X, p.Y, effectOnly: true);

                                    if (Framing.GetTileSafely(p).TileType == TileID.Sand)
                                    {
                                        for (int j = 0; j < 3; j++)
                                        {
                                            LargeDust d = new(p.ToWorldCoordinates(), new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, -8)), new Color(212, 192, 100), new Color(212, 192, 100) * 0.5f, Main.rand.NextFloat(0.75f, 1.5f), 200, Main.rand.NextFloat(0.01f, 0.05f));
                                            ParticleSystem.SpawnParticle(d, DrawLayer.AfterPlayers);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                case DjinnState.Dive:
                    if(Time < 0)
                    {
                        if (Time > -(DiveRepositionTime + 10))
                        {
                            NPC.rotation *= DiveRotationDecay;
                            NPC.position.Y -= 4;
                        }

                        if (Math.Abs(MathHelper.WrapAngle(NPC.rotation)) < 0.1f)
                        {
                            NPC.hide = false;
                            FrameX = 0;
                            NPC.TargetClosest();
                            NPC.velocity = ((Target.Center - Vector2.UnitY * 96f) - NPC.Center) / 90f;
                        }
                        else
                        {
                            NPC.hide = true;
                            NPC.velocity = Vector2.Zero;
                        }

                        if (Time == -10)
                        {
                            Time = 0;
                            AttackFlag = false;
                            NPC.rotation = 0f;
                            NPC.hide = false;

                            if (Exhaustion + 2 < ExhaustionCap && Main.rand.NextBool())
                            {
                                CurrentState = DjinnState.TwisterPunch;
                                Exhaustion += 1;
                            }
                            if (Exhaustion + 1 < ExhaustionCap)
                            {
                                CurrentState = Main.rand.NextBool(3) ? DjinnState.MudaMuda : DjinnState.TwisterPunch;
                                Exhaustion += 1;
                            }
                            else
                            {
                                FrameX = 0;
                                CurrentState = DjinnState.RecoverFlex;
                            }
                            return;
                        }

                    }
                    else if(!AttackFlag)
                    {
                        FrameX = 2;
                        NPC.velocity = Vector2.Zero;
                        if (Time == 0)
                        {
                            Vector2 midPoint = (NPC.Center + Target.Center) / 2f;
                            Point ground = CollisionUtils.FindSurfaceBelow(midPoint.ToTileCoordinates(), true);
                            AttackVector = new Vector2(midPoint.X, MathF.Min((ground.Y * 16f + 8f) - GroundOffset, Target.Center.Y - PlayerOffset));
                        }
                        else
                        {
                            NPC.Center = Vector2.Lerp(NPC.Center, AttackVector, Time / (float)DiveRepositionTime);
                            if (Time == DiveRepositionTime)
                            {
                                AttackFlag = true;
                                Time = 0;
                                return;
                            }
                        }
                        NPC.rotation = AttackVector.AngleTo(Target.Center);
                        if (NPC.direction == -1)
                            NPC.rotation = MathHelper.WrapAngle(NPC.rotation + MathHelper.Pi);
                    }
                    else
                    {
                        if(Time < DiveTargettingTime)
                        {
                            FrameX = 2;

                            NPC.velocity = Vector2.Zero;
                            NPC.direction = Target.Center.X > NPC.Center.X ? 1 : -1;
                            AttackVector = NPC.DirectionTo(Target.Center);
                            NPC.rotation = AttackVector.ToRotation();
                            if (NPC.direction == -1)
                                NPC.rotation = MathHelper.WrapAngle(NPC.rotation + MathHelper.Pi);

                            if (Time == (DiveTargettingTime - 1) && !CollisionUtils.RayCast(NPC.Center, AttackVector, 800f, out _).HasValue)
                            {
                                if (AttackCounter++ >= DiveMercyTime)
                                {
                                    Time = DiveTargettingTime + DiveDelay;
                                    AttackCounter = 0;
                                    return;
                                }
                                else
                                {
                                    Time--;
                                    AttackCounter++;
                                }
                            }
                        }
                        else if(Time >= DiveTargettingTime + DiveDelay)
                        {
                            if (Time == DiveTargettingTime + DiveDelay)
                            {
                                NPC.velocity = AttackVector * DiveSpeed;
                                FrameX = 3;
                            }

                            NPC.hide = true;

                            if (NPC.DistanceSQ(Target.Center) > 640000) //800^2
                            {
                                Time = FallTime;
                                CurrentState = DjinnState.SubmergedUppercut;
                                AttackFlag = true;
                                Exhaustion -= 1;
                                return;
                            }

                            if ((NPC.Bottom.Y + NPC.velocity.Y) > Target.Bottom.Y && CollisionUtils.SurfaceCollision(NPC.position + NPC.velocity, NPC.width, NPC.height))
                            {
                                SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact, NPC.Center);
                                Time = -(DiveStuckTime + DiveSegwayTime + 10);

                                for (int i = -3; i <= 3; i++)
                                {
                                    Point s = NPC.Center.ToTileCoordinates() - new Point(-i * NPC.direction, 8);
                                    Point g = CollisionUtils.FindSurfaceBelow(s);
                                    WorldGen.KillTile(g.X, g.Y, effectOnly: true);
                                }

                                Point start = NPC.Center.ToTileCoordinates() - new Point(-2 * NPC.direction, 8);
                                Point ground = CollisionUtils.FindSurfaceBelow(start);
                                GroundWave particle = new(ground, Phase2 ? 38 : 32, NPC.direction == 1, Phase2 ? 94 : 54, 1, 16, strong: true);
                                ParticleSystem.SpawnParticle(particle, DrawLayer.AfterPlayers);
                                int dir = (NPC.direction == 1 ? 1 : -1);

                                Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), ground.ToWorldCoordinates(), new Vector2(16 * dir, 0f), ModContent.ProjectileType<GroundwaveHurt>(), DiveWaveDamage, 0f, ai1: 10, ai2: Phase2 ? 90 : 50);
                                proj.timeLeft = particle.Lifetime - 12;

                                start = NPC.Center.ToTileCoordinates() - new Point(2 * NPC.direction, 8);
                                ground = CollisionUtils.FindSurfaceBelow(start);
                                particle = new(ground, Phase2 ? 38 : 32, NPC.direction != 1, Phase2 ? 94 : 54, 1, 16, strong: true);
                                ParticleSystem.SpawnParticle(particle, DrawLayer.AfterPlayers);

                                proj = Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), ground.ToWorldCoordinates(), new Vector2(16 * -dir, 0f), ModContent.ProjectileType<GroundwaveHurt>(), DiveWaveDamage, 0f, ai1: 10, ai2: Phase2 ? 90 : 50);
                                proj.timeLeft = particle.Lifetime - 12;

                                start = NPC.Center.ToTileCoordinates() - new Point(0, 8);
                                ground = CollisionUtils.FindSurfaceBelow(start);
                                for (int i = -6; i <= 6; i++)
                                {
                                    Point spawnTile = CollisionUtils.FindSurfaceAround(ground + new Point(i, 0), true);
                                    WorldGen.KillTile(spawnTile.X, spawnTile.Y, effectOnly: true);
                                    if (Framing.GetTileSafely(spawnTile).TileType == TileID.Sand)
                                    {
                                        Vector2 spawnPos = spawnTile.ToWorldCoordinates();
                                        for (int j = 0; j < 3; j++)
                                        {
                                            LargeDust d = new(spawnPos, new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-1, -1 - j)), new Color(212, 192, 100), new Color(212, 192, 100) * 0.5f, Main.rand.NextFloat(0.75f, 1.5f), 200, Main.rand.NextFloat(0.01f, 0.05f));
                                            ParticleSystem.SpawnParticle(d, DrawLayer.AfterPlayers);
                                        }
                                    }
                                }

                                return;
                            }
                        }
                    }
                    break;
                case DjinnState.MudaMuda:
                    if (Time <= MudaMudaRepositionTime)
                    {
                        if (Time == 0)
                        {
                            if (AttackAmount == -1)
                            {
                                AttackAmount = GetMudaMudaAmount();
                                AttackCounter = 1;
                                NPC.netUpdate = true;
                            }
                            FrameX = 0;
                            NPC.damage = 0;
                            AttackVector = Target.velocity.SafeNormalize(Vector2.UnitX * (NPC.Center.X > Target.Center.X ? -1 : 1));
                            AttackAngle = NPC.rotation;
                            int oldDir = NPC.direction;
                            NPC.direction = NPC.Center.X > Target.Center.X ? -1 : 1;
                            if(oldDir != NPC.direction)
                                AttackAngle = MathHelper.WrapAngle(NPC.rotation + MathHelper.Pi);
                        }
                        NPC.Center = Vector2.Lerp(NPC.Center, Target.Center + (AttackVector * MudaMudaOffset), Time / (float)MudaMudaRepositionTime);
                        NPC.velocity = Vector2.Zero;
                        NPC.dontTakeDamage = true;

                        NPC.direction = NPC.Center.X > Target.Center.X ? -1 : 1;

                        if (Time > MudaMudaRepositionTime / 2)
                        {
                            int turnAroundTime = (int)Time - MudaMudaRepositionTime / 2;
                            float goalRotation = (-AttackVector).ToRotation();
                            NPC.rotation = AttackAngle.AngleLerp(goalRotation, MathUtils.CircOutEasing(MathHelper.Clamp(turnAroundTime / (float)(MudaMudaRepositionTime / 2), 0f, 1f)));
                            if (NPC.direction == -1)
                                NPC.rotation = MathHelper.WrapAngle(NPC.rotation + MathHelper.Pi);
                        }

                        NPC.Opacity = MathF.Cos(Time / (float)MudaMudaRepositionTime * MathHelper.TwoPi) / 2f + 0.5f;
                    }
                    else if (Time >= MudaMudaRepositionTime + MudaMudaDelay)
                    {
                        if (Time == MudaMudaRepositionTime + MudaMudaDelay)
                        {
                            NPC.velocity = AttackVector * -MudaMudaSpeed;
                            FrameX = 1;
                            NPC.direction = -Math.Sign(AttackVector.X);
                            NPC.rotation = (-AttackVector).ToRotation();
                            if (NPC.direction == -1)
                                NPC.rotation = MathHelper.WrapAngle(NPC.rotation + MathHelper.Pi);
                            NPC.damage = NPC.defDamage;
                        }
                        else
                            NPC.velocity *= 0.98f;

                        if (Time % 5 == 0)
                            SoundEngine.PlaySound(SoundID.Item1, NPC.Center);

                        if (Time >= MudaMudaRepositionTime + MudaMudaDelay + MudaMudaDuration)
                        {
                            Time = 0;
                            AttackFlag = false;
                            NPC.rotation = 0f;
                            NPC.velocity = Vector2.Zero;
                            AttackAngle = 0f;

                            if (Exhaustion + 1 < ExhaustionCap)
                            {
                                if (AttackCounter < AttackAmount)
                                {
                                    CurrentState = DjinnState.MudaMuda;
                                    AttackCounter++;
                                }
                                else
                                {
                                    CurrentState = Main.rand.NextBool() ? DjinnState.CactusBaseball : DjinnState.SubmergedUppercut;
                                    AttackAmount = -1;
                                    AttackCounter = 0;
                                }


                                Exhaustion += 1;
                            }
                            else
                            {
                                AttackAmount = -1;
                                AttackCounter = 0;
                                FrameX = 0;
                                CurrentState = DjinnState.RecoverFlex;
                            }
                            return;
                        }
                    }
                    else
                    {
                        FrameX = 2;
                        NPC.dontTakeDamage = false;
                    }
                    break;
                case DjinnState.CactusBaseball:
                    float cactusOffset = 56f;
                    if(Time == 0)
                    {
                        FrameX = 2;
                        NPC.TargetClosest();
                        AttackVector = NPC.Center - (NPC.DirectionTo(Target.Center) * cactusOffset);
                        Point tileCoords = CollisionUtils.FindSurfaceBelow(AttackVector.ToTileCoordinates(), true);
                        Vector2 cactusSpawn = tileCoords.ToWorldCoordinates(8, 32);
                        float minimumHeight = NPC.height + 128;
                        if (cactusSpawn.Y - minimumHeight < AttackVector.Y)
                            AttackVector.Y = cactusSpawn.Y - minimumHeight;

                        float desiredHeight = Math.Abs((AttackVector.Y - cactusSpawn.Y) - 96f);
                        float neededSpeed = MathF.Sqrt(2 * 0.3f * desiredHeight);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), cactusSpawn, Vector2.UnitY * -neededSpeed, ModContent.ProjectileType<CactusBaseball>(), 10, 0.5f);
                        if (Phase2)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), cactusSpawn, Vector2.UnitY * -neededSpeed, ModContent.ProjectileType<CactusBaseball>(), 10, 0.5f, -1, -15);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), cactusSpawn, Vector2.UnitY * -neededSpeed, ModContent.ProjectileType<CactusBaseball>(), 10, 0.5f, -1, -30);
                        }
                    }

                    Projectile nextCactus = null;
                    foreach(Projectile p in Main.projectile)
                    {
                        if (p.type != ModContent.ProjectileType<CactusBaseball>() || p.ai[1] == 1)
                            continue;

                        if (nextCactus == null || nextCactus.ai[0] < p.ai[0])
                            nextCactus = p;
                    }

                    Vector2 goalPos = AttackVector + (Target.DirectionTo(AttackVector) * cactusOffset);
                    NPC.velocity = (goalPos - NPC.Center) / 4f;
                    NPC.direction = (NPC.Center + NPC.velocity).X > Target.Center.X ? -1 : 1;
                    NPC.rotation = (NPC.Center + NPC.velocity).AngleTo(Target.Center) + (NPC.direction == -1 ? MathHelper.Pi : 0);

                    if (nextCactus != null)
                    {
                        if (FrameX == 2)
                        {
                            if (nextCactus.velocity.Y > 0)
                            {
                                Vector2 a = (NPC.Center + NPC.velocity).DirectionTo(nextCactus.Center);
                                Vector2 b = (NPC.Center + NPC.velocity).DirectionTo(Target.Center);
                                float cross = (a.X * b.Y) - (a.Y * b.X);

                                if (cross * NPC.direction < 0f)
                                {
                                    FrameX = 3;
                                    nextCactus.velocity = b * 28f;
                                    nextCactus.tileCollide = true;
                                    nextCactus.ai[1] = 1;
                                    Time = 240;
                                    SoundEngine.PlaySound(SoundID.Dig, NPC.Center);
                                    AttackCounter = 0;
                                }
                            }
                        }
                        else
                        {
                            if (AttackCounter >= 5)
                            {
                                FrameX = 2;
                                AttackCounter = 0;
                            }
                            else
                                AttackCounter++;
                        }
                    }

                    if (Time > 270)
                    {
                        Time = 0;
                        NPC.rotation = 0;
                        AttackFlag = false;

                        if (Main.rand.NextBool() && Exhaustion + 2 < ExhaustionCap)
                        {
                            FrameX = 4;
                            CurrentState = DjinnState.GrandSlam;
                            Exhaustion += 2;
                        }
                        else if (Exhaustion + 1 < ExhaustionCap)
                        {
                            CurrentState = DjinnState.Dive;
                            Exhaustion += 1;
                        }
                        else
                        {
                            FrameX = 0;
                            CurrentState = DjinnState.RecoverFlex;
                        }
                        return;
                    }
                    break;
            }

            Time++;
        }

        //Credit to Spirit Reforged
        public void ManageSandstormffects()
        {
            foreach (Player Player in Main.player.Where(p => p.active && !p.dead))
                Player.buffImmune[BuffID.WindPushed] = true;

            if (!Phase2 || CreativePowerManager.Instance.GetPower<CreativePowers.FreezeWindDirectionAndStrength>().Enabled || CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>().Enabled)
                return;

            Sandstorm.Happening = true;
            Sandstorm.TimeLeft = 60;

            //Sandstorm ramps up as the fight progresses
            float intendedSandstormPower = 0.2f + 0.8f * Utils.GetLerpValue(0.5f, 0.1f, NPC.life / (float)NPC.lifeMax, true);
            float sandstormPower = Math.Max(MathHelper.Lerp(Sandstorm.Severity, intendedSandstormPower, 0.2f), 0.2f);

            Sandstorm.Severity = Math.Max(Sandstorm.Severity, sandstormPower);
            Sandstorm.IntendedSeverity = Math.Max(Sandstorm.IntendedSeverity, sandstormPower);
            Main.windSpeedTarget = 0.8f;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            int dust = ModContent.DustType<Dusts.SandDust>();
            for (int Loop = 0; Loop < 5; Loop++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, dust, 0f, 0f, 0);
                Main.dust[d].velocity.Y = hit.HitDirection * 0.1F;
                Main.dust[d].noGravity = false;
            }
            if (NPC.life <= 0)
            {
                if (!Main.dedServ)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity * 0.2f, Mod.Find<ModGore>("DjinnGore1").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity * 0.2f, Mod.Find<ModGore>("DjinnGore2").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity * 0.2f, Mod.Find<ModGore>("DjinnGore3").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity * 0.2f, Mod.Find<ModGore>("DjinnGore4").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity * 0.2f, Mod.Find<ModGore>("DjinnGore5").Type, 1f);
                }
                for (int Loop = 0; Loop < 60; Loop++)
                {
                    int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, dust, 0f, 0f, 0);
                    Main.dust[d].velocity.X *= 0f;
                    Main.dust[d].noGravity = false;
                }
            }
            else if(!Phase2 && (CurrentState != DjinnState.PhaseSwitch || !AttackFlag) && NPC.life <= NPC.lifeMax / 2)
            {
                CurrentState = DjinnState.PhaseSwitch;
                NPC.immortal = true;
                Time = 0;
                AttackFlag = false;
                NPC.direction = -hit.HitDirection;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Width = TextureAssets.Npc[NPC.type].Width() / 6;
            NPC.frame.X = FrameX * NPC.frame.Width;
            NPC.frameCounter++;

            int frameRate = 5;
            if (FrameX == 5)
                frameRate = 9;

            if (NPC.frameCounter > frameRate)
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

        public override void DrawBehind(int index)
        {
            if (NPC.hide)
                Main.instance.DrawCacheNPCsBehindNonSolidTiles.Add(index);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            NPC.spriteDirection = NPC.direction;

            if (!NPC.IsABestiaryIconDummy && (!Target.ZoneDesert || Phase2) && drawColor != Color.Black)
            {
                Vector2[] positions = new Vector2[NPC.oldPos.Length];
                for (int i = 0; i < positions.Length; i++)
                    positions[i] = NPC.oldPos[i] + NPC.frame.Size() * 0.5f;
                
                DrawingUtils.DrawCenteredAfterimages(spriteBatch, NPC, NPCID.Sets.TrailingMode[Type], Color.Goldenrod);
            }

            spriteBatch.Draw(texture, NPC.Center + DrawOffset - screenPos, NPC.frame, drawColor * NPC.Opacity, NPC.rotation, NPC.frame.Size() / 2f, NPC.scale, NPC.SpriteEffectDirection(), 0);

            return false;
        }
    }
}
