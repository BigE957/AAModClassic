
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.AH.Haruka
{
    public class HarukaVanish : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Haruka Yamata");
            Main.npcFrameCount[NPC.type] = 17;
        }

        public override void SetDefaults()
        {
            NPC.dontTakeDamage = true;
            NPC.lifeMax = 1;
            NPC.width = 90;
            NPC.height = 72;
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
                    if (!NPC.collideY)
                    {
                        if (NPC.ai[1] > 3)
                        {
                            NPC.ai[1] = 0;
                        }
                        return;
                    }
                    else
                    {
                        if (NPC.ai[1] < 4)
                        {
                            NPC.ai[1] = 4;
                        }
                    }
                    if (NPC.frame.Y >= (92 * 12))
                    {
                        NPC.ai[2] = 1;
                        SoundEngine.PlaySound(SoundID.Item14, NPC.position);
                        Vector2 position = NPC.Center + (Vector2.One * -20f);
                        int num84 = 40;
                        int height3 = num84;
                        for (int num85 = 0; num85 < 3; num85++)
                        {
                            int num86 = Dust.NewDust(position, num84, height3, 240, 0f, 0f, 100, default, 1.5f);
                            Main.dust[num86].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                        }
                        for (int num87 = 0; num87 < 15; num87++)
                        {
                            int num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 200, default, 3.7f);
                            Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                            Main.dust[num88].noGravity = true;
                            Main.dust[num88].noLight = true;
                            Main.dust[num88].velocity *= 3f;
                            Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                            num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.YamataDust>(), 0f, 0f, 100, default, 1.5f);
                            Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                            Main.dust[num88].velocity *= 2f;
                            Main.dust[num88].noGravity = true;
                            Main.dust[num88].fadeIn = 1f;
                            Main.dust[num88].color = Color.Crimson * 0.5f;
                            Main.dust[num88].noLight = true;
                            Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * 8f;
                        }
                        for (int num89 = 0; num89 < 10; num89++)
                        {
                            int num90 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 0, default, 2.7f);
                            Main.dust[num90].position = NPC.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f);
                            Main.dust[num90].noGravity = true;
                            Main.dust[num90].noLight = true;
                            Main.dust[num90].velocity *= 3f;
                            Main.dust[num90].velocity += NPC.DirectionTo(Main.dust[num90].position) * 2f;
                        }
                        for (int num91 = 0; num91 < 30; num91++)
                        {
                            int num92 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.YamataDust>(), 0f, 0f, 0, default, 1.5f);
                            Main.dust[num92].position = NPC.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f);
                            Main.dust[num92].noGravity = true;
                            Main.dust[num92].velocity *= 3f;
                            Main.dust[num92].velocity += NPC.DirectionTo(Main.dust[num92].position) * 3f;
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
            Texture2D glowTex = Mod.GetTexture("Glowmasks/HarukaVanish_Glow");

            BaseDrawing.DrawTexture(spritebatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.spriteDirection, 17, NPC.frame, NPC.GetAlpha(dColor), true);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.spriteDirection, 17, NPC.frame, Color.White, true);
            BaseDrawing.DrawAfterimage(spritebatch, glowTex, 0, NPC, 0.8f, 1f, 4, true, 0f, 0f, Color.White, NPC.frame, 17);
            return false;
        }
    }
}
