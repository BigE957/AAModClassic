using AAModClassic._Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Underground.___PreHardmode.Items.Armor
{
    public class AncientGoldChestplateSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<AncientGoldChestplateSetPlayer>().effect = true;
        }
    }

    public class AncientGoldChestplateSetPlayer : EquipmentEffectPlayer
    {
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (effect)
            {
                long num = 0;
                for (int i = 0; i < 54; i++)
                {
                    if (Player.inventory[i].type == ItemID.CopperCoin)
                    {
                        num += Player.inventory[i].stack;
                    }
                    if (Player.inventory[i].type == ItemID.SilverCoin)
                    {
                        num += Player.inventory[i].stack * 100;
                    }
                    if (Player.inventory[i].type == ItemID.GoldCoin)
                    {
                        num += Player.inventory[i].stack * 10000;
                    }
                    if (Player.inventory[i].type == ItemID.PlatinumCoin)
                    {
                        num += Player.inventory[i].stack * 1000000;
                    }
                }

                float damage = -1;
                if (modifiers.DamageSource.TryGetCausingEntity(out Entity sourceEntity))
                {
                    switch (sourceEntity)
                    {
                        case Projectile proj:
                            damage = modifiers.GetDamage(proj.damage, Player.statDefense, Player.DefenseEffectiveness.Value);
                            break;
                        case NPC npc:
                            damage = modifiers.GetDamage(npc.damage, Player.statDefense, Player.DefenseEffectiveness.Value);
                            break;
                    }
                }

                if (damage != -1 && num >= damage * 10000)
                {
                    for (int i = 0; i < 54; i++)
                    {
                        if (Player.inventory[i].type == ItemID.CopperCoin)
                        {
                            Player.inventory[i].stack = 0;
                            Player.inventory[i].TurnToAir();
                        }
                        if (Player.inventory[i].type == ItemID.SilverCoin)
                        {
                            Player.inventory[i].stack = 0;
                            Player.inventory[i].TurnToAir();
                        }
                        if (Player.inventory[i].type == ItemID.GoldCoin)
                        {
                            Player.inventory[i].stack = 0;
                            Player.inventory[i].TurnToAir();
                        }
                        if (Player.inventory[i].type == ItemID.PlatinumCoin)
                        {
                            Player.inventory[i].stack = 0;
                            Player.inventory[i].TurnToAir();
                        }
                    }
                    modifiers.Cancel();
                    return;
                }
            }
        }
    }
}