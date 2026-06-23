using AAModClassic._Content.Jungle.___PreHardmode.Items.Armor;
using AAModClassic._Content.Terra.__Hardmode.Items.Armor;
using AAModClassic._Content.Terrarium.__Hardmode.Items.Materials;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content.Terra.__Hardmode.Items
{
    [AutoloadEquip(EquipType.Head)]
    public class TerraHelmetMageTest : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Terra";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Hood");
            /* Tooltip.SetDefault(@"Increases maximum mana by 100 and 30% reduced mana cost
            17% increased magic damage
            15% increased magic critical strike chance"); */
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 24;
            Item.value = 90000;
            Item.rare = ItemRarityID.Lime;
            Item.defense = 22;
        }

        public override void RegisterEquipStats()
        {
            base.RegisterEquipStats();

            damageMap.GetDamage(DamageClass.Magic) += 0.20f;
            damageMap.GetArmorPenetration(DamageClass.Magic) += 5;
            damageMap.GetArmorPenetration(DamageClass.Melee) -= 5;

            AddEffect(new EnduranceEffect(0.04f));
            AddEffect(new MovementSpeedEffect(0.55f));
            AddEffect(new MaxLifeEffect(75));
            AddEffect<ManaFlowerEffect>();
            AddEffect<CrimsonArmorRegenEffect>();
        }

        public override void RegisterArmorSetStats()
        {
            base.RegisterArmorSetStats();

            //GetDamage(DamageClass.Magic) += 0.60f;
            //player.manaFlower = true;
        }

        /*
        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 100;
            player.manaCost -= 0.3f;
            player.GetDamage(DamageClass.Magic) += 0.17f;
            player.GetCritChance(DamageClass.Magic) += 15;

            MaxMana = 100; // Increases max mana by 100
            ManaCost = -0.3f; // Reduces mana cost by 30%
            Damage(DamageClass.Magic) = 0.17f; // Increases magic damage by 17%
            Crit(DamageClass.Magic) = 0.15f; // Increases magic crit chance by 15%
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<TerraChestplate>() && legs.type == ModContent.ItemType<TerraLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = FilePathUtils.SetBonusPath<TerraHelmetMage>();

            player.manaFlower = true;
            player.manaCost *= 0.6f;
            player.GetModPlayer<TerraHelmetMagePlayer>().effect = true;
        }
        */

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TribalHelmet>(), 1);
            recipe.AddIngredient(ModContent.ItemType<TerraPrism>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}