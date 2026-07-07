using AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Weapons;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terra.__Hardmode.Items.Armor
{
    public class TerraHelmetSummonerSetEffect_Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Crystal");
            // Description.SetDefault("Shoots orbs at nearby enemies, if so desired");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            TerraHelmetMageSetPlayer modPlayer = player.GetModPlayer<TerraHelmetMageSetPlayer>();
            if (!modPlayer.effect)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
                player.buffTime[buffIndex] = 18000;
        }
    }
}