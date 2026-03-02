using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.DevTools.Cinematic
{
	public class CinematicMount : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cinematic Mount");
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.sellPrice(0, 0, 0, 0);
			Item.rare = ItemRarityID.Expert;
			Item.UseSound = SoundID.Item25;
			Item.noMelee = true;
			Item.mountType = Mod.Find<ModMount>("CinematicThing").Type;
		}
	}
}