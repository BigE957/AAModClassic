using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossFeudalFungus.Accessories;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossTruffleToad.Accessories;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items._BossMushroomMonarch.Accessories;
using AAModClassic._Unreleased.Content.Parthenan.__Hardmode.Items._BossTechnoTruffle.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content._Tinker.__Hardmode.Accessories
{
    public class MadnessTruffle : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Madness Truffle");
            /* Tooltip.SetDefault(@"'You know what? Just don't put it anywhere near your mouth.'"); */
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
            Item.expert = true;
            Item.defense = 8;
        }

        /*
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.wingTime > 0)
            {
                player.wingTime += 3;
            }
        }
        */

        public override void RegisterEquipEffects()
        {
            AddEffect(new JumpStatsEffect(3.6f, 25, true));
            AddEffect<FallDamageImmunityEffect>();
            AddEffect(new MaxLifeEffect(50));
            AddEffect(new MaxManaEffect(50));
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HeartyTruffle>(), 1);
            recipe.AddIngredient(ModContent.ItemType<GlowingTruffle>(), 1);
            recipe.AddIngredient(ModContent.ItemType<MetallicTruffle>(), 1);
            recipe.AddIngredient(ModContent.ItemType<TruffleLegs>(), 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}