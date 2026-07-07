using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Armor
{
    public class DoomiteHelmetSetEffect_Buff : ModBuff
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
            ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
            if (!player.GetModPlayer<DoomiteHelmetSetPlayer>().effect)
            {
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