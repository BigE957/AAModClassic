using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class DragonSpirit_Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon Spirit");
            // Description.SetDefault("Summons a Dragon Spirit to fight for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<DragonSpirit>()] > 0)
            {
                modPlayer.DragonSpirit = true;
            }
            if (!modPlayer.ChaosSu)
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