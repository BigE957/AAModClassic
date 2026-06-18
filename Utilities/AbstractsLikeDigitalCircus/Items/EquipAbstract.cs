using Humanizer;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items
{
    public abstract class EquipAbstract : BaseAAItem
    {
        public DamageClassMap damageMap = new();
        public List<EquipmentEffectData> effectMap = new();
        private static readonly Dictionary<Type, EquipmentEffectData> _effectCache = new();

        #region the sealing
        public override sealed void UpdateEquip(Player player)
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

        public override sealed void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);


        }
        #endregion

        public virtual void RegisterEquipStats()
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
                        list.Add(line);
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
                list.Add(line);
            }
        }

        public void AddEffect<T>() where T : EquipmentEffectData, new()
        {
            if (!_effectCache.TryGetValue(typeof(T), out var effect))
            {
                effect = new T();
                _effectCache[typeof(T)] = effect;
            }
            effectMap.Add(effect);
        }

        public void AddEffect(EquipmentEffectData data)
        {
            if (!_effectCache.TryGetValue(data.GetType(), out var effect))
            {
                effect = data;
                _effectCache[data.GetType()] = effect;
            }
            effectMap.Add(effect);
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

    public class ManaFlower : EquipmentEffectData 
    {
        public override void DoEffect(Player player)
        {
            player.manaFlower = true;
        }
    }

    public class CrimsonArmorRegen : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.crimsonRegen = true;
        }
    }

    public class Endurance(float amount) : EquipmentEffectData
    {
        private readonly float Amount = amount;
        public override void DoEffect(Player player)
        {
            player.endurance += Amount;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith((Amount * 100));
    }

    public class MaxLife(int amount) : EquipmentEffectData
    {
        private readonly int Amount = amount;
        public override void DoEffect(Player player)
        {
            player.statLifeMax2 += Amount;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(Amount);
    }

    public class MovementSpeed(float amount) : EquipmentEffectData
    {
        private readonly float Amount = amount;
        public override void DoEffect(Player player)
        {
            player.endurance += Amount;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith((Amount * 100));
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
                list.Add(line);
            }
            if (input.Additive != 1)
            {
                string stuff = GetStatModifierTextString(currentClass, input, StatModifierValueType.Additive, inputType, 1, doCritSameAsDamageThing);
                line = new TooltipLine(mod, stuff, stuff);
                list.Add(line);
            }
            if (input.Multiplicative != 1)
            {
                string stuff = GetStatModifierTextString(currentClass, input, StatModifierValueType.Multiplicative, inputType, 1);
                line = new TooltipLine(mod, stuff, stuff);
                list.Add(line);
            }
            if (input.Flat != 0)
            {
                string stuff = GetStatModifierTextString(currentClass, input, StatModifierValueType.Flat, inputType, 0);
                line = new TooltipLine(mod, stuff, stuff);
                list.Add(line);
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
