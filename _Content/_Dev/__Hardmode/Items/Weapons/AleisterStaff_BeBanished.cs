using AAModClassic._Content._EX._PostMoonlord.Items.Accessories;
using Terraria;
using Terraria.ModLoader;


namespace AAModClassic._Content._Dev.__Hardmode.Items.Weapons
{
    public class AleisterStaff_BeBanished : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Be Banished");
            // Description.SetDefault("You are marked by Invoked Magic");
            Main.debuff[Type] = false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<AleisterStaffGlobalNPC>().Banished = true;

            TheBookOfTheLaw_InvokerPlayer InvokerPlayer = Main.player[Main.myPlayer].GetModPlayer<TheBookOfTheLaw_InvokerPlayer>();
            if ((InvokerPlayer.banishing && npc.active && (InvokerPlayer.BanishDamage * InvokerPlayer.BanishDamageMult * InvokerPlayer.BanishLimit > npc.life)) || npc.GetGlobalNPC<AleisterStaffGlobalNPC>().IsBeingBanished)
            {
                npc.GetGlobalNPC<AleisterStaffGlobalNPC>().IsBeingBanished = true;
            }
        }
    }
}