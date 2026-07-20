using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Accessories;
using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Mire.__Hardmode.Items.Materials;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content._Tinker.___PreHardmode.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOff, EquipType.HandsOn)]
    public class KappaMits : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect<ShadowBandUnofficialEffect>();
            AddEffect(new MasterNinjaMobilityEffect(false, true));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ShadowBand>());
            recipe.AddIngredient(ItemID.TigerClimbingGear);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.AddCondition(ConditionUtils.Unofficial);
            recipe.Register();
        }
    }
}