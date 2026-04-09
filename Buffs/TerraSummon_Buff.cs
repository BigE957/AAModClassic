using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class TerraSummon_Buff : ModBuff
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Minions");
            // Description.SetDefault("An array of unity constructs at your disposal");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            modPlayer.TerraSummon = true;
			
            if (!modPlayer.TerraSummon)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}