using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity
{
    public class LittleE : TransformationAccessory, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Vanity.BigE";

        public override string AssetPath => "AAModClassic/_Content/_Dev/__Hardmode/Items/Armor/Vanity/";

        public override (EquipType, string, string)[] EquipSlots =>
        [
            (EquipType.Head, "BigE", null),
            (EquipType.Body, "BigE", null),
            (EquipType.Legs, "BigE", null),
        ];

        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 14));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
            ItemID.Sets.ShimmerTransformToItem[ItemID.AlphabetStatueE] = Type;
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 32;
            Item.accessory = true;
            Item.vanity = true;
            Item.rare = ItemRarityID.Purple;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                    line2.OverrideColor = new Color(255, 0, 0);

            string text = Language.GetTextValue("Mods.AAModClassic.Items.Vanity.BigE.LittleE.Tooltip").FormatWith(Main.LocalPlayer.name);       
            if (Item.social)
                tooltips.Insert(1, new(AAMod.instance, "Tooltip", text));
            else
                tooltips[3].Text = text;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            var frame = Item.GetFrame(whoAmI);
            var position = Item.Center - Main.screenPosition + Vector2.UnitY * 4;
            var origin = frame.Size() / 2f;
            spriteBatch.Draw(TextureAssets.Item[Type].Value, position, frame, lightColor, rotation, origin, scale, SpriteEffects.None, 0);
            return false;
        }
    }
}
