using AAModClassic._Content.Desert.___PreHardmode.NPCs.__BossDesertDjinn;
using AAModClassic._Content.Desert.___PreHardmode.NPCs._Day;
using AAModClassic._Unofficial.Desert.NPCs._BossDesertDjinn;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Desert.NPCs
{
    public class DustDjinn_Unofficial : ModNPC
    {
        public override string Texture => FilePathUtils.TexturePath<DustDjinn>();

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Djinn");
            Main.npcFrameCount[NPC.type] = 16;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 200;
            NPC.defense = 20;
            NPC.damage = 20;
            NPC.width = 42;
            NPC.height = 66;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noTileCollide = true;
            NPC.noGravity = true;

            NPC.knockBackResist = 0f;
            NPC.npcSlots = 2;
        }

        public int Exhaustion = 0;
        public static int ExhaustionCap => Main.expertMode ? 5 : 3;

        internal ref float Time => ref NPC.ai[0];

        internal enum DustDjinnState
        {
            Idle,
            Startled,
            Exhausted,
            Punch,
            Conjure,
            Dive,
            CallBackup,
            BookIt
        }

        internal DustDjinnState State = DustDjinnState.Idle;

        public Player Target => Main.player[NPC.target];

        internal Vector2 AttackVector = Vector2.Zero;
        internal bool HandOut = false;

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                return 0f;

            return (spawnInfo.Player.ZoneDesert || spawnInfo.Player.ZoneUndergroundDesert) &&
                NPC.downedBoss3 && !spawnInfo.Player.ZoneBeach
                && Main.dayTime ? .1f : 0f;
        }

        public override void OnSpawn(IEntitySource source)
        {
            //NPC.Center = CollisionUtils.FindSurfaceBelow(NPC.Center.ToTileCoordinates()).ToWorldCoordinates(8, -NPC.height / 2);
            //NPC.netUpdate = true;
            NPC.direction = NPC.spriteDirection;
        }

        public override void AI()
        {
            switch(State)
            {
                case DustDjinnState.Idle:
                    NPC.velocity = new Vector2(0, MathF.Sin(Time / 10f));
                    float detectionRadius = 100f;
                    Vector2 detectionCenter = NPC.Center + (Vector2.UnitX * detectionRadius * NPC.direction);
                    foreach(Player p in Main.ActivePlayers)
                    {
                        if (p.dead)
                            continue;

                        if (p.Distance(detectionCenter) < detectionRadius)
                        {
                            State = DustDjinnState.Startled;
                            NPC.target = p.whoAmI;
                            Time = 0;
                            return;
                        }
                    }
                    break;
                case DustDjinnState.Startled:
                    if (Time == 0)
                        NPC.velocity = Vector2.UnitX * -6 * NPC.direction;
                    else
                    {
                        NPC.velocity *= 0.9f;
                        if(Time == 30)
                        {
                            State = Main.rand.NextBool() ? DustDjinnState.Punch : DustDjinnState.Dive;
                            Time = 0;
                            NPC.velocity = Vector2.Zero;
                            Exhaustion++;
                            return;
                        }
                    }
                    break;
                case DustDjinnState.Exhausted:
                    NPC.velocity = new Vector2(0, MathF.Sin(Time / 5f));
                    NPC.direction = NPC.Center.X > Target.Center.X ? -1 : 1;
                    if (Time == 240)
                    {
                        State = Main.rand.NextBool() ? DustDjinnState.Punch : DustDjinnState.Dive;
                        Time = 0;
                        NPC.velocity = Vector2.Zero;
                        Exhaustion = 1;
                        return;
                    }
                    break;
                case DustDjinnState.Punch:
                    if(Time < 60)
                    {
                        NPC.direction = NPC.Center.X > Target.Center.X ? -1 : 1;
                        NPC.velocity.X = (Target.Center.X + (150 * -NPC.direction) - NPC.Center.X) / 10f;
                        float lerp = MathHelper.Clamp(Time / 60f, 0f, 1f);
                        NPC.velocity.Y = (Target.Center.Y - NPC.Center.Y) * MathHelper.Lerp(0.1f, 0f, lerp);
                    }
                    else if(Time == 60)
                    {
                        HandOut = true;
                        NPC.direction = NPC.Center.X > Target.Center.X ? -1 : 1;
                        SoundEngine.PlaySound(SoundID.Item1, NPC.Center);
                        NPC.velocity = Vector2.UnitX * 16f * NPC.direction;
                    }
                    else
                    {
                        NPC.velocity *= 0.94f;

                        if(Time == 120)
                        {
                            HandOut = false;
                            if (Exhaustion >= ExhaustionCap)
                                State = DustDjinnState.Exhausted;
                            else
                                State = Main.rand.Next(3) switch
                                {
                                    0 => DustDjinnState.Punch,
                                    1 => DustDjinnState.Conjure,
                                    _ => DustDjinnState.Dive
                                };
                            Time = 0;
                            Exhaustion++;
                            return;
                        }
                    }
                    break;
                case DustDjinnState.Conjure:
                    NPC.direction = NPC.Center.X > Target.Center.X ? -1 : 1;

                    if (Time >= 15)
                        HandOut = Time <= 120;

                    if (Time == 30)
                    {
                        Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), Target.Center, Vector2.Zero, ProjectileID.SandnadoHostileMark, 10, 1f).hostile = false;
                        NPC.velocity = Vector2.UnitX * -NPC.direction * 3f;
                    }
                    else if (Time > 30)
                        NPC.velocity *= 0.96f;
                    else
                        NPC.velocity = Vector2.Zero;

                    if(Time == 180)
                    {
                        State = Exhaustion >= ExhaustionCap ? DustDjinnState.Exhausted : Main.rand.NextBool() ? DustDjinnState.Punch : DustDjinnState.Dive;
                        Time = 0;
                        Exhaustion++;
                        return;
                    }
                    break;
                case DustDjinnState.Dive:
                    if(Time <= 30)
                    {
                        NPC.velocity = Vector2.Zero;
                        if (Time == 0)
                        {
                            NPC.direction = NPC.Center.X > Target.Center.X ? -1 : 1;
                            Vector2 midPoint = (NPC.Center + Target.Center) / 2f;
                            Point ground = CollisionUtils.FindSurfaceBelow(midPoint.ToTileCoordinates(), true);
                            AttackVector = new Vector2(midPoint.X, MathF.Min((ground.Y * 16f + 8f) - 240, Target.Center.Y - 180));
                        }
                        else
                        {
                            NPC.Center = Vector2.Lerp(NPC.Center, AttackVector, Time / 30f);
                            if (Time == 30)
                            {
                                NPC.direction = NPC.Center.X > Target.Center.X ? -1 : 1;
                                Vector2? result = CollisionUtils.RayCast(NPC.Center, NPC.DirectionTo(Target.Center), 900, out _);
                                if (result.HasValue)
                                    AttackVector = result.Value;
                                else
                                {
                                    Time = 30;
                                    State = DustDjinnState.Conjure;
                                    return;
                                }
                            }
                        }
                    }
                    else if(Time >= 60)
                    {
                        if(Time <= 90)
                            NPC.Center = Vector2.Lerp(NPC.Center, AttackVector, (Time - 60) / 30f);
                        else if (Time >= 120)
                        {
                            if (Time == 120)
                                NPC.velocity = Vector2.UnitY * -2f;
                            else
                                NPC.velocity *= 0.98f;

                            if (Time >= 150)
                            {
                                State = Exhaustion >= ExhaustionCap ? DustDjinnState.Exhausted : Main.rand.NextBool() ? DustDjinnState.Punch : DustDjinnState.Conjure;
                                Time = 0;
                                Exhaustion++;
                                return;
                            }
                        }
                    }
                    break;
                case DustDjinnState.CallBackup:
                    if (Time == 0)
                        NPC.velocity = Vector2.UnitX * NPC.direction * -4f;
                    else
                        NPC.velocity *= 0.95f;

                    switch(Time)
                    {
                        case 60:
                            CombatText.NewText(NPC.Hitbox, Color.Yellow, "*Pulls out Desert Lamp*");
                            break;
                        case 180:
                            CombatText.NewText(NPC.Hitbox, Color.Red, "What is your wish?");
                            break;
                        case 300:
                            CombatText.NewText(NPC.Hitbox, Color.Yellow, "I need someone to");
                            break;
                        case 360:
                            CombatText.NewText(NPC.Hitbox, Color.Yellow, "BEAT");
                            break;
                        case 420:
                            CombatText.NewText(NPC.Hitbox, Color.Yellow, "THEIR");
                            break;
                        case 480:
                            CombatText.NewText(NPC.Hitbox, Color.Yellow, "ASS!");
                            break;
                        case 600:
                            CombatText.NewText(NPC.Hitbox, Color.Red, "Your wish is...");
                            break;
                        case 720:
                            CombatText.NewText(NPC.Hitbox, Color.Red, "Granted.");
                            AAModGlobalNPC.SpawnBoss(Target, ModContent.NPCType<DesertDjinn_Unofficial>(), true, 0, 0, Language.GetTextValue("Mods.AAModClassic.Common.DesertDjinn"), false);
                            SoundEngine.PlaySound(SoundID.Roar, Target.Center);
                            break;
                        case 900:
                            State = DustDjinnState.BookIt;
                            Time = 0;
                            return;
                    }
                    break;
                case DustDjinnState.BookIt:
                    NPC.velocity = Vector2.UnitX * NPC.direction * -8f;
                    NPC.Opacity -= 0.05f;
                    if (NPC.Opacity <= 0f)
                    {
                        NPC.active = false;
                        return;
                    }
                    break;
            }

            NPC.spriteDirection = NPC.direction;
            Time++;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 10)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;

                if(HandOut)
                {
                    if (NPC.frame.Y < frameHeight * 8)
                        NPC.frame.Y = frameHeight * 8;

                    if (NPC.frame.Y > frameHeight * 15)
                    {
                        NPC.frame.Y = frameHeight * 8;
                    }
                }
                else if (NPC.frame.Y > frameHeight * 7)
                    NPC.frame.Y = 0;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (State == DustDjinnState.Idle)
            {
                State = Main.rand.NextBool() ? DustDjinnState.Punch : DustDjinnState.Dive;
                Time = 0;
                NPC.velocity = Vector2.Zero;
                Exhaustion = 1;
                NPC.TargetClosest();
                return;
            }

            if (NPC.life <= 0)
            {
                if(true)//!NPCExtensions.BeenKilled<DesertDjinn>() && Main.rand.NextBool(5))
                {
                    NPC.life = 1;
                    NPC.dontTakeDamage = true;
                    State = DustDjinnState.CallBackup;
                    Time = 0;
                    return;
                }

                for (int i = 0; i < 24; i++)
                {
                    int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<SandDust>());
                    //Main.dust[d].velocity.X *= 0f;
                    Main.dust[d].scale *= 1.3f;
                    Main.dust[d].noGravity = false;
                }
            }
        }

        public override void OnKill()
        {
            Main.BestiaryTracker.Kills.RegisterKill(ContentSamples.NpcsByNetId[ModContent.NPCType<DustDjinn>()]);
        }
    }
}
