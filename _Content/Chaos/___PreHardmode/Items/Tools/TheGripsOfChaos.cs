using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic.UI.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.RuntimeDetour.HookGen;
using System;
using System.Numerics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static AAModClassic.Assets.AssetDirectory;

namespace AAModClassic._Content.Chaos.___PreHardmode.Items.Tools
{
	public class TheGripsOfChaos : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Tools";
        public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("The Grips of Chaos");
			/* Tooltip.SetDefault(@"Fires 2 different hooks depending on which one is already out
Red has a longer range
Blue pulls in/retracts quicker"); */
		}

		public override void SetDefaults() 
		{
			Item.CloneDefaults(ItemID.SkeletronHand);
			Item.shoot = ModContent.ProjectileType<TheGripsOfChaos_HookInferno>();
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<DragonClaw_Item>(), 5);
			recipe.AddIngredient(ModContent.ItemType<HydraClaw_Item>(), 5);
			recipe.AddIngredient(ModContent.ItemType<IncineriteBar>(), 5);
			recipe.AddIngredient(ModContent.ItemType<AbyssiumBar>(), 5);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
