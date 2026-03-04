using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic.NPCs.Bosses.Anubis.Forsaken
{
	public class HorusSentry : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.width = 60;
            NPC.height = 48;
            NPC.value = BaseUtility.CalcValue(0, 0, 0, 0);
            NPC.npcSlots = 1;
            NPC.aiStyle = -1;
            NPC.lifeMax = 1200;
            NPC.defense = 30;
            NPC.damage = 60;
            NPC.HitSound = SoundID.NPCHit49;
            NPC.DeathSound = SoundID.NPCDeath51;
            NPC.knockBackResist = 0.7f;
            NPC.alpha = 255;
            NPC.noGravity = true;
        }

        public override void AI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            int FrameHeight = 48;
            if (NPC.localAI[0] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item121, NPC.position);
                NPC.localAI[0] = 1f;
            }

            if (NPC.alpha <= 0)
            {
                NPC.alpha = 0;
            }
            else
            {
                NPC.alpha -= 5;
            }

            NPC.ai[0] += 1f;

            if (!NPC.AnyNPCs(ModContent.NPCType<ForsakenAnubis>()))
            {
                NPC.StrikeInstantKill();
            }

            if (NPC.ai[0] == 160 || Vector2.Distance(NPC.Center, player.Center) > 3000)
            {
                SoundEngine.PlaySound(SoundID.Item121, NPC.position);
                Teleport();
            }

            if (NPC.ai[0] < 180)
            {
                NPC.frame.Y = 0;
            }
            if (NPC.ai[0] > 180)
            {
                NPC.frame.Y = FrameHeight;
            }
            if (NPC.ai[0] > 220)
            {
                NPC.frame.Y = FrameHeight * 2;
            }
            if (NPC.ai[0] > 260)
            {
                NPC.frame.Y = FrameHeight * 3;
            }
            if (NPC.ai[0] > 300)
            {
                NPC.frame.Y = FrameHeight * 3;
            }
            if (NPC.ai[0] >= 340 && NPC.ai[0] <= 460)
            {
                BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, ModContent.ProjectileType<CurseFlame>(), ref NPC.ai[1], 20, NPC.damage / 2, 10, true);
            }
            if (NPC.ai[0] > 460)
            {
                NPC.frame.Y = FrameHeight * 2;
            }
            if (NPC.ai[0] > 500)
            {
                NPC.frame.Y = FrameHeight * 1;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 7, NPC.frame, NPC.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            return false;
        }

        public void Teleport()
        {
            Player player = Main.player[NPC.target];
            Vector2 targetPos = player.Center;
            int posX = Main.rand.Next(3);
            switch (posX)
            {
                case 0:
                    posX = -300;
                    break;
                case 1:
                    posX = 0;
                    break;
                case 2:
                    posX = 300;
                    break;
            }
            int posY = Main.rand.Next(posX == 0 ? 1 : 2);
            switch (posY)
            {
                case 0:
                    posY = -300;
                    break;
                case 1:
                    posY = 0;
                    break;
            }

            NPC.position = new Vector2(targetPos.X + posX, targetPos.Y + posY);

            Vector2 position = NPC.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 15; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 50, default, 3.7f);
                Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 25, default, 1.5f);
                Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 10; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = NPC.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += NPC.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 0, default, 1.5f);
                Main.dust[num92].position = NPC.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += NPC.DirectionTo(Main.dust[num92].position) * 3f;
            }
        }

        public override void OnKill()
        {
            Vector2 position = NPC.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 15; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 50, default, 3.7f);
                Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 25, default, 1.5f);
                Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 10; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = NPC.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += NPC.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 0, default, 1.5f);
                Main.dust[num92].position = NPC.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += NPC.DirectionTo(Main.dust[num92].position) * 3f;
            }
        }
    }

}