using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Terrarium.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content.Terrarium.___PreHardmode.Items.Armor
{
    public class BiomiteHelmetSetEffect : EquipmentEffectData
    {
        public List<string> DumbBullshit = new List<string>();

        public override void DoEffect(Player player)
        {
            if (Main.dayTime)
            {
                player.statLifeMax2 += 20;
                DumbBullshit.Add(Language.GetTextValue($"{Description}.Day"));
            }
            else
            {
                player.statManaMax2 += 20;
                DumbBullshit.Add(Language.GetTextValue($"{Description}.Night"));
            }

            if (player.GetModPlayer<AAPlayer>().ZoneVoid)
            {
                player.detectCreature = true;
                DumbBullshit.Add(Language.GetTextValue($"{Description}.Void"));
            }

            if (player.GetModPlayer<AAPlayer>().ZoneInferno)
            {
                player.buffImmune[BuffID.OnFire] = true;
                DumbBullshit.Add(Language.GetTextValue($"{Description}.Inferno"));
            }

            if (player.GetModPlayer<AAPlayer>().ZoneMire)
            {
                player.buffImmune[BuffID.Poisoned] = true;
                DumbBullshit.Add(Language.GetTextValue($"{Description}.Mire"));
            }

            if (player.GetModPlayer<AAPlayer>().Terrarium)
            {
                player.statDefense += 5;
                DumbBullshit.Add(Language.GetTextValue($"{Description}.Terrarium"));
            }

            if (player.ZoneJungle)
            {
                player.manaRegenBonus += 3;
                DumbBullshit.Add(Language.GetTextValue($"{Description}.Jungle"));
            }

            if (player.ZoneSnow)
            {
                player.buffImmune[BuffID.Chilled] = true;
                DumbBullshit.Add(Language.GetTextValue($"{Description}.Snow"));
            }

            if (player.ZoneDesert)
            {
                player.buffImmune[BuffID.WindPushed] = true;
                DumbBullshit.Add(Language.GetTextValue($"{Description}.Desert"));
            }

            if (player.ZoneHallow)
            {
                player.buffImmune[BuffID.Slow] = true;
                player.lifeRegen += 3;
                DumbBullshit.Add(Language.GetTextValue($"{Description}.Hallow"));
            }

            if (player.ZoneCorrupt)
            {
                player.moveSpeed += .1f;
                player.GetModPlayer<AAPlayer>().MaxMovespeedboost += 0.1f;
                DumbBullshit.Add(Language.GetTextValue($"{Description}.Corruption"));
            }

            if (player.ZoneCrimson)
            {
                player.GetArmorPenetration(DamageClass.Generic) += 5;
                DumbBullshit.Add(Language.GetTextValue($"{Description}.Crimson"));
            }
        }

        public override string GetDescription()
        {
            string text = Language.GetTextValue($"{Description}.Default");

            foreach (string sentence in DumbBullshit)
                text += $"\n{sentence}";
            return text;
        }
    }
}