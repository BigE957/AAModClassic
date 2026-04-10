using Terraria;
using System;
using Terraria.GameContent;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Audio;

namespace AAModClassic.NPCs.Bosses.Shen
{
    public class Shenling : ModNPC
	{
        public override string Texture => "AAModClassic/NPCs/Bosses/Shen/Shenling";


        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Discordian Serpent");
            Main.npcFrameCount[NPC.type] = 2;
        }
        

        public override void SetDefaults()
		{
			NPC.noTileCollide = true;
			NPC.height = 16;
			NPC.width = 16;
			NPC.aiStyle = -1;
			NPC.netAlways = true;
			NPC.knockBackResist = 0f;
            NPC.damage = 50;
            NPC.defense = 90;
            NPC.lifeMax = 8000;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.behindTiles = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.Item124;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.buffImmune[103] = false;
            NPC.alpha = 255;
        }


        public override void AI()
        {
            if (NPC.localAI[3] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item119, NPC.position);
                NPC.localAI[3] = 1f;
            }
            NPC.dontTakeDamage = NPC.alpha > 0;
            if (NPC.dontTakeDamage)
            {
                for (int j = 0; j < 2; j++)
                {
                    int num2 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.GoldFlame, 0f, 0f, 100, default, 2f);
                    Main.dust[num2].noGravity = true;
                    Main.dust[num2].noLight = true;
                }
            }
            NPC.alpha -= 42;
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }

            bool flag = false;
            float num4 = 0.2f;
            int num5 = NPC.type;
            flag = true;

            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || (flag && Main.player[NPC.target].position.Y < Main.worldSurface * 16.0))
            {
                NPC.TargetClosest(true);
            }
            if (Main.player[NPC.target].dead || (flag && Main.player[NPC.target].position.Y < Main.worldSurface * 16.0))
            {
                if (NPC.timeLeft > 300)
                {
                    NPC.timeLeft = 300;
                }
                if (flag)
                {
                    NPC.velocity.Y = NPC.velocity.Y + num4;
                }
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {

                if (NPC.ai[0] == 0f)
                {
                    NPC.ai[3] = NPC.whoAmI;
                    NPC.realLife = NPC.whoAmI;
                    int num9 = NPC.whoAmI;
                    for (int l = 0; l < 10; l++)
                    {
                        int Body1 = ModContent.NPCType<ShenlingBody1>();
                        int Body2 = ModContent.NPCType<ShenlingBody2>();

                        int SpawnBody1 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.position.X + NPC.width / 2), (int)(NPC.position.Y + NPC.height), Body1, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                        Main.npc[SpawnBody1].ai[3] = NPC.whoAmI;
                        Main.npc[SpawnBody1].realLife = NPC.whoAmI;
                        Main.npc[SpawnBody1].ai[1] = num9;
                        Main.npc[num9].ai[0] = SpawnBody1;
                        num9 = SpawnBody1;
                        NPC.netUpdate = true;

                        int SpawnBody2 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.position.X + NPC.width / 2), (int)(NPC.position.Y + NPC.height), Body2, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                        Main.npc[SpawnBody2].ai[3] = NPC.whoAmI;
                        Main.npc[SpawnBody2].realLife = NPC.whoAmI;
                        Main.npc[SpawnBody2].ai[1] = num9;
                        Main.npc[num9].ai[0] = SpawnBody2;
                        num9 = SpawnBody2;
                        NPC.netUpdate = true;
                    }
                    int num10 = ModContent.NPCType<ShenlingTail>();
                    if(Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int num11 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.position.X + NPC.width / 2), (int)(NPC.position.Y + NPC.height), num10, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                        if (Main.netMode == NetmodeID.Server && num11 < 200) NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num11);
                        Main.npc[num11].ai[3] = NPC.whoAmI;
                        Main.npc[num11].realLife = NPC.whoAmI;
                        Main.npc[num11].ai[1] = num9;
                        Main.npc[num9].ai[0] = num11;
                        num9 = num11;
                    }
                    NPC.netUpdate = true;
                }
            }
            int num29 = (int)(NPC.position.X / 16f) - 1;
            int num30 = (int)((NPC.position.X + NPC.width) / 16f) + 2;
            int num31 = (int)(NPC.position.Y / 16f) - 1;
            int num32 = (int)((NPC.position.Y + NPC.height) / 16f) + 2;
            if (num29 < 0)
            {
                num29 = 0;
            }
            if (num30 > Main.maxTilesX)
            {
                num30 = Main.maxTilesX;
            }
            if (num31 < 0)
            {
                num31 = 0;
            }
            if (num32 > Main.maxTilesY)
            {
                num32 = Main.maxTilesY;
            }
            if (NPC.velocity.X < 0f)
            {
                NPC.spriteDirection = 1;
            }
            else if (NPC.velocity.X > 0f)
            {
                NPC.spriteDirection = -1;
            }
            float num37 = 10f;
            float num38 = 0.07f;
            num37 = 20f;
            num38 = 0.55f;

            Vector2 vector2 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
            float num40 = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2;
            float num41 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2;

            num40 = (int)(num40 / 16f) * 16;
            num41 = (int)(num41 / 16f) * 16;
            vector2.X = (int)(vector2.X / 16f) * 16;
            vector2.Y = (int)(vector2.Y / 16f) * 16;
            num40 -= vector2.X;
            num41 -= vector2.Y;

            float num53 = (float)Math.Sqrt(num40 * num40 + num41 * num41);
            if (NPC.ai[1] > 0f && NPC.ai[1] < Main.npc.Length)
            {
                try
                {
                    vector2 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                    num40 = Main.npc[(int)NPC.ai[1]].position.X + Main.npc[(int)NPC.ai[1]].width / 2 - vector2.X;
                    num41 = Main.npc[(int)NPC.ai[1]].position.Y + Main.npc[(int)NPC.ai[1]].height / 2 - vector2.Y;
                }
                catch
                {
                }
                NPC.rotation = (float)Math.Atan2(num41, num40) + 1.57f;
                num53 = (float)Math.Sqrt(num40 * num40 + num41 * num41);
                int num54 = NPC.width;
                num54 = 42;
                num53 = (num53 - num54) / num53;
                num40 *= num53;
                num41 *= num53;
                NPC.velocity = Vector2.Zero;
                NPC.position.X = NPC.position.X + num40;
                NPC.position.Y = NPC.position.Y + num41;
                if (num40 < 0f)
                {
                    NPC.spriteDirection = 1;
                    return;
                }
                if (num40 > 0f)
                {
                    NPC.spriteDirection = -1;
                    return;
                }
            }
            else
            {
                num53 = (float)Math.Sqrt(num40 * num40 + num41 * num41);
                float num56 = Math.Abs(num40);
                float num57 = Math.Abs(num41);
                float num58 = num37 / num53;
                num40 *= num58;
                num41 *= num58;
                bool flag6 = false;
                if (((NPC.velocity.X > 0f && num40 < 0f) || (NPC.velocity.X < 0f && num40 > 0f) || (NPC.velocity.Y > 0f && num41 < 0f) || (NPC.velocity.Y < 0f && num41 > 0f)) && Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) > num38 / 2f && num53 < 300f)
                {
                    flag6 = true;
                    if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num37)
                    {
                        NPC.velocity *= 1.1f;
                    }
                }
                if (NPC.position.Y > Main.player[NPC.target].position.Y || Main.player[NPC.target].dead)
                {
                    flag6 = true;
                    if (Math.Abs(NPC.velocity.X) < num37 / 2f)
                    {
                        if (NPC.velocity.X == 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X - NPC.direction;
                        }
                        NPC.velocity.X = NPC.velocity.X * 1.1f;
                    }
                    else if (NPC.velocity.Y > -num37)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - num38;
                    }
                }
                if (!flag6)
                {
                    if ((NPC.velocity.X > 0f && num40 > 0f) || (NPC.velocity.X < 0f && num40 < 0f) || (NPC.velocity.Y > 0f && num41 > 0f) || (NPC.velocity.Y < 0f && num41 < 0f))
                    {
                        if (NPC.velocity.X < num40)
                        {
                            NPC.velocity.X = NPC.velocity.X + num38;
                        }
                        else if (NPC.velocity.X > num40)
                        {
                            NPC.velocity.X = NPC.velocity.X - num38;
                        }
                        if (NPC.velocity.Y < num41)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num38;
                        }
                        else if (NPC.velocity.Y > num41)
                        {
                            NPC.velocity.Y = NPC.velocity.Y - num38;
                        }
                        if (Math.Abs(num41) < num37 * 0.2 && ((NPC.velocity.X > 0f && num40 < 0f) || (NPC.velocity.X < 0f && num40 > 0f)))
                        {
                            if (NPC.velocity.Y > 0f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y + num38 * 2f;
                            }
                            else
                            {
                                NPC.velocity.Y = NPC.velocity.Y - num38 * 2f;
                            }
                        }
                        if (Math.Abs(num40) < num37 * 0.2 && ((NPC.velocity.Y > 0f && num41 < 0f) || (NPC.velocity.Y < 0f && num41 > 0f)))
                        {
                            if (NPC.velocity.X > 0f)
                            {
                                NPC.velocity.X = NPC.velocity.X + num38 * 2f;
                            }
                            else
                            {
                                NPC.velocity.X = NPC.velocity.X - num38 * 2f;
                            }
                        }
                    }
                    else if (num56 > num57)
                    {
                        if (NPC.velocity.X < num40)
                        {
                            NPC.velocity.X = NPC.velocity.X + num38 * 1.1f;
                        }
                        else if (NPC.velocity.X > num40)
                        {
                            NPC.velocity.X = NPC.velocity.X - num38 * 1.1f;
                        }
                        if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num37 * 0.5)
                        {
                            if (NPC.velocity.Y > 0f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y + num38;
                            }
                            else
                            {
                                NPC.velocity.Y = NPC.velocity.Y - num38;
                            }
                        }
                    }
                    else
                    {
                        if (NPC.velocity.Y < num41)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num38 * 1.1f;
                        }
                        else if (NPC.velocity.Y > num41)
                        {
                            NPC.velocity.Y = NPC.velocity.Y - num38 * 1.1f;
                        }
                        if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num37 * 0.5)
                        {
                            if (NPC.velocity.X > 0f)
                            {
                                NPC.velocity.X = NPC.velocity.X + num38;
                            }
                            else
                            {
                                NPC.velocity.X = NPC.velocity.X - num38;
                            }
                        }
                    }
                }
                NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;

                float num62 = Vector2.Distance(Main.player[NPC.target].Center, NPC.Center);
                int num63 = 0;
                if (Vector2.Normalize(Main.player[NPC.target].Center - NPC.Center).ToRotation().AngleTowards(NPC.velocity.ToRotation(), 1.57079637f) == NPC.velocity.ToRotation() && num62 < 350f)
                {
                    num63 = 4;
                }
                if (num63 > NPC.frameCounter)
                {
                    NPC.frameCounter += 1.0;
                }
                if (num63 < NPC.frameCounter)
                {
                    NPC.frameCounter -= 1.0;
                }
                if (NPC.frameCounter < 0.0)
                {
                    NPC.frameCounter = 0.0;
                }
                if (NPC.frameCounter > 4.0)
                {
                    NPC.frameCounter = 4.0;
                }
            }
        }

        public override void OnKill()
        {
            for (int num468 = 0; num468 < 3; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(NPC.Center.X, NPC.Center.Y), NPC.width, 1, ModContent.DustType<Dusts.DiscordLight>(), -NPC.velocity.X * 0.2f,
                    -NPC.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(NPC.Center.X, NPC.Center.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.DiscordLight>(), -NPC.velocity.X * 0.2f,
                    -NPC.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, NPC.height * 0.5f);
            for (int k = 0; k < NPC.oldPos.Length; k++)
            {
                Texture2D Trail = TextureAssets.Npc[NPC.type].Value;
                Color lightColor = drawColor;
                Vector2 drawPos = NPC.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, NPC.gfxOffY);
                Color color = NPC.GetAlpha(lightColor) * ((NPC.oldPos.Length - k) / (float)NPC.oldPos.Length);
                spriteBatch.Draw(Trail, drawPos, null, color, NPC.rotation, drawOrigin, NPC.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }

    public class ShenlingBody1 : Shenling
    {
        public override string Texture => "AAModClassic/NPCs/Bosses/Shen/ShenlingBody1";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Discordian Serpent");
            Main.npcFrameCount[NPC.type] = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.dontCountMe = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {

                NPC.position.X = NPC.position.X + NPC.width / 2;
                NPC.position.Y = NPC.position.Y + NPC.height / 2;
                NPC.width = 44;
                NPC.height = 78;
                NPC.position.X = NPC.position.X - NPC.width / 2;
                NPC.position.Y = NPC.position.Y - NPC.height / 2;
                int dust1 = ModContent.DustType<Dusts.DiscordLight>();
                int dust2 = ModContent.DustType<Dusts.DiscordLight>();
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust2, 0f, 0f, 0);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }

        public override bool PreAI()
        {
            if (NPC.ai[3] > 0)
                NPC.realLife = (int)NPC.ai[3];
            if (NPC.target < 0 || NPC.target == byte.MaxValue || Main.player[NPC.target].dead)
                NPC.TargetClosest(true);
            if (Main.player[NPC.target].dead && NPC.timeLeft > 300)
                NPC.timeLeft = 300;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!Main.npc[(int)NPC.ai[1]].active)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
            }

            if (Main.npc[(int)NPC.ai[1]].alpha < 128)
            {
                if (NPC.alpha != 0)
                {
                    for (int num934 = 0; num934 < 2; num934++)
                    {
                        int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.DiscordLight>(), 0f, 0f, 100, default, 2f);
                        Main.dust[num935].noGravity = false;
                        Main.dust[num935].noLight = false;
                    }
                }
                NPC.alpha -= 42;
                if (NPC.alpha < 0)
                {
                    NPC.alpha = 0;
                }
            }


            if (NPC.ai[1] < (double)Main.npc.Length)
            {
                Vector2 npcCenter = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float dirX = Main.npc[(int)NPC.ai[1]].position.X + Main.npc[(int)NPC.ai[1]].width / 2 - npcCenter.X;
                float dirY = Main.npc[(int)NPC.ai[1]].position.Y + Main.npc[(int)NPC.ai[1]].height / 2 - npcCenter.Y;
                NPC.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - NPC.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                if (dirX < 0f)
                {
                    NPC.spriteDirection = 1;

                }
                else
                {
                    NPC.spriteDirection = -1;
                }
                NPC.position.X = NPC.position.X + posX;
                NPC.position.Y = NPC.position.Y + posY;
            }
            return false;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[NPC.target];
            if (player.vortexStealthActive && projectile.CountsAsClass(DamageClass.Ranged))
            {
                modifiers.TargetDamageMultiplier /= 2f;
                modifiers.DisableCrit();
            }
            if (projectile.penetrate == -1 && !projectile.minion)
            {
                modifiers.TargetDamageMultiplier *= 0.2f;
            }
            else if (projectile.penetrate > 1) //TODO: was >= 1 but that seemed too strict
            {
               modifiers.TargetDamageMultiplier *= 0.2f;
            }
        }
    }

    public class ShenlingBody2 : Shenling
    {
        public override string Texture => "AAModClassic/NPCs/Bosses/Shen/ShenlingBody2";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Discordian Serpent");
            Main.npcFrameCount[NPC.type] = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.dontCountMe = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {

                NPC.position.X = NPC.position.X + NPC.width / 2;
                NPC.position.Y = NPC.position.Y + NPC.height / 2;
                NPC.width = 44;
                NPC.height = 78;
                NPC.position.X = NPC.position.X - NPC.width / 2;
                NPC.position.Y = NPC.position.Y - NPC.height / 2;
                int dust1 = ModContent.DustType<Dusts.DiscordLight>();
                int dust2 = ModContent.DustType<Dusts.DiscordLight>();
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust2, 0f, 0f, 0);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }

        public override bool PreAI()
        {
            if (NPC.ai[3] > 0)
                NPC.realLife = (int)NPC.ai[3];
            if (NPC.target < 0 || NPC.target == byte.MaxValue || Main.player[NPC.target].dead)
                NPC.TargetClosest(true);
            if (Main.player[NPC.target].dead && NPC.timeLeft > 300)
                NPC.timeLeft = 300;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!Main.npc[(int)NPC.ai[1]].active)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
            }

            if (Main.npc[(int)NPC.ai[1]].alpha < 128)
            {
                if (NPC.alpha != 0)
                {
                    for (int num934 = 0; num934 < 2; num934++)
                    {
                        int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.DiscordLight>(), 0f, 0f, 100, default, 2f);
                        Main.dust[num935].noGravity = false;
                        Main.dust[num935].noLight = false;
                    }
                }
                NPC.alpha -= 42;
                if (NPC.alpha < 0)
                {
                    NPC.alpha = 0;
                }
            }


            if (NPC.ai[1] < (double)Main.npc.Length)
            {
                Vector2 npcCenter = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float dirX = Main.npc[(int)NPC.ai[1]].position.X + Main.npc[(int)NPC.ai[1]].width / 2 - npcCenter.X;
                float dirY = Main.npc[(int)NPC.ai[1]].position.Y + Main.npc[(int)NPC.ai[1]].height / 2 - npcCenter.Y;
                NPC.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - NPC.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                if (dirX < 0f)
                {
                    NPC.spriteDirection = 1;

                }
                else
                {
                    NPC.spriteDirection = -1;
                }
                NPC.position.X = NPC.position.X + posX;
                NPC.position.Y = NPC.position.Y + posY;
            }
            return false;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[NPC.target];
            if (player.vortexStealthActive && projectile.CountsAsClass(DamageClass.Ranged))
            {
                modifiers.TargetDamageMultiplier /= 2f;
                modifiers.DisableCrit();
            }
            if (projectile.penetrate == -1 && !projectile.minion)
            {
                modifiers.TargetDamageMultiplier *= 0.2f;
            }
            else if (projectile.penetrate > 1) //TODO: was >= 1 but that seemed too strict
            {
                modifiers.TargetDamageMultiplier *= 0.2f;
            }
        }
    }

    public class ShenlingTail : Shenling
    {
        public override string Texture => "AAModClassic/NPCs/Bosses/Shen/ShenlingTail";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Discordian Serpent");
            Main.npcFrameCount[NPC.type] = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.dontCountMe = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {

                NPC.position.X = NPC.position.X + NPC.width / 2;
                NPC.position.Y = NPC.position.Y + NPC.height / 2;
                NPC.width = 44;
                NPC.height = 78;
                NPC.position.X = NPC.position.X - NPC.width / 2;
                NPC.position.Y = NPC.position.Y - NPC.height / 2;
                int dust1 = ModContent.DustType<Dusts.DiscordLight>();
                int dust2 = ModContent.DustType<Dusts.DiscordLight>();
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust2, 0f, 0f, 0);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }

        public override bool PreAI()
        {
            if (NPC.ai[3] > 0)
                NPC.realLife = (int)NPC.ai[3];
            if (NPC.target < 0 || NPC.target == byte.MaxValue || Main.player[NPC.target].dead)
                NPC.TargetClosest(true);
            if (Main.player[NPC.target].dead && NPC.timeLeft > 300)
                NPC.timeLeft = 300;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!Main.npc[(int)NPC.ai[1]].active)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
            }

            if (Main.npc[(int)NPC.ai[1]].alpha < 128)
            {
                if (NPC.alpha != 0)
                {
                    for (int num934 = 0; num934 < 2; num934++)
                    {
                        int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.DiscordLight>(), 0f, 0f, 100, default, 2f);
                        Main.dust[num935].noGravity = false;
                        Main.dust[num935].noLight = false;
                    }
                }
                NPC.alpha -= 42;
                if (NPC.alpha < 0)
                {
                    NPC.alpha = 0;
                }
            }


            if (NPC.ai[1] < (double)Main.npc.Length)
            {
                Vector2 npcCenter = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float dirX = Main.npc[(int)NPC.ai[1]].position.X + Main.npc[(int)NPC.ai[1]].width / 2 - npcCenter.X;
                float dirY = Main.npc[(int)NPC.ai[1]].position.Y + Main.npc[(int)NPC.ai[1]].height / 2 - npcCenter.Y;
                NPC.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - NPC.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                if (dirX < 0f)
                {
                    NPC.spriteDirection = 1;

                }
                else
                {
                    NPC.spriteDirection = -1;
                }
                NPC.position.X = NPC.position.X + posX;
                NPC.position.Y = NPC.position.Y + posY;
            }
            return false;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[NPC.target];
            if (player.vortexStealthActive && projectile.CountsAsClass(DamageClass.Ranged))
            {
                modifiers.TargetDamageMultiplier /= 2f;
                modifiers.DisableCrit();
            }
            if (projectile.penetrate == -1 && !projectile.minion)
            {
                modifiers.TargetDamageMultiplier *= 0.2f;
            }
            else if (projectile.penetrate > 1) //TODO: was >= 1 but that seemed too strict
            {
                modifiers.TargetDamageMultiplier *= 0.2f;
            }
        }
    }
}