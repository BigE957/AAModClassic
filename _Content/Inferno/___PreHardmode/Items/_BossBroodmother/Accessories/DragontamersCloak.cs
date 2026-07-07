using AAModClassic._Removed.Content._Tinker.___PreHardmode.Items.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items._BossBroodmother.Accessories
{
    [AutoloadEquip(EquipType.Back, EquipType.Front)]
    public class DragontamersCloak : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragontamer's Cloak");
        }
        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 78;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
            Item.expert = true;
            Item.defense = 3;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect(new EnduranceEffect(0.03f));
        }

        //TODO: can this be centralized?
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
                    if (slot != i && player.armor[i].type == ItemID.WormScarf)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
    
}