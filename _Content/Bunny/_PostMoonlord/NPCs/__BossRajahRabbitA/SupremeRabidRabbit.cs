using AAModClassic._Content.Bunny.__Hardmode.NPCs.__BossRajahRabbit;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.NPCs.__BossRajahRabbitA
{
    public class SupremeRabidRabbit : RabidRabbit
    {
        public override string Texture => ModContent.GetInstance<RabidRabbit>().Texture;
        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.damage = 150;
            NPC.defense = 70;
            NPC.lifeMax = 1200;
        }
        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            modifiers.TargetDamageMultiplier /= 2;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.IsABestiaryIconDummy || NPC.AnyNPCs(ModContent.NPCType<RajahRabbitA>()))
                DrawingUtils.DrawAfterimageWithVelocity(spriteBatch, TextureAssets.Npc[NPC.type].Value, (NPC.Center + Vector2.UnitY * 4) - screenPos, NPC.velocity, 10, NPC.frame, Main.DiscoColor, NPC.scale, [NPC.rotation], NPC.frame.Size() * 0.5f);
                //BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, 1f, 1f, 10, true, 0f, 0f, AAColor.Rainbow2);

            return true;
        }
    }
}