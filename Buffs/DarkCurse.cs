using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Buffs
{
    public class DarkCurse : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dark Curse");
            // Description.SetDefault("You deal significanlty less damage!");
            Main.debuff[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }
    }
    public class DarkCurseEffect : GlobalNPC
    {
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (npc.HasBuff(ModContent.BuffType<DarkCurse>()))
            {
                drawColor.R = (byte)(drawColor.R * .2f);
                drawColor.G = (byte)(drawColor.G * .2f);
                drawColor.B = (byte)(drawColor.B * .2f);
            }

        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if (npc.HasBuff(ModContent.BuffType<DarkCurse>()))
            {
                modifiers.IncomingDamageMultiplier *= 0.5f;
            }
        }
    }
}
