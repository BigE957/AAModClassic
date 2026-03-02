using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Tools
{

    public class DarkmatterJackhammerPro : ModProjectile
    {
        public override void SetDefaults()
        {

            Projectile.width = 22;
            Projectile.height = 52;
            Projectile.aiStyle = ProjAIStyleID.Drill;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Darkmatter Jackhammer");
        }


        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(Mod.Find<ModBuff>("Electrified").Type, 500);
        }
    }
}