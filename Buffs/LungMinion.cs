using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class LungMinion : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ancient Lung");
            // Description.SetDefault("Summons an ancient dragon to fight for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("LungHead").Type] > 0) modPlayer.LungMinion = true;
            if (!modPlayer.LungMinion)
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