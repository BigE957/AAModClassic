
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using AAModClassic.Globals;

namespace AAModClassic.NPCs.Bosses.Akuma.Awakened
{
    public class SunA : ModNPC
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Oni Sun");
        }

		public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 32;
            NPC.aiStyle = -1;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.damage = 70;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Rectangle SunFrame = new Rectangle(0, 0, 64, 64);
            BaseDrawing.DrawTexture(spriteBatch, Mod.GetTexture("NPCs/Bosses/Akuma/Awakened/SunA1"), 0, NPC.position + new Vector2(0, NPC.gfxOffY), NPC.width, NPC.height, NPC.scale, -NPC.rotation, NPC.spriteDirection, 1, SunFrame, AAColor.COLOR_WHITEFADE1, true);
            BaseDrawing.DrawTexture(spriteBatch, Mod.GetTexture("NPCs/Bosses/Akuma/Awakened/SunA"), 0, NPC.position + new Vector2(0, NPC.gfxOffY), NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.spriteDirection, 1, SunFrame, AAColor.COLOR_WHITEFADE1, true);
            return false;
        }

        public override void AI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }
            else
            {
                NPC.alpha -= 5;
            }
            if (NPC.ai[0]++ > 450 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                SoundEngine.PlaySound(SoundID.Item14, NPC.position);
                Vector2 position = NPC.Center + (Vector2.One * -20f);
                int num84 = 40;
                int height3 = num84;
                for (int num85 = 0; num85 < 3; num85++)
                {
                    int num86 = Dust.NewDust(position, num84, height3, DustID.Granite, 0f, 0f, 100, default, 1.5f);
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
                NPC.active = false;
                NPC.netUpdate = true;
            }
            NPC.velocity = Vector2.Zero;
            NPC.rotation -= NPC.direction * 6.28318548f / 120f;
            NPC.scale = NPC.Opacity;
            Lighting.AddLight(NPC.Center, new Vector3(0.9f, 0.6f, 0f) * NPC.Opacity);
            if (Main.rand.Next(2) == 0)
            {
                Vector2 vector135 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
                Dust dust31 = Main.dust[Dust.NewDust(NPC.Center - vector135 * 30f, 0, 0, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 0, default, 1f)];
                dust31.noGravity = true;
                dust31.position = NPC.Center - vector135 * Main.rand.Next(10, 21);
                dust31.velocity = vector135.RotatedBy(1.5707963705062866, default) * 6f;
                dust31.scale = 0.5f + Main.rand.NextFloat();
                dust31.fadeIn = 0.5f;
                dust31.customData = NPC.Center;
            }
            if (Main.rand.Next(2) == 0)
            {
                Vector2 vector136 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
                Dust dust32 = Main.dust[Dust.NewDust(NPC.Center - vector136 * 30f, 0, 0, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 0, default, 1f)];
                dust32.noGravity = true;
                dust32.position = NPC.Center - vector136 * 30f;
                dust32.velocity = vector136.RotatedBy(-1.5707963705062866, default) * 3f;
                dust32.scale = 0.5f + Main.rand.NextFloat();
                dust32.fadeIn = 0.5f;
                dust32.customData = NPC.Center;
            }
            if (NPC.ai[0] < 0f)
            {
                Vector2 center15 = NPC.Center;
                int num1059 = Dust.NewDust(center15 - Vector2.One * 8f, 16, 16, ModContent.DustType<Dusts.AkumaDust>(), NPC.velocity.X / 2f, NPC.velocity.Y / 2f, 0);
                Main.dust[num1059].velocity *= 2f;
                Main.dust[num1059].noGravity = true;
                Main.dust[num1059].scale = Utils.SelectRandom(Main.rand, new float[]
                {
                    0.8f,
                    1.65f
                });
                Main.dust[num1059].customData = this;
            }

            BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, ModContent.ProjectileType<AkumaRock>(), ref NPC.ai[1], 100, NPC.damage / 4, 7, true);
        }
    }
}
