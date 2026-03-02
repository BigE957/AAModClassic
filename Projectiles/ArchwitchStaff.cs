using System.Collections.Generic;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class ArchwitchStaff : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Archwitch Staff");
        }
        public override void SetDefaults()
        {
            Projectile.width = 110;
            Projectile.height = 110;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;       
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
            Projectile.soundDelay--;
            if (Projectile.soundDelay <= 0)
            {
                SoundEngine.PlaySound(SoundID.Item15, Projectile.Center);
                Projectile.soundDelay = 45;
            }
            Player player = Main.player[Projectile.owner];
            if (Main.myPlayer == Projectile.owner)
            {
                if (!player.channel || player.noItems || player.CCed)
                {
                    Projectile.Kill();
                }
            }
            Lighting.AddLight(Projectile.Center, .6f, 0.6f, .7f);
            Projectile.Center = player.MountedCenter;
            Projectile.position.X += player.width / 2 * player.direction;
            Projectile.spriteDirection = player.direction;
            Projectile.rotation += 0.3f * player.direction;
            if (Projectile.rotation > MathHelper.TwoPi)
            {
                Projectile.rotation -= MathHelper.TwoPi;
            }
            else if (Projectile.rotation < 0)
            {
                Projectile.rotation += MathHelper.TwoPi;
            }
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = Projectile.rotation;
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame);
            Main.dust[dust].velocity /= 1f;

            int Target = BaseAI.GetNPC(Projectile.Center, -1, 500);
            if (Target != -1)
            {
                NPC target = Main.npc[Target];
                int p = BaseAI.ShootPeriodic(Projectile, target.position, target.width, target.height, ModContent.ProjectileType<ArchwitchStar>(), ref Projectile.ai[0], 40, Projectile.damage, 4, true);
                Main.projectile[p].ai[1] = target.whoAmI;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }

        public static int[] GetNPCs(Vector2 center, int npcType = -1, float distance = 500F)
        {
            List<int> allNPCs = new List<int>();
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc != null && npc.active && npc.life > 0 && (npcType == -1 || npc.type == npcType) && npc.type != NPCID.TargetDummy && !npc.friendly && (distance == -1 || npc.Distance(center) < distance))
                {
                    bool add = true;
                    if (add) { allNPCs.Add(i); }
                }
            }
            return allNPCs.ToArray();
        }
    }
}