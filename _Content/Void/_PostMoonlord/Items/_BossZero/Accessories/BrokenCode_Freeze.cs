using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Accessories
{
    public class BrokenCode_Freeze : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Freeze");
			// Description.SetDefault("Task Manager isn't gonna help with this");
			Main.persistentBuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }


        public override void Update(Player player, ref int index)
        {
            player.immuneNoBlink = true;
        }
    }
}
