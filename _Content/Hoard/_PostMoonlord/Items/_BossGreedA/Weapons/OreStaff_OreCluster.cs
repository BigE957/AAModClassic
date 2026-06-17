using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.Weapons
{
    public class OreStaff_OreCluster : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ore Cluster");
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.BoulderStaffOfEarth);
            Projectile.penetrate = 1;  
            Projectile.width = 44;
            Projectile.height = 44;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.Kill();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return true;
        }

        public override void OnKill(int a)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int i = 0; i < Main.rand.Next(5, 10); i++)
            {
                int x = Main.rand.Next(-6, 6);
                int y = -Main.rand.Next(3, 5);
                int type = OreCannonSystem.OreData.Keys.ToArray()[Main.rand.Next(OreCannonSystem.OreData.Count)];
                int p = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position, new Vector2(x, y), ModContent.ProjectileType<GravityAffectedOreChunk>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, ai1: type);
                OreProjectileUtils.TriggerOreOnSpawn(Main.projectile[p]);
            }
        }
    }
}
