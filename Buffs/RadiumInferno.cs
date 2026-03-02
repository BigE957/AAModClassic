using Terraria;
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
            longerExpertDebuff/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = false;
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
