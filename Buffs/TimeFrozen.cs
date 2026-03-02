using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class TimeFrozen : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Time Frozen");
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<AAModGlobalNPC>().TimeFrozen = true;
        }
    }
}