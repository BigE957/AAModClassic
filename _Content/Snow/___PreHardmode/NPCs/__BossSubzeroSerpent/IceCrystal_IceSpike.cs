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
    public class IceCrystal_IceSpike : ModProjectile
    {
        private static readonly Dictionary<string, Asset<Texture2D>> BiomeTextures = [];

        public string BiomeType = "Default";
        public override string Texture => "AAModClassic/_Content/Snow/___PreHardmode/NPCs/__BossSubzeroSerpent/SpikeTextures/IceSpike";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ice Spike");
            Main.projFrames[Projectile.type] = 5;

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
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.tileCollide = true;
            Projectile.coldDamage = true;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            if (Projectile.alpha > 50)
            {
                Projectile.alpha -= 10;
            }

            Projectile.ai[0]++;
            if (Projectile.ai[0] >= 50) { Projectile.velocity.Y += 1; }
            if (Projectile.velocity.Y > 16) { Projectile.velocity.Y = 16; }

            if (Projectile.frameCounter != 1)
            {
                Projectile.frameCounter = 1;
                Projectile.frame = Main.rand.Next(5);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle newFrame = new Rectangle();
            newFrame.Width = TextureAssets.Projectile[Projectile.type].Width();
            newFrame.Height = TextureAssets.Projectile[Projectile.type].Height() / 5;
            newFrame.X = 0;
            newFrame.Y = Projectile.frame * (TextureAssets.Projectile[Projectile.type].Height() / 5);

            Main.EntitySpriteDraw(BiomeTextures[BiomeType].Value, Projectile.Center - Main.screenPosition, newFrame, Projectile.GetAlpha(lightColor), Projectile.rotation, newFrame.Size() * 0.5f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
            for (int i = 0; i < 8; i++)
            {
                int dustID = Dust.NewDust(Projectile.Center, 2, 2, ModContent.DustType<Dusts.SnowDust>(), Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 100, Color.White, 0.8f);
            }
        }
    }
}
