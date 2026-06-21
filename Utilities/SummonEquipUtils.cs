using AAModClassic.Base.BaseMod.Base;
using AAModClassic.UI.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using static AAModClassic.Assets.AssetDirectory;

namespace AAModClassic.Utilities
{
    public static class SummonEquipUtils
    {
        public static void HandleSummonerEquip<TBuff>(Player player) where TBuff : ModBuff
        {
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(ModContent.BuffType<TBuff>()) == -1)
                    player.AddBuff(ModContent.BuffType<TBuff>(), 18000, true);
            }
        }

        public static void HandleMinionPersistence<TBuff>(this Projectile projectile, Player player) where TBuff : ModBuff
        {
            if (player.HasBuff<TBuff>())
                projectile.timeLeft = 2;
        }

        public abstract class MinionBuffAbstract<TEquip, TMinion> : ModBuff where TEquip : ModItem where TMinion : ModProjectile
        {
            public virtual int MinionDamage => 0;
            public virtual float MinionKnockback => 1;
            public virtual DamageClass MinionDamageType => DamageClass.Summon;
            public virtual bool ShouldScaleWithClassDamage => false;
            public virtual bool MinionHasVanitySupport => false;

            public override void SetStaticDefaults()
            {
                Main.buffNoSave[Type] = true;
                Main.buffNoTimeDisplay[Type] = true;
            }

            public override void Update(Player player, ref int buffIndex)
            {
                if (!BasePlayer.HasEquipment(player, ModContent.ItemType<TEquip>(), true, true))
                {
                    player.DelBuff(buffIndex);
                    buffIndex--;
                }
                else
                {
                    player.buffTime[buffIndex] = 18000;

                    if (player.ownedProjectileCounts[ModContent.ProjectileType<TMinion>()] < 1)
                    {
                        int finalDamage = ShouldScaleWithClassDamage ? (int)player.GetDamage(MinionDamageType).ApplyTo(MinionDamage) : MinionDamage;

                        Projectile.NewProjectile(player.GetSource_ReleaseEntity(), player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<TMinion>(), finalDamage, MinionKnockback, Main.myPlayer, 0f, 0f);
                    }
                }

                if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && MinionHasVanitySupport && BasePlayer.HasEquipment(player, ModContent.ItemType<TEquip>(), false, true))
                {
                    player.DelBuff(buffIndex);
                    buffIndex--;
                }
            }
        }
    }
}
