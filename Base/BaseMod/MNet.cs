using AAModClassic.Base.BaseMod.Base;
using Terraria;
using Terraria.ID;

namespace AAModClassic.Base.BaseMod
{
	public class MNet
	{
		public static void SendBaseNetMessage(int msg, params object[] param)
		{
			if (Main.netMode == NetmodeID.SinglePlayer) { return; } //nothing to sync in SP
            BaseNet.WriteToPacket(AAMod.instance.GetPacket(), (byte)msg, param).Send();
		}
	}
}