using AAModClassic._Unreleased.Content.Parthenan.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Parthenan.__Hardmode.NPCs.__BossTechnoTruffle
{
    public class Truffling : ModNPC
    {
        public static Asset<Texture2D> Glowmask1;
        public static Asset<Texture2D> Glowmask2;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Truffling");
            Main.npcFrameCount[NPC.type] = 4;

            Glowmask1 = ModContent.Request<Texture2D>(Texture + "_Glow1");
            Glowmask2 = ModContent.Request<Texture2D>(Texture + "_Glow2");
        }

        public override void SetDefaults()
        {
            NPC.width = 14;
            NPC.height = 14;
            NPC.value = 0;
            NPC.npcSlots = 0;
            NPC.aiStyle = -1;
            NPC.lifeMax = 300;
            NPC.defense = 0;
            NPC.damage = 20;
            //TODO
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            SpawnModBiomes = [ModContent.GetInstance<ParthenanBiome>().Type];
        }

        public override void AI()
        {
            Color color = BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, BaseDrawing.GetLightColor(NPC.position), BaseDrawing.GetLightColor(NPC.position), Color.Violet, BaseDrawing.GetLightColor(NPC.position), Color.Violet, BaseDrawing.GetLightColor(NPC.position));

            Lighting.AddLight((int)(NPC.Center.X + NPC.width / 2) / 16, (int)(NPC.position.Y + NPC.height / 2) / 16, color.R / 255, color.G / 255, color.B / 255);

            BaseAI.AIEye(NPC, ref NPC.ai, true, true, .2f, .2f, 4, 2, 1, 1);
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.frameCounter++ > 8)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y > frameHeight * 3)
                {
                    NPC.frame.Y = 0;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glowTex = Glowmask1.Value;
            Texture2D glowTex1 = Glowmask2.Value;
            Color color = BaseUtility.MultiLerpColor(((int)(Main.GlobalTimeWrappedHourly * 60)) % 100 / 100f, drawColor, drawColor, Color.Violet, drawColor, Color.Violet, drawColor);

            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.Draw(glowTex, NPC.Center - screenPos, NPC.frame, color, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.Draw(glowTex1, NPC.Center - screenPos, NPC.frame, Color.White, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            return false;
        }
    }
}


