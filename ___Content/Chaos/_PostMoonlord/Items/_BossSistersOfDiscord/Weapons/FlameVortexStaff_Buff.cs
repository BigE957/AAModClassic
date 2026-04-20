using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons
{
    public class FlameVortexStaff_Buff : ModBuff
	{
        public override void SetStaticDefaults()
        {
			// DisplayName.SetDefault("Orbiters");
            // Description.SetDefault("Flames orbit you, empowering you");
            Main.buffNoTimeDisplay[Type] = true;		
        }

        public override void Update(Player player, ref int buffIndex)
        {
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<FlameVortexStaff_FireOrbiter>()] > 0)
			{
				modPlayer.Orbiters = true;
			}
			if (!modPlayer.Orbiters)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				player.buffTime[buffIndex] = 2;
			}
        }
    }
}