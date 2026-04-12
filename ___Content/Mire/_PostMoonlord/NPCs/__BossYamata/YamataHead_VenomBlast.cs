using AAModClassic.___Content.Mire.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._PostMoonlord.NPCs.__BossYamata
{
    public class YamataHead_VenomBlast : ModProjectile
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Venom");     
            Main.projFrames[Projectile.type] = 5;     
		}

		public override void SetDefaults()
		{
			Projectile.width = 14;               
			Projectile.height = 14;              
			Projectile.aiStyle = ProjAIStyleID.Arrow;             
			Projectile.friendly = false;         
			Projectile.hostile = true;        
			Projectile.penetrate = 1;           
			Projectile.timeLeft = 600;          
			Projectile.alpha = 20;              
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;                 
            
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<HydraToxin_Buff>(), 300);
        }

        public override void OnKill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.YamataDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(86, 191, 188));
                Main.dust[num469].velocity *= 2f;
            }
            SoundEngine.PlaySound(SoundID.Item89);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y - 51 + 8, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<YamataHead_Shockwave>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }
    }
}
