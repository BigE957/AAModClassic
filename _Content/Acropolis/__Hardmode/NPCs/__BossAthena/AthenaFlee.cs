using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.BossStandard;
using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Weapons;
using AAModClassic._Content.Acropolis.__Hardmode.Items.Materials;
using AAModClassic._Content.Acropolis._PostMoonlord.NPCs.__BossAthenaA;
using AAModClassic._Content.Acropolis.Projectiles;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using AAModClassic.Effects;
using AAModClassic.UI.Titles;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis.__Hardmode.NPCs.__BossAthena
{
    public class AthenaFlee : ModNPC
    {
        public override string Texture => ModContent.GetInstance<Athena>().Texture;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Athena");
            Main.npcFrameCount[NPC.type] = 7;
        }
        public override void SetDefaults()
        {
            NPC.width = 152;
            NPC.height = 114;
            NPC.npcSlots = 1000;
            NPC.aiStyle = -1;
            NPC.defense = 1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noTileCollide = true;
            NPC.damage = 0;
            NPC.value = 0;
        }

        public override void AI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[0]++ >= 120)
            {
                if (NPC.ai[0] >= 120 && NPC.ai[0] < 130)
                {
                    NPC.velocity.Y += 1f;
                    NPC.netUpdate = true;
                }
                else if (NPC.ai[0] == 130)
                {
                    NPC.netUpdate = true;
                }
                else if (NPC.ai[0] >= 130)
                {
                    NPC.velocity.Y -= 0.5f;
                    if (NPC.velocity.Y < -8f) NPC.velocity.Y = -8f;
                }
                if (NPC.position.Y + NPC.velocity.Y <= 0f && Main.netMode != NetmodeID.MultiplayerClient) { BaseAI.KillNPC(NPC); NPC.netUpdate = true; }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 6)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0;
            }
            if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
            {
                NPC.frame.Y = 0;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.oldPos, NPC.scale, NPC.rotation, NPC.direction, 7, NPC.frame, 1f, 1f, 5, false, 0f, 0f);
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 7, NPC.frame, NPC.GetAlpha(drawColor), false);
            return false;
        }
    }
}