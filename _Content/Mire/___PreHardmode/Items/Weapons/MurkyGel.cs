using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Weapons
{
    public class MurkyGel : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Ranged";
		public override void SetDefaults()
		{
			Item.damage = 21;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 20;
			Item.height = 18;
			Item.noUseGraphic = true;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.shoot = ModContent.ProjectileType<MurkyGel_Proj>();
			Item.shootSpeed = 9f;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3;
			Item.value = Item.sellPrice(0, 0, 0, 25);
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.noMelee = true;
        }

		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Murky Gel");
            // Tooltip.SetDefault("Inflicts Oiled debuff on hit");
            Item.ResearchUnlockCount = 99;
        }
	}
}
