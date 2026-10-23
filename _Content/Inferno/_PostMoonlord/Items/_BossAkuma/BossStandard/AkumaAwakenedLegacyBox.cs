using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Accessories;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.BossStandard
{
    public class AkumaAwakenedLegacyBox : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.MusicBoxes";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Music Box (Alt Oni Akuma)");
            // Tooltip.SetDefault(@"Plays 'Dawn of the Dragon' by Tyeski");

            ItemID.Sets.CanGetPrefixes[Type] = false;
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<AkumaAwakenedBox>();
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<AkumaAwakenedLegacyBox_Tile>();
            Item.width = 24;
            Item.height = 24;
            Item.rare = ModContent.RarityType<AncientsRarity>();
            Item.value = 10000;
            Item.accessory = true;
        }
    }
}
