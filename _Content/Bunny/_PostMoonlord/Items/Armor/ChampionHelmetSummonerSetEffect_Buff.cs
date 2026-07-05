using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items.Armor
{
    public class ChampionHelmetSummonerSetEffect_Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Baron Bunny");
            // Description.SetDefault("Baron Bunny protects you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (!player.GetModPlayer<ChampionHelmetSummonerSetPlayer>().effect)
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