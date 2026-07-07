using AAModClassic._Content.Hell.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content.Hell.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ImpHelmet : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Imp";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Imp Hood");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 7000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 4;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ImpChestplate>() && legs.type == ModContent.ItemType<ImpLeggings>();
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Summon) += 0.07f;
            AddEffect(new MaxMinionSlotEffect(1));

            AddSetEffect(new AttacksInflictBuffEffect(DamageClass.Summon, (BuffID.OnFire, 180)));
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<DevilSilk>(), 6);
                recipe.AddTile(TileID.Loom);
                recipe.Register();
            }
        }
    }
}