using AAModClassic._Content.Jungle.___PreHardmode.Items.Armor;
using AAModClassic._Content.Terrarium.__Hardmode.Items.Materials;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content.Terra.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class TerraHelmetMage : EquipAbstract, ILocalizedModType
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

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<TerraChestplate>() && legs.type == ModContent.ItemType<TerraLeggings>();
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Magic) += 0.17f;
            damageMap.GetCritChance(DamageClass.Magic) += 15;
            AddEffect(new MaxManaEffect(100));
            AddEffect(new ManaCostEffect(0.3f));

            AddSetEffect<ManaFlowerEffect>();
            AddSetEffect(new ManaCostMultiplierEffect(0.60f));
            AddSetEffect<TerraHelmetMageSetEffect>();
        }

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