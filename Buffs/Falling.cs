using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using AAModClassic.Projectiles.Greed.WKG;

namespace AAModClassic.Buffs
{
    public class Falling : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Falling");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
			Main.buffNoSave[Type] = true;
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.collideY)
            {
                NPC.HitInfo hit = new();
                hit.Damage = npc.GetGlobalNPC<FallDamage>().damage;
                hit.HitDirection = 0;
                hit.Knockback = 0f;
                hit.Crit = true;
                npc.StrikeNPC(hit);
                Projectile.NewProjectile(npc.GetSource_OnHurt(null), npc.position, Vector2.Zero, ModContent.ProjectileType<Earthquake>(), npc.GetGlobalNPC<FallDamage>().damage, 10, Main.myPlayer);
                npc.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }

    public class FallDamage : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public int damage = 0;
    }
}
