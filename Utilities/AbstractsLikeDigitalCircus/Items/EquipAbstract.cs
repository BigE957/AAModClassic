using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items
{
    public abstract class EquipAbstract : BaseAAItem
    {
        #region stat hell
        private DamageClassData[] damageDataForItem;

        internal void ResetDamageClassData()
        {
            damageDataForItem = new DamageClassData[DamageClassLoader.DamageClassCount];

            for (int i = 0; i < damageDataForItem.Length; i++)
            {
                damageDataForItem[i] = new DamageClassData();
            }
        }

        /// <summary>
        /// Gets the damage modifier for this damage type on this player.
        /// This returns a reference, and as such, you can freely modify this method's return value with operators.
        /// </summary>
        public ref StatModifier GetDamage<T>() where T : DamageClass => ref GetDamage(ModContent.GetInstance<T>());

        /// <summary>
        /// Gets the damage modifier for this damage type on this player.
        /// This returns a reference, and as such, you can freely modify this method's return value with operators.
        /// </summary>
        public ref StatModifier GetDamage(DamageClass damageClass) => ref damageDataForItem[damageClass.Type].damage;
        #endregion

        #region the sealing
        public override sealed void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);

            ResetDamageClassData();
            RegisterEquipStats();

            player.GetDamage(DamageClass.Magic) = player.GetDamage(DamageClass.Magic).CombineWith(GetDamage(DamageClass.Magic));
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

            int number = (int)((damageDataForItem[DamageClass.Magic.Type].damage.Additive - 1f) * 100f);
            var line = new TooltipLine(Mod, "DamageAdditiveMage", Language.GetOrRegister("Mods.AAModClassic.EquipStats.Damage.Mage").Format(number));
            list.Add(line);
        }
    }
}
