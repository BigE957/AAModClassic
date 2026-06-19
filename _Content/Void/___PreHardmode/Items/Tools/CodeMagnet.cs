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
    public class CodeMagnet : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Tools";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Binary Code Magnet");
			/* Tooltip.SetDefault(@"Pulls items to you by moving its code closer to you
Right click the item to turn it off"); */
		}

        public override void SetDefaults()
        {
            Item.width = Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 1;
			Item.value = 8000;
        }

        public override bool CanRightClick() => true;

        public override void RightClick(Player player)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                player.GetModPlayer<Magfauhryiahwugyuarguyhfdsghuasdfghfadsghjfasdghjfasdgh>().MagnetSoundSlot = SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/CodeMagnetOff"), player.Center);
                bool favorited = Item.favorited;
                Item.SetDefaults(ModContent.ItemType<CodeMagnetOff>());
                Item.stack++;
                Item.favorited = favorited;
            }
            else
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CodeMagnetOff>());
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 20);
            recipe.AddIngredient(ModContent.ItemType<DoomiteScrap>(), 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }

    public class Magfauhryiahwugyuarguyhfdsghuasdfghfadsghjfasdghjfasdgh : ModPlayer
    {
        public SlotId MagnetSoundSlot;

        public override void PostUpdate()
        {
            if (SoundEngine.TryGetActiveSound(MagnetSoundSlot, out var magnetSound) && magnetSound.IsPlaying)
            {
                magnetSound.Position = Player.Center;
            }
        }
    }
}
