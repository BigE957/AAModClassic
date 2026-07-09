using AAModClassic._Vanilla.Facsimiles._1._3._5._3;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.___PreHardmode.NPCs.__BossSubzeroSerpent
{
    public class SubzeroSerpent_IceBall : BoulderStaffOfEarthFacsimile
    {
        private static readonly Dictionary<string, Asset<Texture2D>> BiomeTextures = [];

        public string BiomeType = "Default";

        public override string Texture => "AAModClassic/_Content/Snow/___PreHardmode/NPCs/__BossSubzeroSerpent/BallTextures/IceBall";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ice Ball");
            Main.projFrames[Projectile.type] = 1;

            foreach (var biome in BiomeConvertableNPC.Biomes)
            {
                if (biome.Name == "Default")
                    BiomeTextures.Add(biome.Name, TextureAssets.Projectile[Type]);
                else if (biome.Name != "Void")
                    BiomeTextures.Add(biome.Name, ModContent.Request<Texture2D>(Texture + "_" + biome.Name));
            }
        }

        public override void SetDefaults()
        {
            //Projectile.CloneDefaults(ProjectileID.BoulderStaffOfEarth);
            base.SetDefaults();
            Projectile.penetrate = 1;  
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.timeLeft = 300;
            Projectile.hostile = true;
            Projectile.friendly = false;
        }

        public override void PostAI()
        {
            Projectile.frame = (int)Projectile.ai[1];
        }

        public override bool PreKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.IceDust>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / pieCut * 6.28f);
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.IceDust>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m /pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
            }
            return true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = BiomeTextures[BiomeType].Value;
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, lightColor * Projectile.Opacity, Projectile.rotation, tex.Size() * 0.5f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}
