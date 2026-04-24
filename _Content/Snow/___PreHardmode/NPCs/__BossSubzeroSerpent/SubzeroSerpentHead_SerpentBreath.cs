using AAModClassic.UI.WorldGen;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs.InfectionBiomeNPCAlternatesHelper;

namespace AAModClassic._Content.Snow.___PreHardmode.NPCs.__BossSubzeroSerpent
{
    internal class SubzeroSerpentHead_SerpentBreath : ModProjectile
    {
        public ref float BiomeType => ref Projectile.ai[1];

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Serpent Breath");
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 60;
            Projectile.timeLeft = 100;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Projectile.timeLeft > 60)
            {
                Projectile.timeLeft = 60;
            }
            if (Projectile.ai[0] > 7f)
            {
                float scaleModifier = 1f;
                if (Projectile.ai[0] == 8f)
                {
                    scaleModifier = 0.25f;
                }
                else if (Projectile.ai[0] == 9f)
                {
                    scaleModifier = 0.5f;
                }
                else if (Projectile.ai[0] == 10f)
                {
                    scaleModifier = 0.75f;
                }
                Projectile.ai[0] += 1f;

                int dustType = ModContent.DustType<Dusts.SnowDustLight>();
                switch (BiomeType)
                {
                    case (int)InfectionType.Corruption:
                        dustType = DustID.CursedTorch;
                        break;
                    case (int)InfectionType.Crimson:
                        dustType = DustID.GoldFlame;
                        break;
                    case (int)InfectionType.Inferno:
                        dustType = ModContent.DustType<Dusts.BroodmotherDust>();
                        break;
                    case (int)InfectionType.Mire:
                        dustType = ModContent.DustType<Dusts.AcidDust>();
                        break;
                    case (int)InfectionType.Hallow:
                        dustType = ModContent.DustType<Dusts.HallowedDustT>();
                        break;
                }

                if (Main.rand.NextBool(2))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int biomeDust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 1f);
                        if (Main.rand.NextBool(3))
                        {
                            Main.dust[biomeDust].noGravity = true;
                            Main.dust[biomeDust].scale *= 2.2f;
                            Dust expr_DD5D_cp_0 = Main.dust[biomeDust];
                            expr_DD5D_cp_0.velocity.X *= 2f;
                            Dust expr_DD7D_cp_0 = Main.dust[biomeDust];
                            expr_DD7D_cp_0.velocity.Y *= 2f;
                        }
                        Main.dust[biomeDust].scale *= 1.2f;
                        Dust expr_DDE2_cp_0 = Main.dust[biomeDust];
                        expr_DDE2_cp_0.velocity.X *= 1.2f;
                        Dust expr_DE02_cp_0 = Main.dust[biomeDust];
                        expr_DE02_cp_0.velocity.Y *= 1.2f;
                        Main.dust[biomeDust].scale *= scaleModifier;
                        Main.dust[biomeDust].velocity += Projectile.velocity;
                        if (!Main.dust[biomeDust].noGravity)
                        {
                            Main.dust[biomeDust].velocity *= 0.5f;
                        }
                    }
                }
            }
            else
            {
                Projectile.ai[0] += 1f;
            }
            Projectile.rotation += 0.3f * Projectile.direction;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Chilled, 300);

            switch (BiomeType)
            {
                case (int)InfectionType.Corruption:
                    target.AddBuff(BuffID.CursedInferno, 180);
                    break;
                case (int)InfectionType.Crimson:
                    target.AddBuff(BuffID.Ichor, 180);
                    break;
                case (int)InfectionType.Inferno:
                    target.AddBuff(BuffID.OnFire, 180);
                    break;
                case (int)InfectionType.Mire:
                    target.AddBuff(BuffID.Poisoned, 180);
                    break;
                case (int)InfectionType.Hallow:
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        target.AddBuff(BuffID.Confused, 180);
                    break;
            }
        }
    }
}