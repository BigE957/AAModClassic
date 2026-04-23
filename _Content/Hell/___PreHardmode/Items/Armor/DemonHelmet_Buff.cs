using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hell.___PreHardmode.Items.Armor
{
    public class DemonHelmet_Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Demon Lord");
            // Description.SetDefault("You command a small imp that follows you around.");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<DemonHelmet_ImpServant>()] > 0)
            {
                modPlayer.ImpServant = true;
            }
            if (!modPlayer.demonSet)
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