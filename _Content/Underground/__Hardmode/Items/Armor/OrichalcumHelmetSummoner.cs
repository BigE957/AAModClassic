using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Underground.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class OrichalcumHelmetSummoner : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Orichalcum";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Oricalcum Face Paint");
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 50000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 2;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.OrichalcumBreastplate && legs.type == ItemID.OrichalcumLeggings;
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Summon) += 0.22f;
            AddEffect(new MaxManaEffect(80));

            AddSetEffect(new MaxMinionSlotEffect(2));
            AddSetEffect<OrichalcumHelmetSetEffect>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.OrichalcumBar, 6);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(TileID.BewitchingTable);
            recipe.Register();
        }
    }
}