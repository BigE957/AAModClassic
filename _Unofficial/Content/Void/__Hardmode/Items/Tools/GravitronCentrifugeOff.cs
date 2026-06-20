using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Tools;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.Void.__Hardmode.Items.Tools
{
    public class GravitronCentrifugeOff : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Tools";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gravitron Centrifuge (Inactive)");
            /* Tooltip.SetDefault(@"Provides a great deal of centrifugal force, granting standard gravity in space
            Effects are currently inactive, Right click to reactivate"); */
        }

        public override void SetDefaults()
        {
            Item.width = Item.height = 16;
            Item.rare = ItemRarityID.Yellow;
            Item.maxStack = 1;
            Item.value = 8000;
        }

        public override bool CanRightClick() => true;

        public override void RightClick(Player player)
        {
            player.GetModPlayer<Magfauhryiahwugyuarguyhfdsghuasdfghfadsghjfasdghjfasdgh>().MagnetSoundSlot = SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/CodeMagnetOn"), player.Center);
            bool favorited = Item.favorited;
            Item.SetDefaults(ModContent.ItemType<GravitronCentrifuge>());
            Item.stack++;
            Item.favorited = favorited;
        }
    }
}
