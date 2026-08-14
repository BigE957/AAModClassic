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
            Projectile.aiStyle = 0;
            AIType = ProjectileID.None;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.timeLeft = 360;
            Projectile.trap = false;
        }

        public ref float Time => ref Projectile.ai[2];
        public bool BeenHit { get => Projectile.ai[1] == 1; set => Projectile.ai[1] = (value ? 1  : 0); }

        public override void AI()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<DesertDjinn_Unofficial>()))
                Projectile.tileCollide = true;

            Projectile.velocity.Y += 0.3f;

            if (!BeenHit)
            {
                Projectile.rotation += Projectile.velocity.Y * Projectile.direction;
            }
            else
                Projectile.rotation += Projectile.velocity.X * Projectile.direction;
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
