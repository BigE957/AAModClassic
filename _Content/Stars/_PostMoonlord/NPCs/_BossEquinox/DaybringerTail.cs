using AAModClassic.Utilities;
using Microsoft.Xna.Framework;

namespace AAModClassic._Content.Stars._PostMoonlord.NPCs._BossEquinox
{
    public class DaybringerTail : DaybringerHead
	{
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            this.HideFromBestiary();
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            NPC.dontCountMe = true;
            NPC.npcSlots = 0;
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