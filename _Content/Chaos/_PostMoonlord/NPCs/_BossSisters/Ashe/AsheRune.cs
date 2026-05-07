using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Shaders;
using System.IO;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs._BossSisters.Ashe
{
    public class AsheRune : ModNPC
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

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(count);
                writer.Write(Control);
                writer.Write(spinLeft);
                writer.Write(SpinCheck);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                count = reader.ReadInt32();
                Control = reader.ReadInt32();
                spinLeft = reader.ReadBoolean();
                SpinCheck = reader.ReadBoolean();
            }
        }

        public int count = 0;
        public int Control = 0;

        public bool spinLeft = false;
        public bool SpinCheck = false;

        public Vector2 Runeshootspeed = new Vector2();

        public override void AI()
        {
            if (!SpinCheck && Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (Main.rand.NextBool(2))
                {
                    spinLeft = true;
                }
                SpinCheck = true;
                NPC.netUpdate = true;
            }
            if (Control == 1)
            {
                NPC.rotation += spinLeft ? .02f : -.02f;
                if (count == 0)
                {
                    if(Main.player[Main.npc[(int)NPC.ai[3]].target].position - new Vector2(NPC.ai[0], NPC.ai[1]) == new Vector2(0f, 0f))
                    {
                        Runeshootspeed = new Vector2(0, 0);
                    }
                    else
                    {
                        Runeshootspeed = 10f * Vector2.Normalize(Main.player[Main.npc[(int)NPC.ai[3]].target].position - new Vector2(NPC.ai[0], NPC.ai[1]));
                    }
                    if(Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int SootProj = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X + Runeshootspeed.X, NPC.Center.Y + Runeshootspeed.Y, Runeshootspeed.X, Runeshootspeed.Y, ModContent.ProjectileType<AsheShot>(), (int)NPC.ai[2]/2, 0, Main.myPlayer, NPC.whoAmI, 0);
                        Main.projectile[SootProj].alpha = 0;
                    }
                    NPC.netUpdate = true;
                }
                
                if(count >= 60)
                {
                    Control = 2;
                    NPC.netUpdate = true;
                }
                count ++;
            }
            else if (Control == 2)
            {
                NPC.rotation -= spinLeft ? .02f : -.02f;
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
                    NPC.scale -= .04f;
                }
            }
            else
            {
                NPC.rotation += spinLeft ? .04f : -.04f;
                if (NPC.alpha > 0)
                {
                    NPC.alpha -= 5;
                }
                else
                {
                    NPC.alpha = 0;
                    Control = 1;
                    NPC.netUpdate = true;
                }
                if (NPC.scale < 1)
                {
                    NPC.scale += .04f;
                }
            }
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawAura(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, auraPercent, 1.4f, NPC.scale, NPC.rotation, NPC.direction, 1, default, 0, 0, ColorUtils.COLOR_GLOWPULSE);
            int red = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, red, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 1, NPC.frame, NPC.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            return false;
        }
    }
}