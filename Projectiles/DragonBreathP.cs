using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;


namespace AAMod.Projectiles   //The directory for your .cs and .png; Example: TutorialMOD/Projectiles
{
    public class DragonBreathP : ModProjectile   //make sure the sprite file is named like the class name (CustomYoyoProjectile)
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon's Breath");
        }

        public override void SetDefaults()
        {
            Projectile.extraUpdates = 0;
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 12f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 300f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 14f;
        }

        public float[] internalAI = new float[1];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadFloat();
            }
        }

        public override void PostAI()
        {
            int Target = BaseAI.GetNPC(Projectile.Center, -1, 500);
            if (Target != -1)
            {
                NPC target = Main.npc[Target];
                BaseAI.ShootPeriodic(Projectile, target.position, target.width, target.height, ModContent.ProjectileType<DragonBreath>(), ref internalAI[0], 5, Projectile.damage, 4, true);
            }
        }
    }
}
