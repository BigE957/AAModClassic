using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using AAMod.Projectiles.Greed.WKG;

namespace AAMod.Buffs
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
                npc.StrikeNPC(npc.GetGlobalNPC<FallDamage>().damage, 0, 0, true);
                Projectile.NewProjectile(npc.position, Vector2.Zero, ModContent.ProjectileType<Earthquake>(), npc.GetGlobalNPC<FallDamage>().damage, 10, Main.myPlayer);
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
