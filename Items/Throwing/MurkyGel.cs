using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
    public class MurkyGel : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.damage = 21;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 20;
			Item.height = 18;
			Item.noUseGraphic = true;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.shoot = Mod.Find<ModProjectile>("MurkyGelP").Type;
			Item.shootSpeed = 9f;
			Item.useStyle = 1;
			Item.knockBack = 3;
			Item.value = Item.sellPrice(0, 0, 0, 25);
			Item.rare = 2;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.noMelee = true;
        }

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Murky Gel");
			// Tooltip.SetDefault("Inflicts Oiled debuff on hit");
		}
	}
}
