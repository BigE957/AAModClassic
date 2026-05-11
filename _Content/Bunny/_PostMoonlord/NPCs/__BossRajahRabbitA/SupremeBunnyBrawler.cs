using AAModClassic._Content.Bunny.__Hardmode.NPCs.__BossRajahRabbit;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.NPCs.__BossRajahRabbitA
{
    public class SupremeBunnyBrawler : BunnyBrawler
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            this.HideFromBestiary();
        }
        public override string Texture => ModContent.GetInstance<BunnyBrawler>().Texture;
        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.damage = 170;
            NPC.defense = 100;
            NPC.lifeMax = 1600;
        }
        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            modifiers.TargetDamageMultiplier /= 2;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<RajahRabbitA>()))
            {
                BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, 1f, 1f, 10, true, 0f, 0f, AAColor.Rainbow3);
            }
            return false;
        }
    }
}