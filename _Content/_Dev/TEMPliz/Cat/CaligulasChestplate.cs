using AAModClassic._Content._Dev.TEMPliz.Dragon;
using AAModClassic._Content.Mire.___PreHardmode.Items.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.TEMPliz.Cat
{
    [AutoloadEquip(EquipType.Body)]
    public class CaligulasChestplate : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Vanity.Caligulas";

        public override void Load()
        {
            EquipLoader.AddEquipTexture(Mod, Texture + "_Body_Alt", EquipType.Body, item: this, name: $"{Name}_Body_Alt");
            ZAAPlayer.ModifyDrawInfoEvent += ModifyDrawInfo;
        }

        private void ModifyDrawInfo(Player player)
        {
            int red = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Body);
            int blue = EquipLoader.GetEquipSlot(Mod, Name + "_Body_Alt", EquipType.Body);

            if (player.body == blue && player.direction == -1)
                player.body = red;
            else if (player.body == red && player.direction == 1)
                player.body = blue;
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Midnight Cat Blouse");
            // Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");
            //ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;

            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<AquariumChestplate>();

            int red = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Body);
            int blue = EquipLoader.GetEquipSlot(Mod, Name + "_Body_Alt", EquipType.Body);
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(121, 21, 214);
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 20;
            Item.rare = ItemRarityID.Purple;
            Item.vanity = true;
        }
    }
}