using AAModClassic._CrossMod.Fables;
using AAModClassic._CrossMod.SpiritReforged;
using AAModClassic.Particles;
using AAModClassic.Particles.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using SpiritReforged.Content.Desert.NPCs.Cactus;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Desert.NPCs._BossDesertDjinn
{
    public class CactusBaseball : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.RollingCactus}";

        private static readonly List<(string id, Asset<Texture2D> texture)> Textures = [];
        private static Asset<Texture2D> stactusFaces = null;

        public override void SetStaticDefaults()
        {
            Textures.Add(("Default", TextureAssets.Projectile[Type]));
            if (SpiritReforgedManager.IsEnabled)
            { 
                Textures.Add(("Stactus", ModContent.Request<Texture2D>("SpiritReforged/Content/Desert/NPCs/Cactus/DesertStactus")));
                stactusFaces = ModContent.Request<Texture2D>("SpiritReforged/Content/Desert/NPCs/Cactus/DesertStactusFaces");
            }
            if (CalamityFables.IsEnabled)
            {
                Textures.Add(("ScourgeSpine1", ModContent.Request<Texture2D>("CalamityFables/Assets/Gores/ScourgeSpineGore1")));
                Textures.Add(("ScourgeSpine3", ModContent.Request<Texture2D>("CalamityFables/Assets/Gores/ScourgeSpineGore3")));
                Textures.Add(("ScourgeSpine5", ModContent.Request<Texture2D>("CalamityFables/Assets/Gores/ScourgeSpineGore5")));
            }
        }

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

        public override void OnSpawn(IEntitySource source)
        {
            if (Textures.Count > 1)
            {
                Projectile.ai[2] = Main.rand.Next(Textures.Count);
                string id = Textures[(int)Projectile.ai[2]].id;
                if (id == "Stactus")
                    Projectile.frame = Main.rand.Next(6);
                Projectile.netUpdate = true;
            }
        }

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
                        LargeDust d = new(spawnPos + Main.rand.NextVector2Circular(8f, 4f), new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, -5 - (j * 2))), new Color(212, 192, 100), new Color(212, 192, 100), Main.rand.NextFloat(0.75f, 1.5f), 200, Main.rand.NextFloat(0.01f, 0.05f));
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
                Projectile.rotation += Projectile.direction * 0.3f;
            else
                Projectile.rotation += Projectile.velocity.X * Projectile.direction * 0.3f;

            Projectile.hostile = BeenHit;
            Time++;
        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                string id = Textures[(int)Projectile.ai[2]].id;

                if (id == "Default")
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
                else if(id == "Stactus")
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.Center);

                    for (int i = 0; i < 5; i++)
                        Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center - Projectile.velocity, Main.rand.NextVector2Unit() * Main.rand.NextFloat(1, 2.2f), ModContent.ProjectileType<CactusSpine>(), 10, 1, ai0: Main.rand.Next(0, 3));
                
                    if(!Main.dedServ)
                    {
                        Vector2 velocity = new Vector2(Projectile.velocity.X * 0.1f, Projectile.velocity.Y);
                        Gore.NewGore(Projectile.GetSource_Death(), Projectile.position, velocity, SpiritReforgedManager.spiritReforged.Find<ModGore>("DesertStactusHead" + Main.rand.Next(1, 3)).Type);

                        if (Projectile.frame > 0 && Projectile.frame < 5)
                        {
                            string[] crowns = ["Juvenile", "FlowerCrown", "Blossom", "Bouquet", "Garland", "PricklyPears"];
                            Gore.NewGore(Projectile.GetSource_Death(), Projectile.position, velocity, SpiritReforgedManager.spiritReforged.Find<ModGore>("DesertStactus" + crowns[Projectile.frame] + 1).Type);

                            for (int i = 0; i < 2; i++)
                                Gore.NewGore(Projectile.GetSource_Death(), Projectile.position, velocity, SpiritReforgedManager.spiritReforged.Find<ModGore>("DesertStactus" + crowns[Projectile.frame] + 2).Type);

                            for (int i = 0; i < 4; i++)
                                Gore.NewGore(Projectile.GetSource_Death(), Projectile.position, velocity, SpiritReforgedManager.spiritReforged.Find<ModGore>("DesertStactus" + crowns[Projectile.frame] + 3).Type);
                        }
                    }
                }
                else
                    SoundEngine.PlaySound(SoundID.DD2_SkeletonHurt, Projectile.Center);
            }
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindNPCsAndTiles.Add(index);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var data = Textures[(int)Projectile.ai[2]];
            string id = data.id;
            Texture2D tex = data.texture.Value;

            if(id == "Stactus")
            {
                Rectangle frame = tex.Frame(4, 10, 3, Projectile.frame);
                Rectangle faceFrame = stactusFaces.Frame(1, 3, 0, BeenHit ? 2 : 1);
                Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, frame, Lighting.GetColor(Projectile.Center.ToTileCoordinates()), Projectile.rotation, frame.Size() * 0.5f, Projectile.scale, 0);
                Main.EntitySpriteDraw(stactusFaces.Value, Projectile.Center - Main.screenPosition, faceFrame, Lighting.GetColor(Projectile.Center.ToTileCoordinates()), Projectile.rotation, faceFrame.Size() * 0.5f, Projectile.scale, 0);
            }
            else
                Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, null, Lighting.GetColor(Projectile.Center.ToTileCoordinates()), Projectile.rotation, tex.Size() * 0.5f, Projectile.scale, 0);
            return false;
        }
    }
}
