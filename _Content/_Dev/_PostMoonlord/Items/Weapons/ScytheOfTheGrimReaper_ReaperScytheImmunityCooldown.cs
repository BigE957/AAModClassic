using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev._PostMoonlord.Items.Weapons
{
    public class ScytheOfTheGrimReaper_ReaperScytheImmunityCooldown : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Reaper Scythe Immunity Cooldown");
			// Description.SetDefault("You cannot use dashing ability of the weapon now");
			Main.debuff[Type] = true;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
	}
}
