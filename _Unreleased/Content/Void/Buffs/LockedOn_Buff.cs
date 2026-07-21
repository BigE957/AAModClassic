using Terraria;
using Terraria.ModLoader;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero;

namespace AAModClassic._Unreleased.Content.Void.Buffs
{
    public class LockedOn_Buff : ModBuff
    {
        public string SBHP()
        {
            string Stringy = "";
            if (IZ)
            {
                Stringy = Stringy + @"
Infinity Zero: " + IZHP;
            }
            return Stringy;
        }

    	public bool IZ = false;
    	public int IZHP = 2000000;
    	public int ShenHP = 1600000;
        public InfinityZero Inf = null;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Locked On");
            // Description.SetDefault("");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            tip = "Target Locked." + SBHP();
            rare = 10;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 60;
            player.GetModPlayer<ZAAPlayer>().LockedOn = true;

            if (IZ && (Inf == null || !Inf.NPC.active))
            {
            	IZ = false;
            	player.GetModPlayer<ZAAPlayer>().InfZ = false;     		
            	player.GetModPlayer<ZAAPlayer>().GetIZHealth = 2000000;
                player.ClearBuff(Type);
                buffIndex--;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<InfinityZero>()) || NPC.AnyNPCs(ModContent.NPCType<InfinityZeroSpawn1>()) || IZ)
            {
            	IZ = true;
            	player.GetModPlayer<ZAAPlayer>().InfZ = true;
            	if (NPC.AnyNPCs(ModContent.NPCType<InfinityZero>()))
            	{
                    if (Inf == null)
                    {
                        Inf = (InfinityZero)Main.npc[NPC.FindFirstNPC(ModContent.NPCType<InfinityZero>())].ModNPC;
                    }
                    if (Inf.NPC.life != 0)
                    {
                        IZHP = Inf.NPC.life;
                        player.GetModPlayer<ZAAPlayer>().GetIZHealth = IZHP;
                    }
            	}
                else
                {
                    Inf = null;
                }
            }
        }
    }
}
    
