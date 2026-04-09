using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework.Graphics;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;

namespace AAModClassic.___Content.Mire._PreHardmode.NPCs._BossHydra
{ 
    public class HarukaShade : ModNPC
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("...");
            Main.npcFrameCount[NPC.type] = 3;
        }

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.defense = 1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = false;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.damage = 0;
            NPC.value = 0;
            NPC.alpha = 255;
            Music = MusicManagementSystem.MusicSlots["Sisters_Intro"];
            NPC.width = 38;
            NPC.height = 58;
        }

        public override void AI()
        {
            NPC.dontTakeDamage = true;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.velocity.Y == 0)
                {
                    NPC.ai[1]++;
                    if (NPC.ai[1] >= 120 && NPC.ai[1] <= 240)
                    {
                        if (NPC.alpha > 50)
                        {
                            NPC.alpha -= 4;
                        }
                        else
                        {
                            NPC.alpha = 50;
                        }
                    }
                    if (NPC.ai[1] > 240)
                    {
                        if (NPC.alpha < 255)
                        {
                            NPC.alpha += 4;
                        }
                        else
                        {
                            NPC.active = false;
                        }
                    }
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.ai[1] >= 120 && NPC.ai[1] <= 240)
            {
                NPC.frame.Y = frameHeight;
            }
            else if (NPC.ai[1] > 240)
            {
                NPC.frame.Y = frameHeight * 2;
            }
            else
            {
                NPC.frame.Y = 0;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = TextureAssets.Npc[NPC.type].Value;
            Texture2D tex2 = Mod.GetTexture("NPCs/Bosses/Hydra/HarukaShade_Glow");
            BaseDrawing.DrawTexture(spriteBatch, tex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 3, NPC.frame, NPC.GetAlpha(drawColor));
            if (NPC.ai[1] >= 60 && NPC.ai[1] < 240)
            {
                Lighting.AddLight(NPC.Center, Color.MediumVioletRed.R / 180, Color.MediumVioletRed.G / 180, Color.MediumVioletRed.B / 180);
                BaseDrawing.DrawTexture(spriteBatch, tex2, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 3, NPC.frame, Color.White);
            }
            return false;
        }
    }
}