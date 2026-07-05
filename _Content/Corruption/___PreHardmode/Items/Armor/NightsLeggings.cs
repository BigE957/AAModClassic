using AAModClassic._Content.Hell.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Corruption.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class NightsLeggings : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Nights";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Night's Greaves");
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = 90000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 7;
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetAttackSpeed(DamageClass.Melee) += 0.09f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ShadowGreaves, 1);
            recipe.AddIngredient(ItemID.JungleSpores, 6);
            recipe.AddIngredient(ItemID.Bone, 6);
            recipe.AddIngredient(ModContent.ItemType<DevilSilk>(), 6);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}