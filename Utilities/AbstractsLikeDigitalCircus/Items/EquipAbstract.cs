using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Accessories;
using AAModClassic._Content.Mire.Buffs;
using Humanizer;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items
{
    public abstract class EquipAbstract : BaseAAItem
    {
        public DamageClassMap damageMap = new();
        public List<EquipmentEffectData> effectMap = new();

        #region the sealing
        public sealed override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            Clear();
            RegisterEquipStats();

            for (int i = 0; i < DamageClassLoader.DamageClassCount; i++)
            {
                DamageClass currentClass = DamageClassLoader.GetDamageClass(i);
                player.GetDamage(currentClass) = player.GetDamage(currentClass).CombineWith(damageMap.GetDamage(currentClass));
                player.GetCritChance(currentClass) += damageMap.GetCritChance(currentClass);
            }

            foreach (EquipmentEffectData effect in effectMap)
            {
                effect.DoEffect(player);
            }
        }

        public sealed override void UpdateVanity(Player player)
        {
            base.UpdateVanity(player);
            Clear();
            RegisterAccVanity();

            foreach (EquipmentEffectData effect in effectMap)
            {
                effect.DoEffect(player);
            }
        }

        public sealed override void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);


        }
        #endregion

        public virtual void RegisterEquipStats()
        {

        }

        public virtual void RegisterAccVanity()
        {

        }

        public virtual void RegisterArmorSetStats()
        {

        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            base.ModifyTooltips(list);
            Clear();
            RegisterEquipStats();

            const string rootPath = "Mods.AAModClassic.EquipStats";
            const string statModifierPath = "ClassGlobalStats.StatModifier";

            var line = new TooltipLine(Mod, "Dummy", "Don't add this!");
            for (int i = 0; i < DamageClassLoader.DamageClassCount; i++)
            {
                DamageClass currentClass = DamageClassLoader.GetDamageClass(i);
                if (damageMap != new DamageClassMap())
                {
                    bool critAndDamageAdditiveAreSame = Math.Round(damageMap.GetDamage(currentClass).Additive - 1, 2) == Math.Round(damageMap.GetCritChance(currentClass), 2);

                    if (damageMap.GetDamage(currentClass) != StatModifier.Default)
                    {
                        StatModifierUtils.HandleStatModifierTooltips(Mod, list, currentClass, damageMap.GetDamage(currentClass), StatModifierUtils.StatModifierInputType.Damage, critAndDamageAdditiveAreSame);
                    }
                    if (damageMap.GetCritChance(currentClass) != 0 && !critAndDamageAdditiveAreSame)
                    {

                    }
                    if (damageMap.GetAttackSpeed(currentClass) != 1)
                    {

                    }
                    if (damageMap.GetArmorPenetration(currentClass) != 0)
                    {
                        string increaseOrDecrease = "Increased";
                        if (damageMap.GetArmorPenetration(currentClass) < 0)
                            increaseOrDecrease = "Decreased";

                        string extraSpaceForGeneric = " ";
                        if (currentClass == DamageClass.Generic)
                            extraSpaceForGeneric = "";

                        string adlibPath = Language.GetTextValue($"{rootPath}.ClassGlobalStats.ArmorPenetration");
                        string increaseOrDecreasePath = Language.GetTextValue($"{rootPath}.{statModifierPath}.{increaseOrDecrease}");
                        string damageTypePath = Language.GetTextValue($"{rootPath}.ClassGlobalStats.{currentClass.Name}");

                        string finalTooltipText = Language.GetOrRegister(adlibPath).Format(increaseOrDecreasePath, damageTypePath, extraSpaceForGeneric, Math.Abs(damageMap.GetArmorPenetration(currentClass)));
                        finalTooltipText = finalTooltipText.FirstCharToUpper();
                        line = new TooltipLine(Mod, "ArmorPenetrationLine", finalTooltipText);
                        int index = list.FindIndex(x => x.Name == "Tooltip0");
                        if (index != -1)
                            list.Insert(index, line);
                    }
                    if (damageMap.GetKnockback(currentClass) != StatModifier.Default)
                    {
                        StatModifierUtils.HandleStatModifierTooltips(Mod, list, currentClass, damageMap.GetDamage(currentClass), StatModifierUtils.StatModifierInputType.Knockback, critAndDamageAdditiveAreSame);
                    }
                }
            }

            foreach (EquipmentEffectData effect in effectMap)
            {
                line = new TooltipLine(Mod, effect.Name, effect.GetDescription());
                int index = list.FindIndex(x => x.Name == "Tooltip0");
                if (index != -1)
                    list.Insert(index, line);
            }
        }

        public void AddEffect<T>() where T : EquipmentEffectData, new()
        {
            effectMap.Add(new T());
        }

        public void AddEffect(EquipmentEffectData data)
        {
            effectMap.Add(data);
        }

        public void Clear()
        {
            damageMap.Clear();
            effectMap.Clear();
        }
    }

    public class DamageClassMap : DamageClassData
    {
        private DamageClassData[] _data;

        public DamageClassMap()
        {
            Clear();
        }

        public void Clear()
        {
            _data = new DamageClassData[DamageClassLoader.DamageClassCount];

            for (int i = 0; i < _data.Length; i++)
            {
                _data[i] = new DamageClassData();
            }
        }

        #region gets/sets
        /// <summary>
        /// Gets the damage modifier for this damage type on this player.
        /// This returns a reference, and as such, you can freely modify this method's return value with operators.
        /// </summary>
        public ref StatModifier GetDamage<T>() where T : DamageClass => ref GetDamage(ModContent.GetInstance<T>());

        /// <summary>
        /// Gets the damage modifier for this damage type on this player.
        /// This returns a reference, and as such, you can freely modify this method's return value with operators.
        /// </summary>
        public ref StatModifier GetDamage(DamageClass damageClass) => ref _data[damageClass.Type].damage;


        /// <summary>
        /// Gets the crit chance modifier for this damage type on this player.
        /// This returns a reference, and as such, you can freely modify this method's return value with operators.
        /// <para/> Note that crit values are percentage values ranging from 0 to 100, unlike damage multipliers. Adding 4, for example, would add 4% to the crit chance.
        /// </summary>
        public ref float GetCritChance<T>() where T : DamageClass => ref GetCritChance(ModContent.GetInstance<T>());

        /// <summary>
        /// Gets the crit chance modifier for this damage type on this player.
        /// This returns a reference, and as such, you can freely modify this method's return value with operators.
        /// <para/> Note that crit values are percentage values ranging from 0 to 100, unlike damage multipliers. Adding 4, for example, would add 4% to the crit chance.
        /// </summary>
        public ref float GetCritChance(DamageClass damageClass) => ref _data[damageClass.Type].critChance;

        /// <summary>
        /// Gets the attack speed modifier for this damage type on this player.
        /// This returns a reference, and as such, you can freely modify this method's return values with operators.
        /// Setting this such that it results in zero or a negative value will throw an exception.
        /// NOTE: Due to the nature of attack speed modifiers, modifications to Flat will do nothing for this modifier.
        /// <para/> Note that attack speed is a multiplier. Adding 0.15f, for example, would add 15% to the attack speed stat.
        /// </summary>
        public ref float GetAttackSpeed<T>() where T : DamageClass => ref GetAttackSpeed(ModContent.GetInstance<T>());

        /// <summary>
        /// Gets the attack speed modifier for this damage type on this player.
        /// This returns a reference, and as such, you can freely modify this method's return values with operators.
        /// <para/> Note that attack speed is a multiplier. Adding 0.15f, for example, would add 15% to the attack speed stat.
        /// </summary>
        public ref float GetAttackSpeed(DamageClass damageClass) => ref _data[damageClass.Type].attackSpeed;

        /// <summary>
        /// Gets the armor penetration modifier for this damage type on this player.
        /// This returns a reference, and as such, you can freely modify this method's return value with operators.
        /// <para/> Note that armor penetration value are typically whole numbers. Adding 5, for example, would add 5 to the armor penetration stat, similar to the Shark Tooth Necklace accessory.
        /// </summary>
        public ref float GetArmorPenetration<T>() where T : DamageClass => ref GetArmorPenetration(ModContent.GetInstance<T>());

        /// <summary>
        /// Gets the armor penetration modifier for this damage type on this player.
        /// This returns a reference, and as such, you can freely modify this method's return value with operators.
        /// <para/> Note that armor penetration value are typically whole numbers. Adding 5, for example, would add 5 to the armor penetration stat, similar to the Shark Tooth Necklace accessory.
        /// </summary>
        public ref float GetArmorPenetration(DamageClass damageClass) => ref _data[damageClass.Type].armorPen;

        /// <summary>
        /// Gets the knockback modifier for this damage type on this player.
        /// This returns a reference, and as such, you can freely modify this method's return value with operators.
        /// <para/> Note that knockback values are multipliers. Adding 0.12f, for example, would add 12% to the knockback stat.
        /// </summary>
        public ref StatModifier GetKnockback<T>() where T : DamageClass => ref GetKnockback(ModContent.GetInstance<T>());

        /// <summary>
        /// Gets the knockback modifier for this damage type on this player.
        /// This returns a reference, and as such, you can freely modify this method's return value with operators.
        /// <para/> Note that knockback values are multipliers. Adding 0.12f, for example, would add 12% to the knockback stat.
        /// </summary>
        public ref StatModifier GetKnockback(DamageClass damageClass) => ref _data[damageClass.Type].knockback;
        #endregion
    }

    public abstract class EquipmentEffectData
    {
        public string Name { get; }
        public string Description { get; }

        protected EquipmentEffectData()
        {
            Name = GetType().Name;
            Description = $"Mods.AAModClassic.EquipStats.Effect.{Name}";
        }

        public virtual void DoEffect(Player player)
        {

        }

        public virtual string GetDescription() => Language.GetTextValue(Description);
    }

    public interface IGlobalEffect { }

    public class ManaFlowerEffect : EquipmentEffectData, IGlobalEffect
    {
        public override void DoEffect(Player player)
        {
            player.manaFlower = true;
        }
    }

    public class CrimsonArmorSetBonusEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.crimsonRegen = true;
        }
    }

    public class EnduranceEffect(float amount) : EquipmentEffectData
    {
        private readonly float Amount = amount;
        public override void DoEffect(Player player)
        {
            player.endurance += Amount;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(Amount * 100);
    }

    public class MaxLifeEffect(int amount) : EquipmentEffectData
    {
        private readonly int Amount = amount;
        public override void DoEffect(Player player)
        {
            player.statLifeMax2 += Amount;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(Amount);
    }

    public class MovementSpeedEffect(float amount) : EquipmentEffectData
    {
        private readonly float Amount = amount;
        public override void DoEffect(Player player)
        {
            player.moveSpeed += Amount;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(Amount * 100);
    }

    public class MaxRunSpeedEffect(float amount) : EquipmentEffectData
    {
        private readonly float Amount = amount;
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += Amount;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(Amount * 100);
    }

    public class WingTimeMaxEffect(int amount) : EquipmentEffectData
    {
        private readonly int Amount = amount;
        public override void DoEffect(Player player)
        {
            player.wingTimeMax = Amount;
        }
    }

    public class FallDamageImmunityEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.noFallDmg = true;
        }
    }

    public class BlackBeltEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.blackBelt = true;
        }
    }

    public class MasterNinjaMobilityEffect(bool doDash, bool doubleSpikedBoots) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (doDash)
                player.dashType = 1;

            if (doubleSpikedBoots)
                player.spikedBoots = 2;
            else
                player.spikedBoots = 1;
        }

        public override string GetDescription()
        {
            string text = doubleSpikedBoots == true ? Description + ".ShoeSpikes2" : Description + ".ShoeSpikes1";
            text = Language.GetTextValue(text);
            if (doDash)
                text += Language.GetTextValue(Description + ".Tabi");

            return text;
        }
    }

    public class SolarArmorSetDashEffect() : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.dashType = 3;
        }
    }

    public class AttacksInflictDebuffEffect(params (int buffID, int time)[] debuffData) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<AttacksInflictDebuffPlayer>().effect = true;
            foreach (var debuffDestuff in debuffData)
            {
                player.GetModPlayer<AttacksInflictDebuffPlayer>().debuffArray.Add(debuffDestuff.buffID);
                player.GetModPlayer<AttacksInflictDebuffPlayer>().debuffLengthArray.Add(debuffDestuff.time);
            }
        }

        public override string GetDescription()
        {
            List<int> debuffList = Main.LocalPlayer.GetModPlayer<AttacksInflictDebuffPlayer>().debuffArray;

            // idgaf
            string text = Language.GetTextValue(Description);
            for (int i = 0; i < debuffList.Count; i++)
            {
                int id = debuffList[i];
                string buffName = "REPORT THIS";

                if (id < BuffID.Count)
                    buffName = Lang.GetBuffName(id);
                else
                    buffName = ModContent.GetModBuff(id).DisplayName.ToString();

                if (i < BuffLoader.BuffCount)
                {
                    if (i != debuffList.Count - 1)
                    {
                        text += buffName;
                        if (i == debuffList.Count - 2)
                            text += " ";
                        else
                            text += ", ";
                    }
                    else if (debuffList.Count > 1)
                        text += "and " + buffName;
                }
            }

            return text;
        }
    }

    public class AttacksInflictDebuffPlayer : EquipEffectAbstract
    {
        public List<int> debuffArray;
        public List<int> debuffLengthArray;

        public override void ResetInfoAccessories()
        {
            debuffArray = new List<int>();
            debuffLengthArray = new List<int>();
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (effect)
            {
                for (int i = 0; i < debuffArray.Count; i++)
                    target.AddBuff(debuffArray[i], debuffLengthArray[i]);
            }
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (effect)
            {
                for (int i = 0; i < debuffArray.Count; i++)
                    target.AddBuff(debuffArray[i], debuffLengthArray[i]);
            }
        }
    }

    public class DebuffImmunityEffect(params int[] buffIDs) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            foreach (int i in buffIDs)
                player.buffImmune[i] = true;
        }

        public override string GetDescription()
        {
            // idgaf
            string text = Language.GetTextValue(Description);
            for (int i = 0; i < buffIDs.Length; i++)
            {
                int id = buffIDs[i];
                string buffName = "REPORT THIS";

                if (id < BuffID.Count)
                    buffName = Lang.GetBuffName(id);
                else
                    buffName = ModContent.GetModBuff(id).DisplayName.ToString();
     
                if (i < BuffLoader.BuffCount)
                {
                    if (i != buffIDs.Length - 1)
                    {
                        text += buffName;
                        if (i == buffIDs.Length - 2)
                            text += " ";
                        else
                            text += ", ";
                    }
                    else if (buffIDs.Length > 1)
                        text += "and " + buffName;
                }
            }

            return text;
        }
    }

    public class JumpStatsEffect(float jumpSpeed = 0, int jumpHeight = 0, bool autoJump = false) : EquipmentEffectData
    {
        private readonly float JumpSpeed = jumpSpeed;
        private readonly int JumpHeight = jumpHeight;
        private readonly bool AutoJump = autoJump;

        public override void DoEffect(Player player)
        {
            player.jumpSpeedBoost += JumpSpeed;
            Player.jumpHeight = JumpHeight;
            player.autoJump = AutoJump;
        }

        public override string GetDescription()
        {
            int stuffCount = 0;
            if (JumpSpeed != 0)
                stuffCount++;
            if (JumpHeight != 0)
                stuffCount++;
            if (AutoJump == true)
                stuffCount++;

            string jumpSpeedText = Language.GetTextValue(Description + ".JumpSpeed");
            string jumpHeightText = Language.GetTextValue(Description + ".JumpHeight");
            string autoJumpText = Language.GetTextValue(Description + ".AutoJump");

            // idgaf
            string text = "Increases ";
            if (JumpSpeed != 0)
                text += jumpSpeedText;
            else if (JumpHeight != 0)
                text += jumpHeightText;
            else if (AutoJump == true)
                text += autoJumpText;

            if (stuffCount == 2)
            {
                text += " and ";
                if (JumpHeight != 0)
                    text += jumpHeightText;
                else if (AutoJump == true)
                    text += autoJumpText;
            }
            else if (stuffCount > 2)
                text += $", {jumpHeightText} and {autoJumpText}";

            if (stuffCount == 1 && AutoJump == true)
                text = autoJumpText;

            text = text.FirstCharToUpper();
            return text;
        }
    }

    public static class StatModifierUtils
    {
        public enum StatModifierValueType
        {
            Base = 0,
            Additive = 1,
            Multiplicative = 2,
            Flat = 3
        }

        public enum StatModifierInputType
        {
            Damage = 0,
            Knockback = 1
        }

        public static void HandleStatModifierTooltips(Mod mod, List<TooltipLine> list, DamageClass currentClass, StatModifier input, StatModifierInputType inputType, bool doCritSameAsDamageThing = false)
        {
            var line = new TooltipLine(mod, "Dummy", "Don't add this!");

            if (input.Base != 0)
            {
                string stuff = GetStatModifierTextString(currentClass, input, StatModifierValueType.Base, inputType, 0);
                line = new TooltipLine(mod, stuff, stuff);
                int index = list.FindIndex(x => x.Name == "Tooltip0");
                if (index != -1)
                    list.Insert(index, line);
            }
            if (input.Additive != 1)
            {
                string stuff = GetStatModifierTextString(currentClass, input, StatModifierValueType.Additive, inputType, 1, doCritSameAsDamageThing);
                line = new TooltipLine(mod, stuff, stuff);
                int index = list.FindIndex(x => x.Name == "Tooltip0");
                if (index != -1)
                    list.Insert(index, line);
            }
            if (input.Multiplicative != 1)
            {
                string stuff = GetStatModifierTextString(currentClass, input, StatModifierValueType.Multiplicative, inputType, 1);
                line = new TooltipLine(mod, stuff, stuff);
                int index = list.FindIndex(x => x.Name == "Tooltip0");
                if (index != -1)
                    list.Insert(index, line);
            }
            if (input.Flat != 0)
            {
                string stuff = GetStatModifierTextString(currentClass, input, StatModifierValueType.Flat, inputType, 0);
                line = new TooltipLine(mod, stuff, stuff);
                int index = list.FindIndex(x => x.Name == "Tooltip0");
                if (index != -1)
                    list.Insert(index, line);
            }
        }

        public static string GetStatModifierTextString(DamageClass currentClass, StatModifier input, StatModifierValueType statType, StatModifierInputType inputType, int statIncreasedThreshold = 0, bool doCritSameAsDamageThing = false)
        {
            const string rootPath = "Mods.AAModClassic.EquipStats";
            const string statModifierPath = "ClassGlobalStats.StatModifier";

            // for loc adlibs
            // 0 is damage value
            // 1 is increased/increases or decreased/decreases
            // 2 is damage type 
            // 3 is damage or kb
            // 4 is space for not generic
            // 5 is "& crit" message if applicable (not on all strings!!)

            float damageToDisplay = 0;
            switch (statType)
            {
                case StatModifierValueType.Base:
                    damageToDisplay = input.Base;
                    break;
                case StatModifierValueType.Additive:
                    damageToDisplay = (int)((input.Additive - 1f) * 100f);
                    break;
                case StatModifierValueType.Multiplicative:
                    damageToDisplay = (int)((input.Multiplicative) * 100f);
                    break;
                case StatModifierValueType.Flat:
                    damageToDisplay = input.Flat;
                    break;
                default:
                    return "SOMETHING WENT TERRIBLY WRONG WITH THE TOOLTIPIFIER";
            }
            bool statIsIncreased = damageToDisplay > statIncreasedThreshold;

            string statModifierAdlib = "EverySingleClassExceptSummoner";
            string extraSpaceForGeneric = " ";
            string currentIncreaseDecreaseThing = "d";
            switch (inputType)
            {
                case StatModifierInputType.Damage:
                    if (currentClass == DamageClass.Generic)
                        extraSpaceForGeneric = "";
                    else if (currentClass == DamageClass.Summon)
                    {
                        statModifierAdlib = "Summoner";
                        currentIncreaseDecreaseThing = "s";
                    }
                    break;
                case StatModifierInputType.Knockback:
                    statModifierAdlib = "EverySingleClassExceptSummoner";
                    break;
                default:
                    return "SOMETHING WENT TERRIBLY WRONG WITH THE TOOLTIPIFIER";
            }

            string increaseOrDecreasePath = statIsIncreased ? $"{rootPath}.{statModifierPath}.Increase{currentIncreaseDecreaseThing}" : $"{rootPath}.{statModifierPath}.Decrease{currentIncreaseDecreaseThing}";
            string damageTypePath = $"{rootPath}.ClassGlobalStats.{currentClass.Name}";
            string damageOrKBPath = $"{rootPath}.{statModifierPath}.{Enum.GetName(typeof(StatModifierInputType), inputType)}";
            string andCritPath = doCritSameAsDamageThing ? $"{rootPath}.{statModifierPath}.CritSameAsDamageAdditive" : $"{rootPath}.Misc.Nothing";
            string currentDamageThingPath = $"{rootPath}.{statModifierPath}.Adlibs.{statModifierAdlib}.{Enum.GetName(typeof(StatModifierValueType), statType)}";

            string increaseOrDecreaseText = Language.GetTextValue(increaseOrDecreasePath);
            string damageTypeText = Language.GetTextValue(damageTypePath);
            string damageOrKBText = Language.GetTextValue(damageOrKBPath);
            string andCritText = Language.GetTextValue(andCritPath);

            string finalTooltipText = Language.GetOrRegister(currentDamageThingPath).Format(damageToDisplay, increaseOrDecreaseText, damageTypeText, extraSpaceForGeneric, damageOrKBText, andCritText);
            finalTooltipText = finalTooltipText.FirstCharToUpper();
            return finalTooltipText;
        }
    }
}
