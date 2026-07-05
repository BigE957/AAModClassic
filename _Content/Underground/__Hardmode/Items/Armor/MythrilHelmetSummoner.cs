using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Underground.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class MythrilHelmetSummoner : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Mythril";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mythril Face Paint");
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
            return body.type == ItemID.MythrilChainmail && legs.type == ItemID.MythrilGreaves;
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Summon) += 0.26f;
            AddEffect(new MaxManaEffect(60));

            AddSetEffect(new MaxMinionSlotEffect(3));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MythrilBar, 6);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(TileID.BewitchingTable);
            recipe.Register();
        }
    }
}