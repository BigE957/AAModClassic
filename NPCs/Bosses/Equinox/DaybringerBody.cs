using Microsoft.Xna.Framework;

namespace AAMod.NPCs.Bosses.Equinox
{
    public class DaybringerBody : DaybringerHead
	{
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