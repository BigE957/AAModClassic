using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic._Content.Inferno.___PreHardmode.Items._BossBroodmother.Accessories;
using AAModClassic._Removed.Content._Tinker.__Hardmode.Items.Accessories;
using AAModClassic._Removed.Content._Tinker.___PreHardmode.Items.Accessories;
using Terraria.ID;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.Accessories
{
    [AutoloadEquip(EquipType.Back, EquipType.Front)]
    public class HoloCape : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Holographic Cloak");
            /* Tooltip.SetDefault(
@"15% Increased Damage Resistance"); */
        }
        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 78;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
            Item.expert = true; Item.expertOnly = true;
            Item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.endurance *= 1.15f;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ModContent.ItemType<DragonSerpentNecklace>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == ModContent.ItemType<StormCharm>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == ModContent.ItemType<DragontamersCloak>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
    
}