using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Effects;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata
{
    public class YamataTransition : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spirit of Wrath");
            Main.npcFrameCount[NPC.type] = 4;
            this.HideFromBestiary();
        }
        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.friendly = false;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.timeLeft = 10;
            NPC.alpha = 255;
            NPC.scale = .1f;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public int RVal = 125;
        public int BVal = 255;


        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 24, NPC.frame, NPC.GetAlpha(drawColor), true);
            BaseDrawing.DrawTexture(spriteBatch, Mod.GetTexture("Glowmasks/YamataTransition"), 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 24, NPC.frame, NPC.GetAlpha(new Color(RVal, 0, BVal)), true);
            return false;
        }

        public override void AI()
        {
			NPC.TargetClosest();			
            Player player = Main.player[NPC.target];
            MoveToPoint(player.Center - new Vector2(0, 300f));

            if (Vector2.Distance(NPC.Center, player.Center) > 2000)
            {
                NPC.alpha = 255;
                NPC.Center = player.Center - new Vector2(0, 300f);
            }
			
			if(Main.netMode != NetmodeID.Server) //clientside stuff
			{
				NPC.frameCounter++;
				if (NPC.frameCounter >= 7)
				{
					NPC.frameCounter = 0;
					NPC.frame.Y += TextureAssets.Npc[NPC.type].Value.Height / 4 ;
				}

				if (NPC.frame.Y > TextureAssets.Npc[NPC.type].Value.Height / 4 * 3)
				{
					NPC.frame.Y = 0 ;
				}
				if (NPC.ai[0] > 375)
				{
					if (NPC.alpha < 0)
					{
						NPC.alpha = 0;
					}
                    else
                    {
                        NPC.alpha -= 5;
                    }
                    if (NPC.scale < 1)
                    {
                        NPC.scale += .02f;
                    }
                    else
                    {
                        NPC.scale = 1;
                    }
				}
				if (NPC.ai[0] >= 375) //after he says 'nyeh' on the server, change music on the client
				{
					Music = MusicManagementSystem.MusicSlots["Yamata_Awakened"];
                    NPC.boss = true;
				}
				if (NPC.ai[0] >= 900) //after he says 'as if' on the server, transition color
				{
					RVal += 5;
					BVal -= 5;
					if (RVal <= 90)
					{
						BVal = 90;
					}
					if (RVal >= 255)
					{
						RVal = 255;
					}
				}
			}
			if(Main.netMode != NetmodeID.MultiplayerClient)
			{
				NPC.ai[0]++;

				if (NPC.ai[0] == 375)    
				{
					if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Transition.1"), new Color(45, 46, 70));
					NPC.netUpdate = true;
				}else
				if (NPC.ai[0] == 650)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Transition.2"), new Color(45, 46, 70));
				}else
				if (NPC.ai[0] == 900)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Transition.3"), new Color(45, 46, 70));
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Transition.PlayerStatus"), Color.PaleVioletRed);
                    NPC.netUpdate = true;
				}else
				if (NPC.ai[0] == 1100)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Transition.4"), new Color(146, 30, 68));
				}else
				if (NPC.ai[0] >= 1455 && !NPC.AnyNPCs(ModContent.NPCType<YamataABody>()))
				{
					AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<YamataABody>(), false, NPC.Center, "", false);
					if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Transition.AwakenStatus"), Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
					if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Transition.5"), new Color(146, 30, 68));
                    int b = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ShockwaveBoom>(), 0, 1, Main.myPlayer, 0, 0);
                    Main.projectile[b].Center = NPC.Center;

                    SoundEngine.PlaySound(Mod.GetLegacySoundSlot(SoundType.Sound, "Sounds/YamataRoar"), NPC.position);
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
                        int num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.YamataADust>(), 0f, 0f, 200, default, 3.7f);
                        Main.dust[num88].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                        Main.dust[num88].noGravity = true;
                        Main.dust[num88].noLight = true;
                        Main.dust[num88].velocity *= 3f;
                        Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + Main.rand.NextFloat() * 4f);
                        num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.YamataADust>(), 0f, 0f, 100, default, 1.5f);
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
                        int num90 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.YamataADust>(), 0f, 0f, 0, default, 2.7f);
                        Main.dust[num90].position = NPC.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f;
                        Main.dust[num90].noGravity = true;
                        Main.dust[num90].noLight = true;
                        Main.dust[num90].velocity *= 3f;
                        Main.dust[num90].velocity += NPC.DirectionTo(Main.dust[num90].position) * 2f;
                    }
                    for (int num91 = 0; num91 < 30; num91++)
                    {
                        int num92 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.YamataADust>(), 0f, 0f, 0, default, 1.5f);
                        Main.dust[num92].position = NPC.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f;
                        Main.dust[num92].noGravity = true;
                        Main.dust[num92].velocity *= 3f;
                        Main.dust[num92].velocity += NPC.DirectionTo(Main.dust[num92].position) * 3f;
                    }

                    NPC.netUpdate = true;
					NPC.active = false;				
				}
			}
        }

        public override bool PreAI()
        {
            if (AAConfigClient.Instance.NoBossDialogue)
            {
                NPC.TargetClosest();
                Player player = Main.player[NPC.target];
                MoveToPoint(player.Center - new Vector2(0, 300f));

                if (Vector2.Distance(NPC.Center, player.Center) > 2000)
                {
                    NPC.alpha = 255;
                    NPC.Center = player.Center - new Vector2(0, 300f);
                }

                if (Main.netMode != NetmodeID.Server) //clientside stuff
                {
                    NPC.frameCounter++;
                    if (NPC.frameCounter >= 7)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y += 52;
                    }
                    if (NPC.frame.Y > 52 * 5)
                    {
                        NPC.frame.Y = 0;
                    }
                    if (NPC.ai[0] > 180)
                    {
                        NPC.alpha -= 5;
                        if (NPC.alpha < 0)
                        {
                            NPC.alpha = 0;
                        }
                    }
                    if (NPC.ai[0] >= 180) //after he says 'heh' on the server, change music on the client
                    {
                        Music = MusicManagementSystem.MusicSlots["Yamata_Awakened"];
                    }
                    if (NPC.ai[0] >= 380)
                    {
                        RVal -= 5;
                        BVal += 5;
                        if (RVal <= 0)
                        {
                            RVal = 0;
                        }
                        if (BVal >= 380)
                        {
                            BVal = 255;
                        }
                    }
                }
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.ai[0]++;
                    if (NPC.ai[0] == 180)
                    {
                        NPC.netUpdate = true;
                    }
                    else
                    if (NPC.ai[0] >= 600 && !NPC.AnyNPCs(ModContent.NPCType<YamataABody>()))
                    {
                        AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<YamataABody>(), false, NPC.Center, "", false);
                        if (Main.netMode != NetmodeID.MultiplayerClient) 
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Transition.AwakenStatus"), Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);

                        int b = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ShockwaveBoom>(), 0, 1, Main.myPlayer, 0, 0);
                        Main.projectile[b].Center = NPC.Center;

                        NPC.netUpdate = true;
                        NPC.active = false;
                    }
                }
                return false;
            }
            return true;
        }

        public void MoveToPoint(Vector2 point, bool goUpFirst = false)
        {
            float moveSpeed = 14f;
            if (moveSpeed == 0f || NPC.Center == point) return; //don't move if you have no move speed
            float velMultiplier = 1f;
            Vector2 dist = point - NPC.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            NPC.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            NPC.velocity *= moveSpeed;
            NPC.velocity *= velMultiplier;
        }

        public override bool CheckActive()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<YamataABody>()))
            {
                return false;
            }
            return true;
        }
    }
}