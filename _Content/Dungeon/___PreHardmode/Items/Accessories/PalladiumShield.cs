using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Dungeon.___PreHardmode.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class PalladiumShield : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
            Item.defense = 1;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect<KnockbackImmunityEffect>();
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Palladium Shield");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PalladiumBar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}