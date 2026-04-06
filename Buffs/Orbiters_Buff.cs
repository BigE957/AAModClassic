using AAModClassic;
using AAModClassic.Projectiles.AH;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class Orbiters_Buff : ModBuff
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
			if (player.ownedProjectileCounts[ModContent.ProjectileType<FireOrbiter>()] > 0)
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