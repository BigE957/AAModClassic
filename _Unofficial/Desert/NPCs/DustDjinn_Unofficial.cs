using AAModClassic._Content.Desert.___PreHardmode.NPCs._Day;
using AAModClassic.Dusts;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
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
            NPC.knockBackResist = 0.4f;
            NPC.noTileCollide = true;
            NPC.noGravity = true;

            NPC.npcSlots = 2;
        }

        internal ref float Time => ref NPC.ai[0];

        internal enum DustDjinnState
        {
            Idle,
            Startled,
            Exhausted,
            Punch,
            Conjure,
            Dive,
            CallBackup
        }

        internal DustDjinnState State = DustDjinnState.Idle;

        public Player Target => Main.player[NPC.target];

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
                            State = DustDjinnState.Punch; /*Main.rand.Next(3) switch
                            {
                                0 => DustDjinnState.Punch,
                                1 => DustDjinnState.Slam,
                                _ => DustDjinnState.Dive
                            };*/
                            Time = 0;
                            NPC.velocity = Vector2.Zero;
                            return;
                        }
                    }
                    break;
                case DustDjinnState.Punch:
                    if(Time < 60)
                    {
                        NPC.direction = NPC.Center.X > Target.Center.X ? 1 : -1;
                        NPC.velocity.X = (Target.Center.X + (150 * NPC.direction) - NPC.Center.X) / 10f;
                        float lerp = MathHelper.Clamp(Time / 60f, 0f, 1f);
                        NPC.velocity.Y = (Target.Center.Y - NPC.Center.Y) * MathHelper.Lerp(0.1f, 0f, lerp);
                    }
                    else if(Time == 60)
                    {
                        NPC.direction = NPC.Center.X < Target.Center.X ? 1 : -1;
                        SoundEngine.PlaySound(SoundID.Item1, NPC.Center);
                        NPC.velocity = Vector2.UnitX * 16f * NPC.direction;
                    }
                    else
                    {
                        NPC.velocity *= 0.94f;
                    }
                    break;
                case DustDjinnState.Conjure:
                    break;
                case DustDjinnState.Dive:
                    break;
                case DustDjinnState.CallBackup:
                    break;
            }

            Time++;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 10)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y > frameHeight * 7)
                    NPC.frame.Y = 0;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<SandDust>());
                Main.dust[d].velocity.X *= 0f;
                Main.dust[d].scale *= 1.3f;
                Main.dust[d].noGravity = false;
                d = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<SandDust>());
                Main.dust[d].velocity.X *= 0f;
                Main.dust[d].scale *= 1.3f;
                Main.dust[d].noGravity = false;
            }
        }

        public override void OnKill()
        {
            Main.BestiaryTracker.Kills.RegisterKill(ContentSamples.NpcsByNetId[ModContent.NPCType<DustDjinn>()]);
        }
    }
}
