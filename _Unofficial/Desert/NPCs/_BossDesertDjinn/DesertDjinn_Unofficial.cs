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
            RecoverFlex,
            GrandSlam,
            TwisterPunch,
            SubmergedUppercut,
            Dive,
            MudaMuda
        }

        public DjinnState CurrentState { get => (DjinnState)NPC.ai[0]; set => NPC.ai[0] = (float)value; }
        public DjinnState PreviousStartingState = DjinnState.Spawn;
        public ref float Time => ref NPC.ai[1];
        public bool AttackFlag = false;
        public bool AttemptFailedFlag = false;
        public Vector2 AttackVector = Vector2.Zero;
        public int AttackCounter = 0;

        public bool Phase2 => NPC.life / (float)NPC.lifeMax <= 0.5f;

        public Player Target => Main.player[NPC.target];

        private int FrameX = 0;

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
        }

        public override void AI()
        {
            NPC.hide = CurrentState == DjinnState.SubmergedUppercut;

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
                case DjinnState.RecoverFlex:
                    NPC.TargetClosest();

                    if (Time < 240)
                    {
                        NPC.velocity *= 0.95f;
                        if(Time <= 30f)
                        {
                            NPC.Center = Vector2.Lerp(NPC.Center, CollisionUtils.FindSurfaceBelow(NPC.Center.ToTileCoordinates(), true).ToWorldCoordinates(8f, -NPC.height), Time / 30f);
                        }
                    }
                    else
                        NPC.velocity = ((Target.Center - Vector2.UnitY * 96f) - NPC.Center) / 90f;

                    if (Time >= 30 && Time < 240)
                    {
                        NPC.damage = 0;
                        if (Time == 30)
                        {
                            NPC.frameCounter = -1;
                            NPC.frame.Y = Main.rand.Next(3) * NPC.frame.Height * 3;
                        }
                        FrameX = 5;
                    }
                    else
                    {
                        NPC.damage = NPC.defDamage;
                        FrameX = 0;
                    }


                    if (Time >= 300)
                    {
                        List<DjinnState> options = [DjinnState.GrandSlam, DjinnState.TwisterPunch, DjinnState.SubmergedUppercut, DjinnState.Dive];
                        options.Remove(PreviousStartingState);

                        CurrentState = options[Main.rand.Next(options.Count)];
                        PreviousStartingState = CurrentState;
                        AttackFlag = false;
                        Time = 0;
                        Exhaustion = CurrentState == DjinnState.GrandSlam ? 2 : 1;
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
                                GroundWave particle = new(ground, 32, NPC.direction == 1, 162, 1, 16);
                                ParticleSystem.SpawnParticle(particle, DrawLayer.AfterPlayers);
                                int dir = (NPC.direction == 1 ? 1 : -1);

                                Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), ground.ToWorldCoordinates(), new Vector2(16 * dir, 0f), ModContent.ProjectileType<GroundwaveHurt>(), 10, 0f, ai1: 10, ai2: 150);
                                proj.timeLeft = particle.Lifetime - 12;
                                proj.position.Y -= 25;

                                start = NPC.Center.ToTileCoordinates() - new Point(2 * NPC.direction, 8);
                                ground = CollisionUtils.FindSurfaceBelow(start);
                                ParticleSystem.SpawnParticle(new GroundWave(ground, 10, NPC.direction != 1, 24, 3, 16), DrawLayer.AfterPlayers);

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

                                if (Exhaustion + 1 < ExhaustionCap)
                                {
                                    CurrentState = AttemptFailedFlag ? DjinnState.MudaMuda : DjinnState.SubmergedUppercut;
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
                    if(Time == 30)
                    { 
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Target.Top - Vector2.UnitX * 96, Vector2.Zero, ModContent.ProjectileType<DesertDjinn_Djinnado>(), 20, 1f, ai2: 60);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Target.Top + Vector2.UnitX * 96, Vector2.Zero, ModContent.ProjectileType<DesertDjinn_Djinnado>(), 20, 1f, ai2: 60);
                    }
                    else if(Time < 30)
                    {
                        if (Time == 0)
                            FrameX = 2;

                        Point hitPos = Point.Zero;
                        int mod = (int)Time % 4;
                        if (mod == 0)
                            hitPos = CollisionUtils.FindSurfaceBelow((Target.Center - Vector2.UnitX * 96).ToTileCoordinates());
                        else if(mod == 2)
                            hitPos = CollisionUtils.FindSurfaceBelow((Target.Center + Vector2.UnitX * 96).ToTileCoordinates());
                            
                        if(hitPos != Point.Zero)
                        {
                            int amt = Main.rand.Next(3, 5);
                            for (int i = 0; i < amt; i++)
                                Dust.NewDust(hitPos.ToWorldCoordinates(0, 0), 16, 16, DustID.Sandnado, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-12f, -4f), Scale: Main.rand.NextFloat(0.5f, 2f));
                        }
                    }

                    if (Time < 90)
                    {
                        NPC.TargetClosest();
                        int side = NPC.Center.X > Target.Center.X ? 1 : -1;
                        NPC.velocity.X = (Target.Center.X + (192 * side) - NPC.Center.X) / 10f;
                        float lerp = MathHelper.Clamp((Time - 30) / 60f, 0f, 1f);
                        NPC.velocity.Y = (Target.Center.Y - NPC.Center.Y) * MathHelper.Lerp(Phase2 ? 0.2f : 0.1f, 0f, lerp);
                        //NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y, -8, 8);
                    }
                    else
                    {
                        int side = NPC.Center.X > Target.Center.X ? 1 : -1;
                        if (Time == 90)
                        {
                            NPC.velocity = Vector2.UnitX * (Phase2 ? -48 : -24) * side;
                            FrameX = 3;
                        }
                        else
                            NPC.velocity *= Phase2 ? 0.9f : 0.95f;
                    }

                    if (Time > (Phase2 ? 120 : 150))
                    {
                        Time = 0;
                        AttackFlag = false;

                        if (Main.rand.NextBool() && Exhaustion + 2 < ExhaustionCap)
                        {
                            FrameX = 4;
                            CurrentState = DjinnState.GrandSlam;
                            Exhaustion += 2;
                        }
                        else if(Exhaustion + 1 < ExhaustionCap)
                        {
                            CurrentState = DjinnState.SubmergedUppercut;
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

                            if (NPC.Bottom.Y > Target.Bottom.Y && Collision.SolidCollision(NPC.position + NPC.velocity, NPC.width, NPC.height))
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
                                GroundWave particle = new(ground, 8, NPC.direction == 1, 24, 2, 16);
                                ParticleSystem.SpawnParticle(particle, DrawLayer.AfterPlayers);

                                start = NPC.Center.ToTileCoordinates() - new Point(2 * NPC.direction, 8);
                                ground = CollisionUtils.FindSurfaceBelow(start);
                                ParticleSystem.SpawnParticle(new GroundWave(ground, 8, NPC.direction != 1, 24, 2, 16), DrawLayer.AfterPlayers);
                                return;
                            }
                        }
                    }
                    else
                    {
                        //Maintian velocity for a few frames to submerge
                        int fallTime = 5;

                        if(Time > fallTime)
                        {
                            int burrowTime = Phase2 ? 25 : 55;
                            int holdTime = Phase2 ? 20 : 30;
                            if (Time < fallTime + burrowTime)
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
                                }
                            }
                            else if (Time > fallTime + burrowTime + holdTime)
                            {
                                if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                                {
                                    NPC.velocity = Vector2.UnitY * -24f;
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
                                        GroundWave particle = new(ground, 8, NPC.direction == 1, 24, 2, 16);
                                        ParticleSystem.SpawnParticle(particle, DrawLayer.AfterPlayers);

                                        start = NPC.Center.ToTileCoordinates() - new Point(2 * NPC.direction, 8);
                                        ground = CollisionUtils.FindSurfaceBelow(start, true);
                                        ParticleSystem.SpawnParticle(new GroundWave(ground, 8, NPC.direction != 1, 24, 2, 16), DrawLayer.AfterPlayers);
                                    }
                                }
                                else
                                {
                                    if (NPC.Center.Y > Target.Bottom.Y + 16)
                                        NPC.velocity = Vector2.UnitY * -24f;
                                    else
                                        NPC.velocity *= 0.9f;

                                    NPC.rotation *= 0.92f;
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
                                }
                            }
                        }
                    }
                    break;
                case DjinnState.Dive:
                    if(Time < 0)
                    {
                        if(Time > -75)
                            NPC.rotation *= 0.9f;

                        if (Math.Abs(NPC.rotation) < 0.1f)
                        {
                            FrameX = 0;
                            NPC.TargetClosest();
                            NPC.velocity = ((Target.Center - Vector2.UnitY * 96f) - NPC.Center) / 90f;
                        }
                        else
                        {
                            NPC.velocity = Vector2.Zero;
                        }

                        if (Time == -10)
                        {
                            Time = 0;
                            AttackFlag = false;
                            NPC.rotation = 0f;

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
                            AttackVector = new Vector2(midPoint.X, MathF.Min((ground.Y * 16f + 8f) - 360, Target.Center.Y - 240f));
                        }
                        else
                        {
                            NPC.Center = Vector2.Lerp(NPC.Center, AttackVector, Time / 30f);
                            if (Time == 30)
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
                        if(Time < 10)
                        {
                            FrameX = 2;

                            NPC.velocity = Vector2.Zero;
                            NPC.direction = Target.Center.X > NPC.Center.X ? 1 : -1;
                            AttackVector = NPC.DirectionTo(Target.Center);
                            NPC.rotation = AttackVector.ToRotation();
                            if (NPC.direction == -1)
                                NPC.rotation = MathHelper.WrapAngle(NPC.rotation + MathHelper.Pi);

                            if (Time == 9 && !CollisionUtils.RayCast(NPC.Center, AttackVector, 800f, out _).HasValue)
                            {
                                if (AttackCounter++ >= 30)
                                {
                                    Time = 30;
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
                        else if(Time >= 30)
                        {
                            if (Time == 30)
                            {
                                NPC.velocity = AttackVector * (Phase2 ? 48f: 32f);
                                FrameX = 3;
                            }

                            if(NPC.DistanceSQ(Target.Center) > 640000) //800^2
                            {
                                Time = 10;
                                CurrentState = DjinnState.SubmergedUppercut;
                                AttackFlag = true;
                                Exhaustion -= 1;
                                return;
                            }

                            if ((NPC.Bottom.Y + NPC.velocity.Y) > Target.Bottom.Y && CollisionUtils.SurfaceCollision(NPC.position + NPC.velocity, NPC.width, NPC.height))
                            {
                                SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact, NPC.Center);
                                Time = -90;

                                for (int i = -3; i <= 3; i++)
                                {
                                    Point s = NPC.Center.ToTileCoordinates() - new Point(-i * NPC.direction, 8);
                                    Point g = CollisionUtils.FindSurfaceBelow(s);
                                    WorldGen.KillTile(g.X, g.Y, effectOnly: true);
                                }

                                Point start = NPC.Center.ToTileCoordinates() - new Point(-2 * NPC.direction, 8);
                                Point ground = CollisionUtils.FindSurfaceBelow(start);
                                GroundWave particle = new(ground, Phase2 ? 38 : 32, NPC.direction == 1, Phase2 ? 94 : 54, 1, 16);
                                ParticleSystem.SpawnParticle(particle, DrawLayer.AfterPlayers);
                                int dir = (NPC.direction == 1 ? 1 : -1);

                                Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), ground.ToWorldCoordinates(), new Vector2(16 * dir, 0f), ModContent.ProjectileType<GroundwaveHurt>(), 10, 0f, ai1: 10, ai2: Phase2 ? 90 : 50);
                                proj.timeLeft = particle.Lifetime - 12;

                                start = NPC.Center.ToTileCoordinates() - new Point(2 * NPC.direction, 8);
                                ground = CollisionUtils.FindSurfaceBelow(start);
                                particle = new(ground, Phase2 ? 38 : 32, NPC.direction != 1, Phase2 ? 94 : 54, 1, 16);
                                ParticleSystem.SpawnParticle(particle, DrawLayer.AfterPlayers);

                                proj = Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), ground.ToWorldCoordinates(), new Vector2(16 * -dir, 0f), ModContent.ProjectileType<GroundwaveHurt>(), 10, 0f, ai1: 10, ai2: Phase2 ? 90 : 50);
                                proj.timeLeft = particle.Lifetime - 12;
                                return;
                            }
                        }
                    }
                    break;
                case DjinnState.MudaMuda:
                    if (Time <= 30)
                    {
                        if (Time == 0)
                        {
                            FrameX = 0;
                            NPC.damage = 0;
                            AttackVector = Target.velocity.SafeNormalize(Vector2.UnitX * (NPC.Center.X > Target.Center.X ? -1 : 1));
                        }
                        NPC.Center = Vector2.Lerp(NPC.Center, Target.Center + (AttackVector * 128), Time / 30f);
                        NPC.direction = NPC.Center.X > Target.Center.X ? -1 : 1;
                        NPC.Opacity = MathF.Cos(Time / 30f * MathHelper.TwoPi) / 2f + 0.5f;
                        NPC.rotation = MathHelper.Lerp(0f, (-AttackVector).ToRotation(), MathUtils.SineOutEasing(MathHelper.Clamp((Time - 15) / 15f, 0f, 1f)));
                        NPC.dontTakeDamage = true;
                        if (NPC.direction == -1)
                            NPC.rotation = MathHelper.WrapAngle(NPC.rotation + MathHelper.Pi);
                    }
                    else if (Time >= 40)
                    {
                        if (Time == 40)
                        {
                            NPC.velocity = AttackVector * (Phase2 ? -20f : -10f);
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

                        if (Time >= (Phase2 ? 65 : 90))
                        {
                            Time = 0;
                            AttackFlag = false;
                            NPC.rotation = 0f;
                            NPC.velocity = Vector2.Zero;

                            if (Exhaustion + 1 < ExhaustionCap)
                            {
                                if(Phase2)
                                {
                                    if (AttackCounter <= (Main.masterMode ? 2 : Main.expertMode ? 1 : 0))
                                        CurrentState = DjinnState.MudaMuda;
                                    else if (Main.rand.NextBool(AttackCounter - (Main.masterMode ? 1 : Main.expertMode ? 0 : -1)))
                                        CurrentState = DjinnState.MudaMuda;
                                    else
                                        CurrentState = DjinnState.TwisterPunch;
                                }
                                else
                                    CurrentState = Main.rand.NextBool(AttackCounter + 2) ? DjinnState.MudaMuda : DjinnState.TwisterPunch;
                                
                                if (CurrentState == DjinnState.TwisterPunch)
                                    AttackCounter = 0;
                                else
                                    AttackCounter++;

                                Exhaustion += 1;
                            }
                            else
                            {
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
            }

            Time++;
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

            spriteBatch.Draw(texture, NPC.Center - screenPos, NPC.frame, drawColor * NPC.Opacity, NPC.rotation, NPC.frame.Size() / 2f, NPC.scale, NPC.SpriteEffectDirection(), 0);

            return false;
        }
    }
}
