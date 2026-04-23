using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items.Armor
{
    public class ChampionHelmetMage_ChampionBoost2 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Champion Boost");
            // Description.SetDefault("Increased stats");
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)

        {
            AAPlayer mplayer = player.GetModPlayer<AAPlayer>();
            player.manaRegenBonus += 30;
            player.GetDamage(DamageClass.Generic) += 0.18f * mplayer.CarrotBuff;
            player.lifeRegen += 12 * mplayer.CarrotBuff;

            if (player.buffTime[buffIndex] == 2)
            {
                mplayer.CarrotBuff--;
                player.buffType[buffIndex] = ModContent.BuffType<ChampionHelmetMage_ChampionBoost1>();
                player.buffTime[buffIndex] = 480;
            }
        }
    }
}