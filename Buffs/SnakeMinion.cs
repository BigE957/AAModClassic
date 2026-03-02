using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class SnakeMinion : ModBuff
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
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("SerpentHead").Type] > 0) modPlayer.SnakeMinion = true;
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