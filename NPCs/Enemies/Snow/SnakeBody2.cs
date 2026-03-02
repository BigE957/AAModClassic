using Microsoft.Xna.Framework;

namespace AAModClassic.NPCs.Enemies.Snow
{
    public class SnakeBody2 : SnakeHead
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