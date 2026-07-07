using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Weapons
{
    public class SnakeStaff_Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Snow Serpent");
            // Description.SetDefault("Summons a snow serpent to fight for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<SnakeStaff_SerpentHead>()] > 0) modPlayer.SnakeMinion = true;
            if (!modPlayer.SnakeMinion)
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