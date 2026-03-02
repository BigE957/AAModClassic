using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

using Microsoft.Xna.Framework.Graphics;

namespace AAMod.NPCs.Bosses.Anubis
{
    public class MinionCircle : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.alpha = 255;
            NPC.dontTakeDamage = true;
            NPC.lifeMax = 1;
            NPC.aiStyle = -1;
            NPC.damage = Main.expertMode ? 50 : 84;
            NPC.defense = Main.expertMode ? 1 : 1;
            NPC.knockBackResist = 0.2f;
            NPC.width = 82;
            NPC.height = 82;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.scale = .001f;
            NPC.friendly = false;
        }

        public override void AI()
        {
            if (NPC.ai[1] == 0)
            {
                if (NPC.alpha > 50)
                {
                    NPC.alpha -= 5;
                }
                if (NPC.scale < 1)
                {
                    NPC.scale += .02f;
                }
                NPC.rotation += .05f;
                if (Main.netMode != 1)
                {
                    NPC.ai[0]++;
                    if (NPC.ai[0] >= 150)
                    {
                        NPC.ai[0] = 0;
                        NPC.ai[1] = 1;

                        int Type = Main.rand.Next(2);

                        if (!NPC.AnyNPCs(ModContent.NPCType<Uraeus>()))
                        {
                            Type = Main.rand.Next(3);
                        }

                        switch (Type)
                        {
                            case 0:
                                Type = ModContent.NPCType<HorusHawk>();
                                break;
                            case 1:
                                Type = ModContent.NPCType<Scarab>();
                                break;
                            case 2:
                                Type = ModContent.NPCType<Uraeus>();
                                break;
                        }

                        int m = NPC.NewNPC((int)NPC.position.X, (int)NPC.position.Y, Type);
                        Main.npc[m].Center = NPC.Center;

                        NPC.netUpdate = true;
                    }
                }
            }
            else
            {
                if (NPC.alpha < 255)
                {
                    NPC.alpha += 5;
                }
                else
                {
                    NPC.active = false;
                }
                if (NPC.scale < 1)
                {
                    NPC.scale -= .02f;
                }
                NPC.rotation -= .05f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            BaseDrawing.DrawTexture(sb, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 1, NPC.frame, NPC.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            return false;
        }
    }
}