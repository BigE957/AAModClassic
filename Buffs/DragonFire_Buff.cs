using AAModClassic;
using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class DragonFire_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dragon Fire");
			// Description.SetDefault("Your damage output is reduced");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<AAPlayer>().dragonFire = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<Dusts.DragonflameDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 107);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.7f, 0.2f, 0.1f);
            
        }


    }
    public class DragonFireDamageReduction : GlobalNPC
    {
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (Main.player[projectile.owner].HasBuff(ModContent.BuffType<Buffs.DragonFire_Buff>()))
            {
                modifiers.TargetDamageMultiplier *= 0.8f;
            }
        }
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (player.HasBuff(ModContent.BuffType<Buffs.DragonFire_Buff>()))
            {
                modifiers.TargetDamageMultiplier *= 0.8f;
            }
        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if (npc.HasBuff(ModContent.BuffType<Buffs.DragonFire_Buff>()))
            {
                modifiers.FinalDamage.Flat -= 10;
                if(modifiers.FinalDamage.Flat < 0)
                {
                    modifiers.FinalDamage.Flat = 0;
                }
            }
        }
    }
}
