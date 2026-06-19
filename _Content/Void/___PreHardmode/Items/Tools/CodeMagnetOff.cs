using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Tools
{
    public class CodeMagnetOff : BaseAAItem
    {
        public SlotId MagnetSoundSlot;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Binary Code Magnet");
			/* Tooltip.SetDefault(@"Pulls items to you by moving its code closer to you
Right click the item to turn it on"); */
		}

        public override void SetDefaults()
        {
            Item.width = Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = Item.CommonMaxStack;
			Item.value = 8000;
        }

        public override bool CanRightClick() => true;

        public override void RightClick(Player player)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                MagnetSoundSlot = SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/CodeMagnetOn"), player.Center);
                bool favorited = Item.favorited;
                Item.SetDefaults(ModContent.ItemType<CodeMagnet>());
                Item.stack++;
                Item.favorited = favorited;
            }
            else
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CodeMagnet>());
        }

        public override void UpdateInventory(Player player)
        {
            if (SoundEngine.TryGetActiveSound(MagnetSoundSlot, out var magnetSound) && magnetSound.IsPlaying)
                magnetSound.Position = player.Center;
        }
    }
}
