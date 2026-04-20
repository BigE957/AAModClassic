using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Bunny._PostMoonlord.Items.Armor
{
    public class ChampionHelmetSummoner_Buff : ModBuff
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
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<ChampionHelmetSummoner_BaronBunny>()] > 0)
            {
                modPlayer.Baron = true;
            }
            if (!modPlayer.ChampionSu)
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