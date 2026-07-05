using AAModClassic._Content.Dungeon.___PreHardmode.Items.Armor;
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
    public class TerraHelmetRanged : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Terra";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Visor");
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 34;
            Item.value = 90000;
            Item.rare = ItemRarityID.Lime;
            Item.defense = 6;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<TerraChestplate>() && legs.type == ModContent.ItemType<TerraLeggings>();
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Ranged) += 0.24f;
            AddEffect<AmmoCost75Effect>();
            AddEffect<HunterEffect>();
            AddEffect<NightOwlEffect>();

            setDamageMap.GetCritChance(DamageClass.Ranged) += 20;
            AddSetEffect(new AggroEffect(-5));
            AddSetEffect<TerraHelmetRangedSetEffect>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DeathlyHelmet>(), 1);
            recipe.AddIngredient(ModContent.ItemType<TerraPrism>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}