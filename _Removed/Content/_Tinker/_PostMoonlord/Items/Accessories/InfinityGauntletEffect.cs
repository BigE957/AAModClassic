using AAModClassic;
using AAModClassic._Content.Void.___PreHardmode.NPCs;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Accessories;
using AAModClassic._Removed.Dusts;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content._Tinker._PostMoonlord.Items.Accessories
{
    public class InfinityGauntletEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<InfinityGauntletPlayer>().effect = true;
        }
    }

    public class InfinityGauntletPlayer : EquipmentEffectPlayer
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (effect && AAMod.AccessoryAbilityKey.JustPressed && !Player.HasBuff<InfinityGauntletEffect_InfinityBurnout>())
            {
                Player.AddBuff(ModContent.BuffType<InfinityGauntletEffect_InfinityBurnout>(), 18000);

                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.position, Vector2.Zero, ModContent.ProjectileType<InfinityGauntletEffect_Snap>(), 0, 0, Player.whoAmI);

                if (Main.netMode != 1)
                {
                    BaseUtility.Chat("Perfectly Balanced, as all things should be...", Color.Purple);
                }

                Main.npc.Where(x => x.active && !x.townNPC && x.type != NPCID.TargetDummy && !NPCID.Sets.ShouldBeCountedAsBoss[x.type] && !x.boss && x.type != ModContent.NPCType<ZeroDeactivated>()).ToList().ForEach(x =>
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int dust = Dust.NewDust(x.position, x.width, x.height, ModContent.DustType<SnapDust>(), 0f, 0f, 0);

                        Main.dust[dust].velocity.Y = 3f + Main.rand.Next(30) * 0.1f;

                        Dust expr_292_cp_0 = Main.dust[dust];
                        expr_292_cp_0.velocity.Y *= Main.dust[dust].scale;

                        Main.dust[dust].velocity.X = (Main.cloudAlpha + 0.5f) * 25f + Main.rand.NextFloat() * 0.2f - 0.1f;

                        Dust expr_370_cp_0 = Main.dust[dust];
                        expr_370_cp_0.velocity.Y += expr_370_cp_0.velocity.Y * 0.5f;

                        Dust expr_38E_cp_0 = Main.dust[dust];
                        expr_38E_cp_0.velocity.Y *= 1f + 0.3f * Main.cloudAlpha;

                        Main.dust[dust].scale += Main.cloudAlpha * 0.2f;
                        Main.dust[dust].velocity *= 1f + Main.cloudAlpha * 0.5f;
                    }

                    x.NPCLoot();
                    x.active = false;
                });
            }
        }
    }
}