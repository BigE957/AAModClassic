using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class Socc : ModBuff
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Socc");
            // Description.SetDefault("Socc.");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<SoccMinion>()] > 0)
            {
                modPlayer.Socc = true;
            }
            if (!modPlayer.Socc)
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