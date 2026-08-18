using AAModClassic.Particles;
using AAModClassic.Particles.Types;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Desert.NPCs._BossDesertDjinn
{
    public class CactusBaseball : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.RollingCactus}";

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.RollingCactus);
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.aiStyle = 0;
            AIType = ProjectileID.None;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.timeLeft = 360;
            Projectile.trap = false;
        }

        public ref float Time => ref Projectile.ai[0];
        public bool BeenHit { get => Projectile.ai[1] == 1; set => Projectile.ai[1] = (value ? 1  : 0); }

        public override void AI()
        {
            if (Time == 0)
            {
                SoundEngine.PlaySound(SoundID.Item39, Projectile.Center);
                Point tileCoords = Projectile.Center.ToTileCoordinates();
                WorldGen.KillTile(tileCoords.X, tileCoords.Y, effectOnly: true);

                if (Framing.GetTileSafely(tileCoords).TileType == TileID.Sand)
                {
                    Vector2 spawnPos = tileCoords.ToWorldCoordinates();
                    for (int j = 0; j < 5; j++)
                    {
                        LargeDust d = new(spawnPos + Main.rand.NextVector2Circular(8f, 4f), new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, -5 - (j * 2))), new Color(212, 192, 100), new Color(212, 192, 100) * 0.5f, Main.rand.NextFloat(0.75f, 1.5f), 200, Main.rand.NextFloat(0.01f, 0.05f));
                        ParticleSystem.SpawnParticle(d, DrawLayer.AfterPlayers);
                    }
                }
            }
            else if (Time < 0)
            {
                Projectile.Center -= Projectile.velocity;
                Time++;
                return;
            }

            if (!NPC.AnyNPCs(ModContent.NPCType<DesertDjinn_Unofficial>()))
                Projectile.tileCollide = true;

            Projectile.velocity.Y += 0.3f;

            if (!BeenHit)
                Projectile.rotation += Projectile.velocity.Y * Projectile.direction;
            else
                Projectile.rotation += Projectile.velocity.X * Projectile.direction;

            Projectile.hostile = BeenHit;
            Time++;
        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int amount = Main.rand.Next(3, 6);
                float startOff = Main.rand.NextFloat();
                for (float ratio = 0f; ratio < 1f; ratio += 1f / amount)
                {
                    Vector2 velocity = ((startOff + ratio) * MathHelper.TwoPi).ToRotationVector2() * 2f;
                    if (velocity.Y > 0f)
                        velocity *= -0.7f;

                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity, ProjectileID.RollingCactusSpike, 10, 2f, Main.myPlayer);
                }
            }
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindNPCsAndTiles.Add(index);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, null, Lighting.GetColor(Projectile.Center.ToTileCoordinates()), Projectile.rotation, TextureAssets.Projectile[Type].Size() * 0.5f, Projectile.scale, 0);
            return false;
        }
    }
}
