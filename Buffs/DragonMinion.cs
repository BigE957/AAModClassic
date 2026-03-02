using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class DragonMinion : ModBuff
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
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("DragonHead").Type] > 0) modPlayer.DragonMinion = true;
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