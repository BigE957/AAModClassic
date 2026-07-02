using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items._BossSagittarius.Accessories
{
    public class SagittariusShieldEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<SagittariusShieldPlayer>().effect = true;
        }
    }

    public class SagittariusShieldPlayer : EquipmentEffectPlayer
    {
        public int cooldown;

        public override void PostUpdate()
        {
            if (cooldown > 0)
            {
                cooldown--;
            }
            else
            {
                cooldown = 0;
            }

            if (Player.HasBuff<SagittariusShield_ShieldsUp>())
            {
                Player.GetModPlayer<AAPlayer>().RingRotation += .05f;
                Player.GetModPlayer<AAPlayer>().ShieldScale += .02f;
                if (Player.GetModPlayer<AAPlayer>().ShieldScale >= 1f)
                {
                    Player.GetModPlayer<AAPlayer>().ShieldScale = 1f;
                }
            }
            else
            {
                Player.GetModPlayer<AAPlayer>().ShieldScale -= .02f;
                if (Player.GetModPlayer<AAPlayer>().ShieldScale <= 0f)
                {
                    Player.GetModPlayer<AAPlayer>().ShieldScale = 0f;
                }
            }

            if (Player.GetModPlayer<AAPlayer>().ShieldScale > 0f)
            {
                Player.GetModPlayer<AAPlayer>().RingRotation += .05f;
            }

            if (Player.GetModPlayer<AAPlayer>().ShieldScale > 0)
            {
                Player.GetModPlayer<AAPlayer>().RingRotation += .05f;
            }
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (Player.HasBuff<SagittariusShield_ShieldsUp>())
            {
                modifiers.Cancel();
                return;
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (effect)
            {
                if (AAMod.AccessoryAbilityKey.JustPressed && cooldown == 0)
                {
                    Player.AddBuff(ModContent.BuffType<SagittariusShield_ShieldsUp>(), 300);
                    cooldown = 5400;
                }
            }
        }

        public override void UpdateLifeRegen()
        {
            if (effect)
            {
                if (Player.lifeRegen < 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0;
                Player.lifeRegen += 2;
            }
        }
    }

    public class SagittariusShieldItem : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            if (player.HasBuff<SagittariusShield_ShieldsUp>() && item.damage > 0)
            {
                return false;
            }
            return true;
        }
    }
}