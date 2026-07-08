using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content._Tinker.__Hardmode.Items.Accessories
{
    [AutoloadEquip(EquipType.Back)]
    public class AnkhTablet : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ankh Tablet");
            /* Tooltip.SetDefault(@"Grants immunity to knockback and fire blocks
Grants immunity to most debuffs
+2 max minions"); */
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.AnkhShield);
            Item.shieldSlot = -1;
            AutoDefaults();
        }

        public override void RegisterEquipEffects()
        {
            AddEffect<AnkhShieldEffect>();
            AddEffect(new MaxMinionSlotEffect(2));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AnkhCharm);
            recipe.AddIngredient(ItemID.SolarTablet);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
