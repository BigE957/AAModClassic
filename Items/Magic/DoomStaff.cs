using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class DoomStaff : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Doom Rod");
		}

		public override void SetDefaults()
		{
			Item.damage = 14;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 6;
			Item.width = 42;
			Item.height = 42;
			Item.useTime = 28;
			Item.useAnimation = 28;
			Item.useStyle = 1;
			Item.noMelee = true;
			Item.knockBack = 5;
			Item.value = 1000;
			Item.rare = 2;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("DoomProj").Type;
			Item.shootSpeed = 6f;
		}
	}
}