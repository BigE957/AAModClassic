using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public abstract class ArmorBounsLeech : ModProjectile
    {
        protected int dust = 0;
        bool runOnce = true;
        int effectPotency = 0;
        protected float potencyFactor = 1f;
        public virtual void PlayerBenifit(int potency, Player player)
        {
           
        }

        public override void AI()
        {
            if (runOnce)
            {
                runOnce = false;
                Projectile.localNPCImmunity[(int)Projectile.ai[0]] = 0;
            }
            if (effectPotency > 0)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dust);
                Player player = Main.player[Projectile.owner];
                Projectile.velocity = (player.Center - Projectile.Center).SafeNormalize(-Vector2.UnitY) * 12f;
                if (Collision.CheckAABBvAABBCollision(player.position, player.Size, Projectile.position, Projectile.Size))
                {
                    PlayerBenifit(effectPotency, player);
                    Projectile.Kill();
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 0;
            Projectile.localNPCImmunity[target.whoAmI] = -1;
            effectPotency = (int)(damage * potencyFactor);
            if (effectPotency > 0)
            {
                Projectile.timeLeft = 120;
            }


        }
    }
    public class DarkLeech : ArmorBounsLeech
    {

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = Projectile.height = 4;
            Projectile.usesLocalNPCImmunity = true;
            for (int i = 0; i < Projectile.localNPCImmunity.Length; i++)
            {
                Projectile.localNPCImmunity[i] = -1;
            }
            Projectile.timeLeft = 3;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            dust = Mod.Find<ModDust>("DarkmatterDust").Type;
            potencyFactor = .02f;
        }
        public override void PlayerBenifit(int potency, Player player)
        {
            player.statLife += potency;
            player.HealEffect(potency, true);
        }

    }
    public class SunSiphon : ArmorBounsLeech
    {

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = Projectile.height = 4;
            Projectile.usesLocalNPCImmunity = true;
            for (int i = 0; i < Projectile.localNPCImmunity.Length; i++)
            {
                Projectile.localNPCImmunity[i] = -1;
            }
            Projectile.timeLeft = 3;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            dust = Mod.Find<ModDust>("RadiumDust").Type;
            potencyFactor = .25f;
        }
        public override void PlayerBenifit(int potency, Player player)
        {
            int manaCount = 0;
            int overloadCount = 0;
            for(; potency > 0; potency--)
            {
                if(player.statManaMax2 > player.statMana )
                {
                    player.statMana++;
                    manaCount++;
                }
                else
                {
                    overloadCount++;
                    
                }
            }
            if(manaCount>0)
            {
                player.ManaEffect(manaCount);
            }
            if (overloadCount >0)
            {
                if (player.HasBuff(Mod.Find<ModBuff>("ManaOverload").Type))
                {

                    player.buffTime[player.FindBuffIndex(Mod.Find<ModBuff>("ManaOverload").Type)] += overloadCount * 2;
                    if (player.buffTime[player.FindBuffIndex(Mod.Find<ModBuff>("ManaOverload").Type)] > 600)
                    {
                        player.buffTime[player.FindBuffIndex(Mod.Find<ModBuff>("ManaOverload").Type)] = 600;
                    }
                }
                else
                {
                    player.AddBuff(Mod.Find<ModBuff>("ManaOverload").Type, overloadCount * 2);
                }
                CombatText.NewText(player.Hitbox, Color.Purple, overloadCount * 2);
            }
            
        }

    }
}
