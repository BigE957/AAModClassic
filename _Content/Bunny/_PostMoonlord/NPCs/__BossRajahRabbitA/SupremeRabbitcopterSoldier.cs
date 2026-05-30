using AAModClassic._Content.Bunny.__Hardmode.NPCs.__BossRajahRabbit;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.NPCs.__BossRajahRabbitA
{
    public class SupremeRabbitcopterSoldier : RabbitcopterSoldier
    {
        public override string Texture => ModContent.GetInstance<RabbitcopterSoldier>().Texture;
        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.damage = 170;
            NPC.defense = 70;
            NPC.lifeMax = 900;
        }
        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            modifiers.TargetDamageMultiplier /= 2;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<RajahRabbitA>()))
            {
                BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, 1f, 1f, 10, false, 0f, 0f, AAColor.Rainbow3);
            }
            return true;
        }
    }
}