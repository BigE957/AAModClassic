using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Globals
{
    public class AAModGlobalBuff : GlobalBuff
	{
        public override void Update(int type, NPC npc, ref int buffIndex)
		{
            if(type == BuffID.BrokenArmor && !npc.boss)
            {
                npc.defense /= 2;
            }
            if(type == BuffID.Weak && !npc.boss)
            {
                npc.damage = (int)(npc.damage * 0.051f);
                npc.defense -= 4;
                if(npc.velocity.X > 0.1f) npc.velocity.X -= 0.1f;
                else if(npc.velocity.X < -0.1f) npc.velocity.X += 0.1f;
            }
		}
    }
}
