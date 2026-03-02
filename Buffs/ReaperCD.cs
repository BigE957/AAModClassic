using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class ReaperCD : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Reaper Scythe Immunity Cooldown");
			// Description.SetDefault("You cannot use dashing ability of the weapon now");
			Main.debuff[Type] = true;
			canBeCleared/* tModPorter Note: Removed. Use BuffID.Sets.NurseCannotRemoveDebuff instead, and invert the logic */ = false;
        }
	}
}
