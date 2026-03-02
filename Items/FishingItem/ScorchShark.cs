using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.FishingItem
{
    public class ScorchShark : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.damage = 40;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 36;
			Item.height = 32;
			Item.useTime = 7;
			Item.useAnimation = 20;
			Item.pick = 180;
			Item.useStyle = 1;
			Item.knockBack = 5;
            Item.value = 108000;
            Item.rare = 9;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Scorch Shark");
			// Tooltip.SetDefault("");
		}
    }
}
