using System;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu
{
    public class UDUNFUKED : ModNPC
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lovecraftian Shadow");
        }

        public override void SetDefaults()
        {
            NPC.npcSlots = 100;
            NPC.width = 130;
            NPC.height = 130;
            NPC.aiStyle = -1;
            NPC.damage = 999999999;
            NPC.dontTakeDamage = true;
            NPC.lifeMax = 1000000;
            NPC.DeathSound = SoundID.Item88;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.netAlways = true;
            NPC.noTileCollide = true;
            for (int m = 0; m < NPC.buffImmune.Length; m++) NPC.buffImmune[m] = true;
        }

        public float Rotation = 0;
        public float RiftSpin = 0;
        public bool Line = false;


        public override void AI()
        {
            Player player = Main.player[NPC.target];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();

            if (Main.player[NPC.target].dead || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000.0 || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000.0)
            {
                NPC.TargetClosest(true);
                NPC.active = false;
            }
            if (player.dead || !player.active || !modPlayer.ZoneShip)
            {
                if (Line == false && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Line = true;
                    Main.NewText("Do not return...", Color.DarkCyan);
                }
                
            }
            if (Line == true)
            {
                NPC.velocity.X *= 0.8f;
                NPC.velocity.Y *= 0.8f;
                NPC.alpha += 10;
                if (NPC.alpha >= 255)
                {
                    NPC.active = false;
                }
                return;
            }
            NPC.rotation += NPC.direction * 0.7f;
            Vector2 vector44 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
            float num441 = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - vector44.X;
            float num442 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2 - vector44.Y;
            float num443 = (float)Math.Sqrt((double)(num441 * num441 + num442 * num442));
            float num4 = 9f + num443 / 100f;
            if (num4 < 8.0)
                num4 = 8f;
            if (num4 > 32.0)
                num4 = 32f;
            float num5 = num4 / num443;
            NPC.velocity.X = num441 * num5;
            NPC.velocity.Y = num442 * num5;
            Rotation += NPC.velocity.X * .08f;
            RiftSpin -= NPC.velocity.X * .08f;
            return;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture2D13 = TextureAssets.Npc[NPC.type].Value;
            Texture2D WheelTex = Mod.GetTexture("_Unreleased/NPCs/Bosses/SoC/UDUNFUKED_Wheel");;
            Texture2D Rift = Mod.GetTexture("_Unreleased/NPCs/Bosses/SoC/Rift");
            Vector2 vector38 = NPC.position + new Vector2(NPC.width, NPC.height) / 2f + Vector2.UnitY * NPC.gfxOffY - Main.screenPosition;
            int num214 = TextureAssets.Npc[NPC.type].Value.Height;
            int y6 = 0;
            Vector2 drawCenter = new Vector2(NPC.Center.X, NPC.Center.Y);

            Main.spriteBatch.Draw(Rift, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, Rift.Width, Rift.Height)), AAColor.Cthulhu, RiftSpin, new Vector2(Rift.Width / 2f, Rift.Height / 2f), 1.5f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(WheelTex, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, WheelTex.Width, WheelTex.Height)), drawColor, Rotation, new Vector2(texture2D13.Width / 2f, texture2D13.Height / 2f), NPC.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture2D13, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, texture2D13.Width, texture2D13.Height)), drawColor, NPC.rotation, new Vector2(texture2D13.Width / 2f, texture2D13.Height / 2f), NPC.scale, SpriteEffects.None, 0f);

            return false;
        }

        private void RainStart()
        {
            if (!Main.raining)
            {
                int num = 86400;
                int num2 = num / 24;
                Main.rainTime = Main.rand.Next(num2 * 8, num);
                if (Main.rand.Next(3) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2);
                }
                if (Main.rand.Next(4) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 2);
                }
                if (Main.rand.Next(5) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 2);
                }
                if (Main.rand.Next(6) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 3);
                }
                if (Main.rand.Next(7) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 4);
                }
                if (Main.rand.Next(8) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 5);
                }
                float num3 = 1f;
                if (Main.rand.Next(2) == 0)
                {
                    num3 += 0.05f;
                }
                if (Main.rand.Next(3) == 0)
                {
                    num3 += 0.1f;
                }
                if (Main.rand.Next(4) == 0)
                {
                    num3 += 0.15f;
                }
                if (Main.rand.Next(5) == 0)
                {
                    num3 += 0.2f;
                }
                Main.rainTime = (int)((float)Main.rainTime * num3);
                Main.raining = true;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.WorldData, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                }
            }
        }
    }
}