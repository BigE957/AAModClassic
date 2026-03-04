using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework.Graphics;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic.NPCs.Enemies.Mire
{ 
    public class HarukaShadow : ModNPC
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
            NPC.damage = 0;
            NPC.value = 0;
            NPC.alpha = 50;
            NPC.width = 38;
            NPC.height = 58;
            NPC.rarity = 1;
        }

        public override void AI()
        {
            if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
            {
                NPC.ai[0] = 1;
            }
            if (NPC.ai[0] == 1)
            {
                NPC.dontTakeDamage = true;
                if (NPC.ai[1] < 255)
                {
                    NPC.alpha += 4;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.ai[1] += 4;
                    }
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.active = false;
                        NPC.netUpdate = true;
                    }
                }
            }
        }

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            knockback = 0;
            damage = 0;
            crit = false;
            if (NPC.ai[0] != 1)
            {
                NPC.ai[0] = 1;
                CombatText.NewText(NPC.Hitbox, new Color(72, 78, 117), "pathetic.");
            }
            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.ai[0] == 0)
            {
                NPC.frame.Y = frameHeight;
            }
            else
            {
                NPC.frame.Y = frameHeight * 2;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = TextureAssets.Npc[NPC.type].Value;
            Texture2D tex2 = Mod.GetTexture("NPCs/Bosses/Hydra/HarukaShade_Glow");
            BaseDrawing.DrawTexture(spriteBatch, tex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 3, NPC.frame, NPC.GetAlpha(drawColor));
            if (NPC.ai[0] == 0)
            {
                Lighting.AddLight(NPC.Center, Color.MediumVioletRed.R / 180, Color.MediumVioletRed.G / 180, Color.MediumVioletRed.B / 180);
                BaseDrawing.DrawTexture(spriteBatch, tex2, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 3, NPC.frame, Color.White);
            }
            return false;
        }
    }
}