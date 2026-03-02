using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Toad
{
    public class Seed : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Waterleaf Seed");
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        { 
            Collision.HitTiles(Projectile.position, oldVelocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            NPC.NewNPC((int)Projectile.Top.X, (int)Projectile.Top.Y, Mod.Find<ModNPC>("ShroomGlow").Type, Mod.Find<ModProjectile>("ShroomGlow").Type, Projectile.damage, 0, Projectile.owner, 0, 1);
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<ShroomDust>(), 0f, 0f, 100, default, 1.2f);
                Main.dust[dustIndex].velocity *= 1.8f;
            }
        }
    }
}