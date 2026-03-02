using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class RustyCutlass : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rusty Cutlass");
			// Tooltip.SetDefault("Even being rusty, it's still hard 'n sharp");
		}
		
		public override void SetDefaults()
		{
			Item.damage = 21;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 34;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3;
			Item.value = 20000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;  
		}
	}
}