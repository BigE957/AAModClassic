using AAModClassic._Content.Terra.__Hardmode.Items.Armor;
using AAModClassic._Unreleased.Content.Void.Dusts;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Accessories
{
    public class BrokenCode : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Broken Code");
            /* Tooltip.SetDefault(@"
            'You don't look so good'"); */
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 52;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.expert = true;
            Item.accessory = true;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.COLOR_WHITEFADE1;
        }

        public override void RegisterEquipEffects()
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                AddEffect<BrokenCodeTeleportUnofficialEffect>();
            }
            else
            {
                AddEffect<BrokenCodeWhateverThisShitIsEffect>();
                AddEffect<BrokenCodeTeleportEffect>();
            }
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Red.ToVector3() * 0.55f * Main.essScale);
        }
    }
}