using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Hallow.__Hardmode.Items.Tiles.Functional;
using AAModClassic._Content.Mire.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.__Hardmode.Items.Materials;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.__Hardmode.Items.Accessories
{
    public class BlackLotusEmblem : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Black Lotus Emblem");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 50, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Magic) += 0.18f;
            AddEffect(new MovementSpeedEffect(0.15f));
            AddEffect(new MaxRunSpeedEffect(0.15f));
            AddEffect(new ManaCostEffect(-0.12f));
            AddEffect<BlackLotusEmblemEffect>();
            AddEffect<CelestialMagnetEffect>();
            AddEffect(new AttacksInflictBuffEffect(DamageClass.Magic, (ModContent.BuffType<Moonraze_Buff>(), 100)));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ManaFlower, 1);
            recipe.AddIngredient(ItemID.CelestialEmblem, 1);
            recipe.AddIngredient(ModContent.ItemType<BlackLotus>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ShadowBand>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SoulOfSpite>(), 10);
            recipe.AddTile(ModContent.TileType<HallowedAnvil_Tile>());
            recipe.Register();
        }

    }
}