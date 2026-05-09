using AAModClassic._Content._Dev.Invoker;
using AAModClassic._Content.Bunny._PostMoonlord.NPCs._BossRajahA;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs._BossShen;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;
using AAModClassic._Content.Void._PostMoonlord.NPCs._BossZero.Protocol;
using AAModClassic.Buffs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
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

            InvokerPlayer InvokerPlayer = Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>();
            if ((InvokerPlayer.banishing && npc.active && (InvokerPlayer.BanishDamage * InvokerPlayer.BanishDamageMult * InvokerPlayer.BanishLimit > npc.life)) || npc.GetGlobalNPC<AleisterStaffGlobalNPC>().IsBeingBanished)
            {
                npc.GetGlobalNPC<AleisterStaffGlobalNPC>().IsBeingBanished = true;
            }
        }
    }
}