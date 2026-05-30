using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons;
using AAModClassic._Content.Hoard.__Hardmode.NPCs.__BossGreed;
using AAModClassic._Content.Hoard.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard._PostMoonlord.NPCs.__BossGreedA
{
    public class SingularityOfDesire : ModNPC
    {
        public override string Texture => ModContent.GetInstance<SparkOfDesire>().Texture;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Singularity of Desire");
            Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 4500;
            NPC.defense = 100;
            NPC.width = 60;
            NPC.height = 60;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.Tink;
            NPC.DeathSound = SoundID.Item14;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            SpawnModBiomes = [ModContent.GetInstance<HoardBiome>().Type];
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            int damage = 34;


            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }
            else
            {
                NPC.alpha -= 3;
            }

            Vector2 npcCenter = new Vector2(NPC.Center.X, NPC.Center.Y);

            if (NPC.ai[0] == 0)
            {
                NPC.ai[0] = Main.rand.Next(1, 4);
            }

            if (NPC.ai[0] == 1)
            {
                int type = ModContent.ProjectileType<SingularityOfDesire_DesireBlast>();
                float Speed = 8f;
                float rotation = (float)Math.Atan2(npcCenter.Y - (player.position.Y + player.height * 0.5f), npcCenter.X - (player.position.X + player.width * 0.5f));

                if (++NPC.ai[1] >= 80)
                {
                    SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.position);
                    int proj = Projectile.NewProjectile(NPC.GetSource_FromThis(), npcCenter.X, npcCenter.Y, (float)(Math.Cos(rotation) * Speed * -1), (float)(Math.Sin(rotation) * Speed * -1), type, damage, 0f, 0);
                    Main.projectile[proj].netUpdate = true;
                    NPC.ai[1] = 0;
                }
            }
            if (NPC.ai[0] == 2)
            {
                int type = ModContent.ProjectileType<SingularityOfDesire_DesireBlast>();
                float Speed = 7f;
                float rotation = (float)Math.Atan2(npcCenter.Y - (player.position.Y + player.height * 0.5f), npcCenter.X - (player.position.X + player.width * 0.5f));

                if (++NPC.ai[1] >= 120)
                {
                    SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.position);
                    int proj = Projectile.NewProjectile(NPC.GetSource_FromThis(), npcCenter.X, npcCenter.Y, (float)(Math.Cos(rotation) * Speed * -1), (float)(Math.Sin(rotation) * Speed * -1), type, damage, 0f, 0);
                    int proj2 = Projectile.NewProjectile(NPC.GetSource_FromThis(), npcCenter.X, npcCenter.Y, (float)(Math.Cos(rotation) * Speed * -1) + 2, (float)(Math.Sin(rotation) * Speed * -1) + 2, type, damage, 0f, 0);
                    int proj3 = Projectile.NewProjectile(NPC.GetSource_FromThis(), npcCenter.X, npcCenter.Y, (float)(Math.Cos(rotation) * Speed * -1) - 2, (float)(Math.Sin(rotation) * Speed * -1) - 2, type, damage, 0f, 0);
                    Main.projectile[proj].netUpdate = true;
                    NPC.ai[1] = 0;
                }
            }
            if (NPC.ai[0] >= 3)
            {
                int type = ModContent.ProjectileType<SingularityOfDesire_DesireBeam>();
                float Speed = 10f;
                float rotation = (float)Math.Atan2(npcCenter.Y - player.Center.Y, npcCenter.X - player.Center.X);

                if (++NPC.ai[1] >= 200)
                {
                    SoundEngine.PlaySound(SoundID.DD2_BetsysWrathShot, NPC.position);
                    int proj = Projectile.NewProjectile(NPC.GetSource_FromThis(), npcCenter.X, npcCenter.Y, (float)(Math.Cos(rotation) * Speed * -1), (float)(Math.Sin(rotation) * Speed * -1), type, damage, 0f, 0);
                    Main.projectile[proj].netUpdate = true;
                    NPC.ai[1] = 0;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Width = 70;
            NPC.frame.Height = 70;

            if (++NPC.frameCounter >= 8)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += 70;
                if (NPC.frame.Y >= 210)
                {
                    NPC.frame.Y = 0;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center + new Vector2(0, NPC.gfxOffY) - screenPos, NPC.frame, NPC.GetAlpha(AAColor.COLOR_WHITEFADE1), NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, 0, 0);
            return false;
        }
    }
}