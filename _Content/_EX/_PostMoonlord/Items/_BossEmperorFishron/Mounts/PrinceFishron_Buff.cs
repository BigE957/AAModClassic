using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items._BossEmperorFishron.Mounts
{
    public class PrinceFishron_Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Prince Fishron");
            // Description.SetDefault("The fishron heir");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<PrinceFishron>(), player);
            player.buffTime[buffIndex] = 10;
        }
    }
}
