using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using AAModClassic.Globals;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons
{
    public class TheVortex_Holdout : ModProjectile  
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Vortex");
        }

        public override void SetDefaults()
        {
            Projectile.extraUpdates = 5;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = ProjAIStyleID.Yoyo;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 60f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 1000f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 15f;
        }
        int ProjTimer = 0;

        public override void PostAI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                ProjTimer++;
                if (ProjTimer >= 20)
                {
                    ProjTimer = 0;
                    int NPCTarget = Target();

                    if (NPCTarget != -1 && AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<TheVortex_Proj>()) < 5)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity, ModContent.ProjectileType<TheVortex_Proj>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                    }
                }
            }
        }

        private int Target()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 500;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.CanBeChasedBy(Projectile) && (!n.wet || homingCanAimAtWetEnemies))
                {
                    float distance = Projectile.Distance(n.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                        (
                            selectedTarget == -1 || //there is no selected target
                            Projectile.Distance(Main.npc[selectedTarget].Center) > distance) 
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }


        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Width(), TextureAssets.Projectile[Projectile.type].Height(), 0, 0);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.direction, 1, frame, lightColor, true);
            return false;
        }
    }
}
