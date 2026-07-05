using AAModClassic._Content.Hell.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Corruption.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class NightsChestplate : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Nights";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Night's Plate");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.value = 90000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 8;
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetAttackSpeed(DamageClass.Melee) += 0.09f;
        }

        public override void AddRecipes()
        {
            { 
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ShadowScalemail, 1);
            recipe.AddIngredient(ItemID.JungleSpores, 8);
            recipe.AddIngredient(ItemID.Bone, 8);
            recipe.AddIngredient(ModContent.ItemType<DevilSilk>(), 8);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
            }
        }
    }
}