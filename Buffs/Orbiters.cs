using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class Orbiters : ModBuff
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
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("FireOrbiter").Type] > 0)
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