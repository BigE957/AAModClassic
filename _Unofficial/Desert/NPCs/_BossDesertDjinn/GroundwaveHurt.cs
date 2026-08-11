using AAModClassic._Content.Desert.___PreHardmode.NPCs.__BossDesertDjinn;
using AAModClassic._Content.Desert.___PreHardmode.NPCs._Day;
using AAModClassic.Assets;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Desert.NPCs._BossDesertDjinn
{
    public class GroundwaveHurt : ModProjectile
    {
        public override string Texture => AssetDirectory.General.Nothing;

        public override void SetDefaults()
        {
            Projectile.width = 180;
            Projectile.height = 16;
            Projectile.hostile = true;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1200;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void PostAI()
        {
            if (Projectile.ai[0] <= Projectile.ai[1])
                Projectile.Resize(Projectile.width, (int)(Projectile.ai[2] * (Projectile.ai[0] / Projectile.ai[1])));

            Vector2 ground = CollisionUtils.FindSurfaceBelow((Projectile.Center + Vector2.UnitX * Projectile.ai[0]).ToTileCoordinates()).ToWorldCoordinates();
            Projectile.Bottom = new(Projectile.Center.X, ground.Y);

            if (Projectile.ai[0] < Projectile.ai[1])
                Projectile.position -= Projectile.velocity;

            Projectile.ai[0]++;
        }

        private static readonly HashSet<int> ImmuneNPCs = 
        [
            ModContent.NPCType<DesertDjinn_Unofficial>(),
            ModContent.NPCType<DesertDjinn>(),
            ModContent.NPCType<DustDjinn>(),
            NPCID.SandElemental,
            NPCID.DesertDjinn
        ];

        public override bool? CanHitNPC(NPC target) => !ImmuneNPCs.Contains(target.type);

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.DisableKnockback();
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            modifiers.Knockback *= 0f;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (target.noKnockback)
                target.velocity = Projectile.velocity / 2f + Vector2.UnitY * -10;
            else
                target.velocity = Projectile.velocity + Vector2.UnitY * -16;

            target.velocity *= Projectile.height == 150 ? 1f : 0.6f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.noTileCollide)
                return;

            Vector2 maxVelocity = Projectile.velocity * 1.5f + Vector2.UnitY * -24;

            target.velocity += Vector2.Lerp(Vector2.Zero, maxVelocity, target.knockBackResist) * (Projectile.height == 150 ? 1f : 0.6f);
        }
    }
}
