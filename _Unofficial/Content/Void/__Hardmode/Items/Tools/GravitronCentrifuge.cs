using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Tools;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.Void.__Hardmode.Items.Tools
{
    //TODO: make this drop from vortexes in unofficial (this was a real request the recipe was evil)
    public class GravitronCentrifuge : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Tools";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gravitron Centrifuge");
            /* Tooltip.SetDefault(@"Provides a great deal of centrifugal force, granting standard gravity in space
            Right click to deactivate effects"); */
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.ItemNoGravity[Item.type] = true;
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
            player.GetModPlayer<Magfauhryiahwugyuarguyhfdsghuasdfghfadsghjfasdghjfasdgh>().MagnetSoundSlot = SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/CodeMagnetOff"), player.Center);
            bool favorited = Item.favorited;
            Item.SetDefaults(ModContent.ItemType<GravitronCentrifugeOff>());
            Item.stack++;
            Item.favorited = favorited;
        }

        public override void UpdateInventory(Player player)
        {
            player.gravity = Math.Max(player.gravity, Player.defaultGravity);
        }
    }
}
