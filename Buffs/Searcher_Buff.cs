using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class Searcher_Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Searcher Scout");
            // Description.SetDefault("Summons a searcher to fight for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Searcher>()] > 0)
            {
                modPlayer.Searcher = true;
            }
            if (!modPlayer.doomite)
            {
                modPlayer.Searcher = false;
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