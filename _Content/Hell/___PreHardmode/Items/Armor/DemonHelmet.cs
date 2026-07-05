using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content.Hell.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class DemonHelmet : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Demon";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Demon Cowl");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 20;
            Item.value = 9000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 6;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<DemonChestplate>() && legs.type == ModContent.ItemType<DemonLeggings>();
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Summon) += 0.09f;

            AddSetEffect(new AttacksInflictBuffEffect(DamageClass.Summon, (BuffID.OnFire, 180)));
            AddSetEffect<DemonHelmetSetEffect>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ImpHelmet>(), 1);
            recipe.AddIngredient(ItemID.Bone, 5);
            recipe.AddIngredient(ItemID.JungleSpores, 5);
            recipe.AddRecipeGroup("AAModClassic:EvilMaterial", 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}