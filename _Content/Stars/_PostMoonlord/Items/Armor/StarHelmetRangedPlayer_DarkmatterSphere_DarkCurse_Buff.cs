using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    public class StarHelmetRangedPlayer_DarkmatterSphere_DarkCurse_Buff : ModBuff
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
            if (npc.HasBuff(ModContent.BuffType<StarHelmetRangedPlayer_DarkmatterSphere_DarkCurse_Buff>()))
            {
                drawColor.R = (byte)(drawColor.R * .2f);
                drawColor.G = (byte)(drawColor.G * .2f);
                drawColor.B = (byte)(drawColor.B * .2f);
            }

        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if (npc.HasBuff(ModContent.BuffType<StarHelmetRangedPlayer_DarkmatterSphere_DarkCurse_Buff>()))
            {
                modifiers.IncomingDamageMultiplier *= 0.5f;
            }
        }
    }
}
