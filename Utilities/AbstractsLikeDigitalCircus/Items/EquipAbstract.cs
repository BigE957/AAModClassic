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

        #region the sealing
        public override sealed void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            Clear();
            RegisterEquipStats();

            // add all stats
            player.GetDamage(DamageClass.Magic) = player.GetDamage(DamageClass.Magic).CombineWith(damageMap.GetDamage(DamageClass.Magic));

            // add all equips
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
            // im not the hugest fan of this but w/e
            Clear();
            RegisterEquipStats();

            int number = (int)((damageMap.GetDamage(DamageClass.Magic).Additive - 1f) * 100f);
            var line = new TooltipLine(Mod, "DamageAdditiveMage", Language.GetOrRegister("Mods.AAModClassic.EquipStats.Damage.Additive.Mage").Format(number));
            list.Add(line);

            foreach (EquipmentEffectData effect in effectMap)
            {
                line = new TooltipLine(Mod, effect.Name, Language.GetOrRegister(effect.Description).ToString());
                list.Add(line);
            }
        }

        public void AddEffect<T>() where T : EquipmentEffectData, new()
        {
            effectMap.Add(new T());
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
}
