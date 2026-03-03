using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class RadiumInferno : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Radium Inferno");
            // Description.SetDefault("Rapidly depleting life");
            Main.debuff[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            npc.lifeRegen -= 200;
            npc.lifeRegenExpectedLossPerSecond = 100;
            Dust.NewDust(npc.position, npc.width, npc.height, Mod.Find<ModDust>("RadiumDust").Type);
        }
    }
    
}
