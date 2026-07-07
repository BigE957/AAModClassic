using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items.Armor
{
    public class ChampionHelmetMageSetEffect_ChampionBoost2 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Champion Boost");
            // Description.SetDefault("Increased stats");
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)

        {
            ChampionHelmetMageSetPlayer mplayer = player.GetModPlayer<ChampionHelmetMageSetPlayer>();
            player.manaRegenBonus += 30;
            player.GetDamage(DamageClass.Generic) += 0.18f * mplayer.CarrotBuff;
            player.lifeRegen += 12 * mplayer.CarrotBuff;

            if (player.buffTime[buffIndex] == 2)
            {
                mplayer.CarrotBuff--;
                player.buffType[buffIndex] = ModContent.BuffType<ChampionHelmetMageSetEffect_ChampionBoost1>();
                player.buffTime[buffIndex] = 480;
            }
        }
    }
}