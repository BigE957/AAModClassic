using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Effects;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma
{
    public class AkumaTransition : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul Of Fury");
            Main.npcFrameCount[NPC.type] = 8;
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
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
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public int RVal = 255;
        public int BVal = 0;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            //NPC.frame.Y += 
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, NPC.GetAlpha(new Color(RVal, 125, BVal)), NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, 0, 0);
            return false;
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
                    NPC.netOffset = Vector2.Zero;
                }

                if (Main.netMode != NetmodeID.Server) //clientside stuff
                {
                    NPC.frameCounter++;
                    if (NPC.frameCounter >= 7)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y += 42;
                    }
                    if (NPC.frame.Y > 42 * 7)
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
                        Music = MusicManagementSystem.MusicSlots["Akuma_Awakened"];
                        NPC.boss = true;
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

                NPC.ai[0]++;
                if (NPC.ai[0] == 180)
                {
                    NPC.netUpdate = true;
                }
                else if (NPC.ai[0] >= 600 && !NPC.AnyNPCs(ModContent.NPCType<AkumaAHead>()))
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<AkumaAHead>(), false, NPC.Center, "", false);
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.AkumaTransition4"), Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);

                        int b = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ShockwaveBoom>(), 0, 1, Main.myPlayer, 0, 0);
                        Main.projectile[b].Center = NPC.Center;
                    }
                    NPC.netUpdate = true;
                    NPC.active = false;
                }
                return false;
            }
            return true;
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
                NPC.netOffset = Vector2.Zero;
            }
			
			if(Main.netMode != NetmodeID.Server) //clientside stuff
			{
				NPC.frameCounter++;
				if (NPC.frameCounter >= 5)
				{
					NPC.frameCounter = 0;
					NPC.frame.Y += 42;
				}
				if (NPC.frame.Y > 42 * 7)
				{
					NPC.frame.Y = 0;
				}
				if (NPC.ai[0] > 300)
				{
					NPC.alpha -= 5;
					if (NPC.alpha < 0)
					{
						NPC.alpha = 0;
					}
				}
				if (NPC.ai[0] >= 300) //after he says 'heh' on the server, change music on the client
				{
					Music = MusicManagementSystem.MusicSlots["Akuma_Awakened"];
                    NPC.boss = true;
                }				
				if (NPC.ai[0] >= 660) //after 660 on the server, transition color
				{
					RVal -= 5;
					BVal += 5;
					if (RVal <= 0)
					{
						RVal = 0;
					}
					if (BVal >= 255)
					{
						BVal = 255;
					}
				}
			}

			NPC.ai[0]++;	
			if(NPC.ai[0] == 300)
			{
				NPC.netUpdate = true;
			}
            else if (NPC.ai[0] == 300)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Transition.1"), new Color(180, 41, 32));
				NPC.netUpdate = true;
			}
            else if (NPC.ai[0] == 525)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Transition.2"), new Color(180, 41, 32));
			}
            else if(NPC.ai[0] == 750) //sync so the color transition occurs
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Transition.6"), new Color(175, 75, 255));
                NPC.netUpdate = true;
			}
            else if (NPC.ai[0] == 976)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Transition.3"), Color.DeepSkyBlue);
			}
            else if (NPC.ai[0] >= 1200 && !NPC.AnyNPCs(ModContent.NPCType<AkumaAHead>()))
			{
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<AkumaAHead>(), false, NPC.Center, "", false);
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Transition.4"), Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Transition.5"), Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);

                    int b = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ShockwaveBoom>(), 0, 1, Main.myPlayer, 0, 0);
                    Main.projectile[b].Center = NPC.Center;
                }
                NPC.netUpdate = true;
				NPC.active = false;
			}
        }

        public void MoveToPoint(Vector2 point)
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
            if (!NPC.AnyNPCs(ModContent.NPCType<AkumaAHead>()))
            {
                return false;
            }
            return true;
        }

    }
}