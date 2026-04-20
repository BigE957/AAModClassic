using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.GlowingMushroom.___PreHardmode.Items._BossTruffleToad.Weapons
{
    public class MushrockStaff : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Mushrock Staff");
		}

		public override void SetDefaults()
		{
			Item.damage = 15;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 6;
			Item.width = 58;
			Item.height = 58;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.knockBack = 5;
			Item.value = 100000;
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<MushrockStaff_Rock>();
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.shootSpeed = 15f;
        }
	}
}