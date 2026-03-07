using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;

namespace AAModClassic.NPCs.Bosses.Yamata.Awakened
{
    public class YamataSoul : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mire Soul");
            Main.npcFrameCount[NPC.type] = 6;
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.ShadowFlameApparition);
            AIType = NPCID.ShadowFlameApparition;
            AnimationType = NPCID.ShadowFlameApparition;
            NPC.npcSlots = 0;
            NPC.value = BaseUtility.CalcValue(0, 0, 0, 0);
            NPC.aiStyle = NPCAIStyleID.AncientVision;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.damage = 200;
            NPC.alpha = 255;

        }
        public override void AI()
        {
            Lighting.AddLight(NPC.Center, AAColor.YamataA.R / 255, AAColor.YamataA.G / 255, AAColor.YamataA.B / 255);
            AAAI.AIShadowflameGhost(NPC, ref NPC.ai, false, 660f, 0.3f, 15f, 0.2f, 8f, 5f, 10f, 0.4f, 0.4f, 0.95f, 5f);
            if (!NPC.AnyNPCs(ModContent.NPCType<YamataA>()))
            {
                NPC.life = 0;
            }
            if (NPC.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, Mod.Find<ModDust>("YamataAuraDust").Type, 0f, 0f, 100, default, 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            NPC.alpha -= 12;
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }
        }

        public static Color GetGlowAlpha()
        {
            return new Color(200, 0, 50) * (Main.mouseTextColor / 255f);
        }
        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glowTex = Mod.GetTexture("NPCs/Bosses/Yamata/Awakened/YamataSoul");
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, drawColor);
            BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, NPC, GetGlowAlpha());
            BaseDrawing.DrawAfterimage(spriteBatch, glowTex, 0, NPC, 0.8f, 1f, 4, false, 0f, 0f, Color.White);
            return false;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(Mod.Find<ModBuff>("HydraToxin").Type, 600);
        }
    }
}
