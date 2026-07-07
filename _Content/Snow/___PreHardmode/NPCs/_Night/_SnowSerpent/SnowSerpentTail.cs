using AAModClassic.Utilities;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.___PreHardmode.NPCs._Night._SnowSerpent
{
    public class SnowSerpentTail : SnowSerpentHead, IBannerNPC
    {
        public int OverrideBannerNPCType => ModContent.NPCType<SnowSerpentHead>();

        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Snow Serpent");
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
	}
}