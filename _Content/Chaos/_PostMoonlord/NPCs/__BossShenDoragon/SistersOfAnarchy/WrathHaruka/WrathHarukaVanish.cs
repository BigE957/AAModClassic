
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.SistersOfAnarchy.WrathHaruka
{
    public class WrathHarukaVanish : ModNPC
    {
        public static Asset<Texture2D> Glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wrath Haruka");     
            Main.npcFrameCount[NPC.type] = 17;

            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.dontTakeDamage = true;
            NPC.lifeMax = 1;
            NPC.width = 90;
            NPC.height = 78;
            NPC.friendly = false;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.timeLeft = 10;

            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override void AI()
        {
            NPC.velocity.Y += .1f;

            NPC.frame.Y = 78 * (int)NPC.ai[1];

            if (NPC.ai[2] == 0)
            {
                if (++NPC.ai[0] >= 6)
                {
                    NPC.ai[0] = 0;
                    NPC.ai[1] += 1;
                    if (NPC.frame.Y > 92 * 12)
                    {
                        NPC.ai[2] = 1;
                        SoundEngine.PlaySound(SoundID.Item14, NPC.position);
                        Vector2 position = NPC.Center + Vector2.One * -20f;
                        int num84 = 40;
                        int height3 = num84;
                        for (int num85 = 0; num85 < 3; num85++)
                        {
                            int num86 = Dust.NewDust(position, num84, height3, DustID.Granite, 0f, 0f, 100, default, 1.5f);
                            Main.dust[num86].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                        }
                        for (int num87 = 0; num87 < 15; num87++)
                        {
                            int num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 200, default, 3.7f);
                            Main.dust[num88].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                            Main.dust[num88].noGravity = true;
                            Main.dust[num88].noLight = true;
                            Main.dust[num88].velocity *= 3f;
                            Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + Main.rand.NextFloat() * 4f);
                            num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.YamataDust>(), 0f, 0f, 100, default, 1.5f);
                            Main.dust[num88].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                            Main.dust[num88].velocity *= 2f;
                            Main.dust[num88].noGravity = true;
                            Main.dust[num88].fadeIn = 1f;
                            Main.dust[num88].color = Color.Crimson * 0.5f;
                            Main.dust[num88].noLight = true;
                            Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * 8f;
                        }
                        for (int num89 = 0; num89 < 10; num89++)
                        {
                            NPC.ai[2] = 1;
                            for (int Loop = 0; Loop < 20; Loop++)
                            {
                                int Smoke2 = Dust.NewDust(new Vector2(NPC.Center.X, NPC.Center.Y + 31), NPC.width, NPC.height, DustID.RedsWingsRun, 1 * Main.rand.NextFloat(-1, 1), -1, 0);
                                Main.dust[Smoke2].noGravity = true;
                                Main.dust[Smoke2].noLight = true;
                                int Smoke = Dust.NewDust(new Vector2(NPC.Center.X, NPC.Center.Y + 31), NPC.width, NPC.height, DustID.RedsWingsRun, 1 * Main.rand.NextFloat(-1, 1), -1, 0, default, 2f);
                                Main.dust[Smoke].noGravity = true;
                                Main.dust[Smoke].noLight = true;
                            }
                        }
                    }
                }
            }
            else
            {
                NPC.alpha += 15;
                if (NPC.alpha > 255)
                {
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
                if (++NPC.ai[0] >= 6)
                {
                    NPC.ai[0] = 0;
                    NPC.ai[1] += 1;
                    if (NPC.ai[1] < 13 || NPC.ai[1] > 16)
                    {
                        NPC.ai[1] = 13;
                    }
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glowTex = Glowmask.Value;

            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, NPC.GetAlpha(drawColor), NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, SpriteEffects.None, 0);
            spriteBatch.Draw(glowTex, NPC.Center - screenPos, NPC.frame, Color.White, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, SpriteEffects.None, 0);
            BaseDrawing.DrawAfterimage(spriteBatch, glowTex, 0, NPC, 0.8f, 1f, 4, true, 0f, 0f, Color.White, NPC.frame, 17);
            return false;
        }
    }
}
