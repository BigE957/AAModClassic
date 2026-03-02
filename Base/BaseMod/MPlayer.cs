using Terraria.ModLoader;

namespace AAModClassic.Base.BaseMod
{
    public class MPlayer : ModPlayer
    {
		public static bool useItem = false;

		public override void SetControls() 
		{
			if(useItem)
			{
				useItem = false;
				Player.delayUseItem = false;
				Player.controlUseItem = true;
			}
		}
    }
}