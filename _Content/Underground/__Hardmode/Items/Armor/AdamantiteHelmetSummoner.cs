using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Underground.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class AdamantiteHelmetSummoner : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Adamantite";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Adamantite Face Paint");
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 50000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 3;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.AdamantiteBreastplate && legs.type == ItemID.AdamantiteLeggings;
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Summon) += 0.3f;
            AddEffect(new MaxManaEffect(80));

            AddSetEffect(new MaxMinionSlotEffect(4));
            AddSetEffect<OutlinesAndShadowEffect>();
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.AdamantiteBar, 6);
                recipe.AddIngredient(ItemID.BottledWater, 1);
                recipe.AddTile(TileID.BewitchingTable);
                recipe.Register();
            }
        }
    }
}