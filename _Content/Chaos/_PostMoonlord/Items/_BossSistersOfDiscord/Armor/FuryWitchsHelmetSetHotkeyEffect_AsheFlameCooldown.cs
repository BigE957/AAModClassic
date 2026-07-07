using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Armor
{
    public class FuryWitchsHelmetSetHotkeyEffect_AsheFlameCooldown : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ashe Flame Cooldown");
			// Description.SetDefault("You cannot wield the flames for a period of time");
			Main.buffNoSave[Type] = true;
		}
	}
}