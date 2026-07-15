using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.Consumables
{
    public abstract class CrateAbstract : BaseAAItem
    {
        public virtual int Tile => TileID.FishingCrate;
        public virtual bool Hardmode => false;
        public virtual int? ShimmerInto => null;
        public virtual IItemDropRule[] TopLoot => null;
        public virtual IItemDropRule[] BottomLoot => null;

        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Right click to open");

            Item.ResearchUnlockCount = 5;
            ItemID.Sets.IsFishingCrate[Type] = true;
            if (ShimmerInto != null)
                ItemID.Sets.ShimmerTransformToItem[Type] = (int)ShimmerInto;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(Tile);
            Item.width = Item.height = 32;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.Crates;
        }

        public override bool CanRightClick() => true;

        public override void ModifyItemLoot(ItemLoot itemLoot) => itemLoot.RegisterBiomeCrateDrops(Hardmode, TopLoot, BottomLoot);
    }
}
