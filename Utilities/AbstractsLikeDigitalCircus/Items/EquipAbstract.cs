using AAModClassic._Content.Hell.___PreHardmode.NPCs.__Friendly;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Accessories;
using AAModClassic._Content.Mire.Buffs;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items
{
    public abstract class EquipAbstract : BaseAAItem
    {
        public DamageClassMap damageMap = new();
        public DamageClassMap setDamageMap = new();
        public List<EquipmentEffectData> effectMap = new();
        public List<EquipmentEffectData> setEffectMap = new();

        #region the sealing
        public sealed override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            Clear();
            RegisterEquipEffects();

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

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            Clear();
            RegisterInventoryEffects();

            foreach (EquipmentEffectData effect in effectMap)
            {
                effect.DoEffect(player);
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            Clear();

            foreach (EquipmentEffectData effect in effectMap)
            {
                effect.DoEffectReliantOnVanityToggle(player, hideVisual);
            }
        }

        public sealed override void UpdateVanity(Player player)
        {
            base.UpdateVanity(player);
            Clear();
            RegisterVanityEffects();

            foreach (EquipmentEffectData effect in effectMap)
            {
                effect.DoEffect(player);
            }
        }

        public sealed override void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);
            Clear();
            RegisterEquipEffects();

            for (int i = 0; i < DamageClassLoader.DamageClassCount; i++)
            {
                DamageClass currentClass = DamageClassLoader.GetDamageClass(i);
                player.GetDamage(currentClass) = player.GetDamage(currentClass).CombineWith(setDamageMap.GetDamage(currentClass));
                player.GetCritChance(currentClass) += setDamageMap.GetCritChance(currentClass);
            }

            foreach (EquipmentEffectData effect in setEffectMap)
            {
                effect.DoEffect(player);
            }

            StatModifierUtils.HandleSetBonusEffectsInItemDesc(Mod, ref player.setBonus, ref setDamageMap, ref setEffectMap);
        }
        #endregion

        public virtual void RegisterEquipEffects()
        {

        }

        public virtual void RegisterVanityEffects()
        {

        }

        public virtual void RegisterInventoryEffects()
        {

        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            base.ModifyTooltips(list);
            Clear();
            RegisterEquipEffects();

            const string rootPath = "Mods.AAModClassic.EquipStats";
            const string statModifierPath = "ClassGlobalStats.StatModifier";

            StatModifierUtils.HandleDamageClassStatsInItemDesc(Mod, ref list, ref damageMap);
            StatModifierUtils.HandleEffectsInItemDesc(Mod, ref list, ref effectMap);
        }

        public void AddEffect<T>() where T : EquipmentEffectData, new()
        {
            effectMap.Add(new T());
        }

        public void AddEffect(EquipmentEffectData data)
        {
            effectMap.Add(data);
        }

        public void AddSetEffect<T>() where T : EquipmentEffectData, new()
        {
            setEffectMap.Add(new T());
        }

        public void AddSetEffect(EquipmentEffectData data)
        {
            setEffectMap.Add(data);
        }

        public void Clear()
        {
            damageMap.Clear();
            setDamageMap.Clear();
            effectMap.Clear();
            setEffectMap.Clear();
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

        public virtual void DoEffectReliantOnVanityToggle(Player player, bool hideVisual)
        {

        }

        public virtual string GetDescription() => Language.GetTextValue(Description);
    }


    public class ManaFlowerEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.manaFlower = true;
        }
    }

    public class CrimsonHelmetSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.crimsonRegen = true;
        }
    }

    public class ChlorophyteHelmetSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.AddBuff(BuffID.LeafCrystal, 2);
        }
    }

    public class ManaRegenEffect(int amount) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.manaRegenBonus += amount;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(ChatUtils.IncreaseOrDecreaseText(amount, ChatUtils.IncreaseDecreaseTextType.IncreasesDecreases).FirstCharToUpper(), Math.Abs(amount));
    }

    public class LifeRegenEffect(int amount) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.lifeRegen += amount;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(ChatUtils.IncreaseOrDecreaseText(amount, ChatUtils.IncreaseDecreaseTextType.IncreasesDecreases).FirstCharToUpper(), Math.Abs(amount));
    }

    public class OutlinesAndShadowEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.armorEffectDrawOutlines = true;
            player.armorEffectDrawShadow = true;
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

    public class FishingPowerEffect(int amount) : EquipmentEffectData
    {
        private readonly int Amount = amount;
        public override void DoEffect(Player player)
        {
            player.fishingSkill += Amount;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(ChatUtils.IncreaseOrDecreaseText(Amount, ChatUtils.IncreaseDecreaseTextType.IncreasesDecreases), Math.Abs(Amount));
    }

    public class MaxLifeEffect(int amount) : EquipmentEffectData
    {
        private readonly int Amount = amount;
        public override void DoEffect(Player player)
        {
            player.statLifeMax2 += Amount;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(ChatUtils.IncreaseOrDecreaseText(amount, ChatUtils.IncreaseDecreaseTextType.IncreasesDecreases), Math.Abs(amount)).FirstCharToUpper();
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

    public class FlipperEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.accFlipper = true;
        }
    }

    public class HighTestFishingLineEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.accFishingLine = true;
        }
    }

    public class TackleBoxEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.accTackleBox = true;
        }
    }

    public class SonarEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.sonarPotion = true;
        }
    }

    public class FrostsparkBootsEffect(int flightType = 0, float runSpeed = 3f, bool skates = false) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.vanityRocketBoots = flightType;
            player.rocketBoots = flightType;
            player.accRunSpeed = runSpeed;
            player.iceSkate = skates;
        }

        public override string GetDescription()
        {
            List<string> nameList = new List<string>();
            if (flightType > 0)
                nameList.Add(Language.GetTextValue($"{Description}.Flight"));
            if (runSpeed > 3f)
                nameList.Add(Language.GetTextValue($"{Description}.Running"));
            if (skates)
                nameList.Add(Language.GetTextValue($"{Description}.IceSkates"));

            string text = Language.GetTextValue($"{Description}.Default") + ChatUtils.GetFormattedListOfStrings(nameList);

            return text;
        }
    }

    public class LavaWadersWaterWalkingEffect(bool lava = false) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (lava)
                player.waterWalk2 = true;
            else
                player.waterWalk = true;
        }

        public override string GetDescription()
        {
            List<string> nameList = new List<string>();
            nameList.Add(Language.GetTextValue($"{Description}.Water"));
            nameList.Add(Language.GetTextValue($"{Description}.Honey"));
            if (lava)
                nameList.Add(Language.GetTextValue($"{Description}.Lava"));

            string text = Language.GetTextValue($"{Description}.Default") + ChatUtils.GetFormattedListOfStrings(nameList, true);

            return text;
        }
    }

    public class UnlimitedBreathingUnderWaterForeverAndEverEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.breath = player.breathMax - 1;
        }
    }

    public class LavaWadersFireImmunityEffect(bool fireImmune = false, int lavaImmuneFrames = 0) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.fireWalk = fireImmune;
            player.lavaMax += lavaImmuneFrames;
        }

        public override string GetDescription()
        {
            List<string> nameList = new List<string>();
            if (fireImmune)
                nameList.Add(Language.GetTextValue($"{Description}.FireImmune"));
            if (lavaImmuneFrames > 0)
                nameList.Add(Language.GetTextValue($"{Description}.LavaImmune"));

            string text = Language.GetTextValue($"{Description}.Default") + ChatUtils.GetFormattedListOfStrings(nameList, true).FormatWith(lavaImmuneFrames / 60);

            return text;
        }
    }

    public class BlackBeltEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.blackBelt = true;
        }
    }

    public class KnockbackImmunityEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.noKnockback = true;
        }
    }

    public class PDAEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.accWatch = 3;
            player.accDepthMeter = 1;
            player.accCompass = 1;
            player.accFishFinder = true;
            player.accWeatherRadio = true;
            player.accCalendar = true;
            player.accThirdEye = true;
            player.accJarOfSouls = true;
            player.accCritterGuide = true;
            player.accStopwatch = true;
            player.accOreFinder = true;
            player.accDreamCatcher = true;
        }
    }

    // got lazy
    public class ArcticDivingGearEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.arcticDivingGear = true;
            player.accFlipper = true;
            player.accDivingHelm = true;
            player.iceSkate = true;
            if (player.wet)
                Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.2f, 0.8f, 0.9f);
        }
    }

    public class DrawShadowLokisEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.armorEffectDrawShadowLokis = true;
        }
    }

    public class MaxManaEffect(int amount) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.statManaMax2 += amount;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(ChatUtils.IncreaseOrDecreaseText(amount, ChatUtils.IncreaseDecreaseTextType.IncreasesDecreases), Math.Abs(amount)).FirstCharToUpper();
    }

    public class DefenseEffect(int amount) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.statDefense += amount;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(amount);
    }

    public class ObsidianRoseEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.lavaRose = true;
        }
    }

    public class DiscountCardEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.discountAvailable = true;
        }
    }

    public class GoldRingEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.goldRing = true;
        }
    }

    public class LuckyCoinEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.hasLuckyCoin = true;
        }
    }

    public class ManaCostEffect(float amount) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.manaCost += amount;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(Math.Abs(amount * 100), ChatUtils.IncreaseOrDecreaseText(amount, reduced: true));
    }

    public class ManaCostMultiplierEffect(float amount) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.manaCost *= amount;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(amount);
    }

    public class MaxSentrySlotEffect(int amount) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.maxTurrets += amount;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(ChatUtils.IncreaseOrDecreaseText(amount, ChatUtils.IncreaseDecreaseTextType.IncreaseDecrease), Math.Abs(amount));
    }

    public class MaxMinionSlotEffect(int amount) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.maxMinions += amount;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(ChatUtils.IncreaseOrDecreaseText(amount, ChatUtils.IncreaseDecreaseTextType.IncreaseDecrease), Math.Abs(amount));
    }

    public class ShieldOfCthulhuDashEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.dashType = 2;
        }
    }

    public class AggroEffect(int amount) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.aggro += amount;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(ChatUtils.IncreaseOrDecreaseText(amount, ChatUtils.IncreaseDecreaseTextType.MoreLess));
    }

    public class MiningSpeedEffect(float amount) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.pickSpeed -= amount;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(ChatUtils.IncreaseOrDecreaseText(amount * -1, ChatUtils.IncreaseDecreaseTextType.IncreasesDecreases), Math.Abs(amount * 100));
    }

    public class AmmoCost75Effect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.ammoCost75 = true;
        }
    }

    public class AmmoCost80Effect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.ammoCost80 = true;
        }
    }

    public class PanicNecklaceEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.panic = true;
        }
    }

    public class HunterEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.detectCreature = true;
        }
    }

    public class DangersenseEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.dangerSense = true;
        }
    }

    public class CelestialMagnetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.manaMagnet = true;
        }
    }

    public class NightOwlEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.nightVision = true;
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

    public class SolarArmorSetDashEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.dashType = 3;
        }
    }

    public class ArchitectGizmoPackEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.autoPaint = true;
            player.pickSpeed -= 1f;
            player.tileSpeed += 3f;
            player.wallSpeed += 3f;
            if (player.whoAmI == Main.myPlayer)
            {
                Player.tileRangeX += 6;
                Player.tileRangeY += 4;
            }
        }
    }

    public class IgnoreWaterEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.ignoreWater = true;
        }
    }

    public class PhilosophersStoneEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.pStone = true;
        }
    }

    public class GillsEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.gills = true;
        }
    }

    public class MagmaStoneEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.magmaStone = true;
        }
    }

    public class SpelunkerEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.findTreasure = true;
        }
    }

    public class OrichalcumHelmetSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.onHitPetal = true;
        }
    }

    public class TitaniumHelmetSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.onHitTitaniumStorm = true;
        }
    }

    public class PalladiumHelmetSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.palladiumRegen = true;
        }
    }

    public class HallowedHelmetSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.onHitDodge = true;
        }
    }

    public class EmitLightFromPlayerEffect(float r, float g, float b) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            Lighting.AddLight((int)player.Center.X, (int)player.Center.Y, r, g, b);
        }
    }

    public class MoonCharmEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.wolfAcc = true;
        }

        public override void DoEffectReliantOnVanityToggle(Player player, bool hideVisual)
        {
            if (hideVisual)
                player.hideWolf = true;
        }
    }

    public class NeptunesShellEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.accMerman = true;
        }

        public override void DoEffectReliantOnVanityToggle(Player player, bool hideVisual)
        {
            if (hideVisual)
                player.hideMerman = true;
        }
    }

    public class AttacksInflictBuffEffect(DamageClass damageClass = null, params (int buffID, int time)[] debuffData) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<AttacksInflictBuffPlayer>().effect = true;
            player.GetModPlayer<AttacksInflictBuffPlayer>().damageToDoEffectOn = damageClass;

            if (player.GetModPlayer<AttacksInflictBuffPlayer>().debuffArray != null)
            {
                foreach (var debuffDestuff in debuffData)
                {
                    player.GetModPlayer<AttacksInflictBuffPlayer>().debuffArray.Add(debuffDestuff.buffID);
                    player.GetModPlayer<AttacksInflictBuffPlayer>().debuffLengthArray.Add(debuffDestuff.time);
                }
            }
        }

        public override string GetDescription()
        {
            List<int> debuffList = new List<int>();
            foreach (var debuffDestuff in debuffData)
                debuffList.Add(debuffDestuff.buffID);

            string text = Language.GetTextValue(Description).FormatWith("", "");
            if (damageClass != null)
                text = Language.GetTextValue(Description).FormatWith(ChatUtils.GetDamageTypeName(damageClass), " ");

            // idgaf
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
                    else
                        text += buffName;
                }
            }

            return text;
        }
    }

    public class AttacksInflictBuffPlayer : EquipmentEffectPlayer
    {
        public List<int> debuffArray;
        public List<int> debuffLengthArray;
        public DamageClass damageToDoEffectOn;

        public override void ResetInfoAccessories()
        {
            debuffArray = new List<int>();
            debuffLengthArray = new List<int>();
            damageToDoEffectOn = null;
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (effect && (damageToDoEffectOn == null || damageToDoEffectOn == hit.DamageType))
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

    public class BuffImmunityEffect(params int[] buffIDs) : EquipmentEffectData
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
                    else
                        text += buffName;
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

        public static void HandleDamageClassStatsInItemDesc(Mod mod, ref List<TooltipLine> list, ref DamageClassMap damageMap)
        {
            const string rootPath = "Mods.AAModClassic.EquipStats";
            const string statModifierPath = "ClassGlobalStats.StatModifier";

            var line = new TooltipLine(mod, "Dummy", "Don't add this!");

            for (int i = 0; i < DamageClassLoader.DamageClassCount; i++)
            {
                DamageClass currentClass = DamageClassLoader.GetDamageClass(i);
                if (damageMap != new DamageClassMap())
                {
                    bool critAndDamageAdditiveAreSame = Math.Round(damageMap.GetDamage(currentClass).Additive - 1, 2) == Math.Round(damageMap.GetCritChance(currentClass), 2);

                    if (damageMap.GetDamage(currentClass) != StatModifier.Default)
                    {
                        HandleStatModifierTooltips(mod, list, currentClass, damageMap.GetDamage(currentClass), StatModifierInputType.Damage, critAndDamageAdditiveAreSame);
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
                        line = new TooltipLine(mod, "ArmorPenetrationLine", finalTooltipText);
                        int index = list.FindIndex(x => x.Name == "Tooltip0");
                        if (index != -1)
                            list.Insert(index, line);
                    }
                    if (damageMap.GetKnockback(currentClass) != StatModifier.Default)
                    {
                        HandleStatModifierTooltips(mod, list, currentClass, damageMap.GetDamage(currentClass), StatModifierInputType.Knockback, critAndDamageAdditiveAreSame);
                    }
                }
            }
        }

        public static void HandleEffectsInItemDesc(Mod mod, ref List<TooltipLine> list, ref List<EquipmentEffectData> effectMap)
        {
            var line = new TooltipLine(mod, "Dummy", "Don't add this!");

            foreach (EquipmentEffectData effect in effectMap)
            {
                line = new TooltipLine(mod, effect.Name, effect.GetDescription());
                int index = list.FindIndex(x => x.Name == "Tooltip0");
                if (index != -1 && effect.GetDescription() != "")
                    list.Insert(index, line);
            }
        }

        public static void HandleSetBonusEffectsInItemDesc(Mod mod, ref string setBonus, ref DamageClassMap setDamageMap, ref List<EquipmentEffectData> setEffectMap)
        {
            List<TooltipLine> list = new List<TooltipLine>();
            list.Add(new TooltipLine(mod, "Tooltip0", "remove this"));

            HandleDamageClassStatsInItemDesc(mod, ref list, ref setDamageMap);
            HandleEffectsInItemDesc(mod, ref list, ref setEffectMap);

            string listHack = "";
            for (int i = 0; i < list.Count; i++)
            {
                TooltipLine line = list[i];
                if (line.Name != "Tooltip0")
                {
                    listHack += line.Text;
                    if (i < list.Count - 2)
                        listHack += "\n";
                }
            }

            setBonus = string.Join("\n", listHack);
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
