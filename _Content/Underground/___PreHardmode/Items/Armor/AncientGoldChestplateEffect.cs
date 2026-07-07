using AAModClassic._Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items.Armor
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
            if (Main.player[Main.myPlayer].GetModPlayer<AncientGoldChestplatePlayer>().effect)
            {
                if (TileID.Sets.Conversion.Stone[type] && Main.rand.NextBool(50))
                {
                    Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 32, 32, ItemID.GoldCoin, 1, false, 0, false, false);
                }
            }
        }
    }
}