using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class PrinceFishron : ModBuff
    {
        public override string Texture => "AAModClassic/Buffs/_Blankbuff";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Pretty Pony");
            // Description.SetDefault("Its a horse.");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<Mounts.PrinceFishron>(), player);
            player.buffTime[buffIndex] = 10;
        }
    }
}
