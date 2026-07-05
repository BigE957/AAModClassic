using AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Chaos.Buffs;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Accessories;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Accessories;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic._Content.Terrarium.Buffs;
using AAModClassic.Globals;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Accessories
{
    public class Duality : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Duality");
            /* Tooltip.SetDefault(@"'Chaos flares from this ancient talisman'"); */
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 8));
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 34;
            Item.value = Item.sellPrice(5, 0, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.expert = true;
            Item.accessory = true;
            Item.defense = 8;
        }


        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.DarkMagenta.ToVector3() * 0.55f * Main.essScale);
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Generic) += .15f;
            damageMap.GetDamage(DamageClass.Default).Flat += 5;
            AddEffect<DualityChaosEffect>();
            AddEffect(new MovementSpeedEffect(2));
            AddEffect(new NaitokurosuNightEffect(0.5f));
            AddEffect(new EnduranceEffect(0.06f));
            AddEffect<DualityDefenseEffect>();
            AddEffect<TaiyangBaoleiImmunityEffect>();
            AddEffect(new BuffImmunityEffect(ModContent.BuffType<DragonFire_Buff>(), ModContent.BuffType<HydraToxin_Buff>(), ModContent.BuffType<Terrablaze_Buff>(), ModContent.BuffType<DiscordianInferno_Buff>()));
            AddEffect<LanternEffect>();
            AddEffect<AshProofVestEffect>();
            AddEffect<FallDamageImmunityEffect>();
            AddEffect<SolarArmorSetDashEffect>(); //TODO: replace with moddash... like thats ever gonna happen
            AddEffect(new MasterNinjaMobilityEffect(false, true));
            AddEffect<BlackBeltEffect>();
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased))
                AddEffect(new AttacksInflictBuffEffect(null, (ModContent.BuffType<DiscordianInferno_Buff>(), 300)));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TaiyangBaolei>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Naitokurosu>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ChaosSoul>(), 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }

    public class DualityChaosEffect : EquipmentEffectData
    {
        public const float DAMAGEBOOST = 0.15f;

        public override void DoEffect(Player player)
        {
            if (player.GetModPlayer<AAPlayer>().ZoneInferno || player.GetModPlayer<AAPlayer>().ZoneMire)
                player.GetDamage(DamageClass.Ranged) += DAMAGEBOOST;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(Math.Round(DAMAGEBOOST * 100, 0));
    }

    public class DualityDefenseEffect : EquipmentEffectData
    {
        public const int DEFENSEBOOST = 10;

        public override void DoEffect(Player player)
        {
            if (player.GetModPlayer<AAPlayer>().ZoneInferno)
                player.statDefense += DEFENSEBOOST;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(DEFENSEBOOST);
    }
}