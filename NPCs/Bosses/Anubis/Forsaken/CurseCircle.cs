using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Anubis.Forsaken
{
    public class CurseCircle : ModNPC
    {
        public override void SetStaticDefaults()
        {
            this.HideFromBestiary();
        }
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
                NPC.rotation += .1f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.ai[0]++;
                    if (NPC.ai[0] >= 120)
                    {
                        NPC.ai[0] = 0;
                        NPC.ai[1] = 1;

                        int Type = Main.rand.Next(3);

                        switch (Type)
                        {
                            case 0:
                                Type = ModContent.NPCType<CursedLocust>();
                                break;
                            case 1:
                                Type = ModContent.NPCType<CursedScarab>();
                                break;
                            case 2:
                                Type = ModContent.NPCType<Naddaha>();
                                break;
                        }

                        int m = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, Type);
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
                NPC.rotation -= .1f;
            }
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawAura(Main.spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, auraPercent, 1.4f, NPC.scale, NPC.rotation, NPC.direction, 1, default, 0, 0, ColorUtils.COLOR_GLOWPULSE);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 1, NPC.frame, NPC.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            return false;
        }
    }
}