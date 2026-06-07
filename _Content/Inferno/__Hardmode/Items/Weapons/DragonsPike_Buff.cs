using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Weapons
{
    public class DragonsPike_Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flame Dragon");
            // Description.SetDefault("Summons a dragon to fight for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<DragonsPike_DragonHead>()] > 0) modPlayer.DragonMinion = true;
            if (!modPlayer.DragonMinion)
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