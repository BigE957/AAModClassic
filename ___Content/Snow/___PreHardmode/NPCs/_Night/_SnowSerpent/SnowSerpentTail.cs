using Microsoft.Xna.Framework;

namespace AAModClassic.___Content.Snow.___PreHardmode.NPCs._Night._SnowSerpent
{
    public class SnowSerpentTail : SnowSerpentHead
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Snow Serpent");
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