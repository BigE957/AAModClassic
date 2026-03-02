using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Yamata   //The directory for your .cs and .png; Example: TutorialMOD/Projectiles
{
    public class Crescent : ModProjectile   //make sure the sprite file is named like the class name (CustomYoyoProjectile)
    {
        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Crescent");
        }

        public override void SetDefaults()
        {
            Projectile.extraUpdates = 3;
            Projectile.width = 16;
            Projectile.height = 16; 
            Projectile.aiStyle = ProjAIStyleID.Yoyo;
            Projectile.friendly = true; 
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 60f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 1000f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 17f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(Mod.Find<ModBuff>("Moonraze").Type, 600);
        }
        int ProjTimer = 0;

        public override void PostAI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                ProjTimer++;
                if (ProjTimer >= 50)
                {
                    ProjTimer = 0;
                    Projectile.NewProjectile(Projectile.position, Vector2.Zero, ModContent.ProjectileType<FlairdraCyclone>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
            }
        }
    }
}
