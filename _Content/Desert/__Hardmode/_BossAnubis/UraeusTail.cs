using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;

namespace AAModClassic._Content.Desert.__Hardmode._BossAnubis
{
    public class UraeusTail : Uraeus
    {
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Uraeus");
            this.HideFromBestiary();
        }

		public override void SetDefaults()
		{
            base.SetDefaults();
            NPC.dontCountMe = true;
		}

		public override bool PreKill()
		{
			return false;
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, drawColor, true);
            return false;
        }
    }
}