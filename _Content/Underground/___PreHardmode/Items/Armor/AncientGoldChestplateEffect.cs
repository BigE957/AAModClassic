using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Underground.___PreHardmode.Items.Armor
{
    public class AncientGoldChestplateEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<AncientGoldChestplatePlayer>().effect = true;
        }
    }

    public class AncientGoldChestplatePlayer : EquipmentEffectPlayer
    {

    }

    public class AncientGoldChestplateTile : GlobalTile
    {
        public override void Drop(int i, int j, int type)
        {
            if (Main.LocalPlayer.GetModPlayer<AncientGoldChestplatePlayer>().effect)
            {
                if (TileID.Sets.Conversion.Stone[type] && Main.rand.NextBool(50))
                {
                    Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 32, 32, ItemID.GoldCoin, 1, false, 0, false, false);
                }
            }
        }
    }
}