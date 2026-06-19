using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items
{
    public class VoidGraivtyItemIDKTheNameYet : BaseAAItem
    {
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
            Terraria.Audio.SoundEngine.PlaySound(SoundID.DD2_DarkMageHealImpact);
            bool favorited = Item.favorited;
            Item.SetDefaults(ModContent.ItemType<VoidGraivtyItemIDKTheNameYetOff>());
            Item.stack++;
            Item.favorited = favorited;
        }

        public override void UpdateInventory(Player player)
        {
            player.gravity = Math.Max(player.gravity, Player.defaultGravity);
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
}
