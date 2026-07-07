using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terrarium.__Hardmode.Items.Weapons
{
    public class TerraGauntlet_Buff : ModBuff
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
            ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
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