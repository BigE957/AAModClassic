using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Underground.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class TitaniumHelmetSummoner : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Titanium";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Titanium Face Paint");
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 50000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 4;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.TitaniumBreastplate && legs.type == ItemID.TitaniumLeggings;
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Summon) += 0.27f;
            AddEffect(new MaxManaEffect(100));

            AddSetEffect(new MaxMinionSlotEffect(4));
            AddSetEffect<TitaniumHelmetSetEffect>();
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.TitaniumBar, 6);
                recipe.AddIngredient(ItemID.BottledWater, 1);
                recipe.AddTile(TileID.BewitchingTable);
                recipe.Register();
            }
        }
    }
}