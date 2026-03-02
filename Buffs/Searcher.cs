using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class Searcher : ModBuff
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
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Searcher").Type] > 0)
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