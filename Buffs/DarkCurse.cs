using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;


namespace AAMod.Buffs
{
    public class DarkCurse : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dark Curse");
            // Description.SetDefault("You deal significanlty less damage!");
            Main.debuff[Type] = true;
            longerExpertDebuff/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = false;
        }
    }
    public class DarkCurseEffect : GlobalNPC
    {
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (npc.HasBuff(Mod.Find<ModBuff>("DarkCurse").Type))
            {
                drawColor.R = (byte)(drawColor.R * .2f);
                drawColor.G = (byte)(drawColor.G * .2f);
                drawColor.B = (byte)(drawColor.B * .2f);
            }

        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if (npc.HasBuff(Mod.Find<ModBuff>("DarkCurse").Type))
            {
                damage = (int)(damage * .5f);
            }
        }
    }
}
