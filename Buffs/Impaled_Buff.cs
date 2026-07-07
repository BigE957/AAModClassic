using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.Weapons;

namespace AAModClassic.Buffs
{
    public class Impaled_Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Impaled");
            // Description.SetDefault("Ouch!");
            Main.debuff[Type] = true;


            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            //int num = npc.lifeRegenExpectedLossPerSecond;
            if (npc.lifeRegen > 0)
                npc.lifeRegen = 0;
            int JavelinCount = 0;
            int impaleDamage = 0;
            foreach(Projectile p in Main.ActiveProjectiles)
            {
                if (p.active && p.GetGlobalProjectile<ImplaingProjectile>().CanImpale && ((p.ai[0] == 1f && p.ai[1] == npc.whoAmI) || (p.type == ModContent.ProjectileType<OreChunk>() && p.ai[0] == 1f && p.ai[1] == ItemID.TungstenOre && p.localAI[1] == npc.whoAmI)))
                {
                    impaleDamage += p.GetGlobalProjectile<ImplaingProjectile>().damagePerImpaler;
                    JavelinCount++;
                }
            }
            npc.lifeRegen -= impaleDamage * 2;
            npc.lifeRegenExpectedLossPerSecond = impaleDamage;
        }
    }

    public class ImplaingProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public bool CanImpale = false;
        public int damagePerImpaler = 0;
    }
}
