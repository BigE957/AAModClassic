using Terraria.ModLoader;

namespace AAModClassic.Base.BaseMod.Base
{
    public class BaseNet
    {
        //------------------------------------------------------//
        //--------------------BASE NET CLASS--------------------//
        //------------------------------------------------------//
        // Contains methods relating to netmessages.            //
        //------------------------------------------------------//
        //  Author(s): Grox the Great                           //
        //------------------------------------------------------//

        public static ModPacket WriteToPacket(ModPacket packet, byte msg, params object[] param)
        {
            packet.Write((byte)msg);
            for (int m = 0; m < param.Length; m++)
            {
                object obj = param[m];

				if(obj is byte[])
				{
					byte[] array = (byte[])obj;
					foreach(byte b in array) packet.Write((byte)b); 
				}else
                if (obj is bool) packet.Write((bool)obj); else
                if (obj is byte) packet.Write((byte)obj); else
                if (obj is short) packet.Write((short)obj); else
                if (obj is int) packet.Write((int)obj); else
                if (obj is float) packet.Write((float)obj);
            }
            return packet;
        }		

        /*
         * Used to sync custom ai float arrays. (the npc or projectile requires a method called 'public void SetAI(float[] ai, int type)' that sets the ai for this to work)
         */
        public static void SyncAI(int entType, int id, float[] ai, int aitype)
        {
            object[] ai2 = new object[ai.Length + 4];
            ai2[0] = (byte)entType;
            ai2[1] = (short)id;
            ai2[2] = (byte)aitype;
            ai2[3] = (byte)ai.Length;
            for(int m = 4; m < ai2.Length; m++){ ai2[m] = ai[m - 4]; }
            MNet.SendBaseNetMessage(1, ai2);
        }
    }
}