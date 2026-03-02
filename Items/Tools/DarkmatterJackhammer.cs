using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class DarkmatterJackhammer : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Darkmatter Jackhammer");
        }

		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 52;
            Item.height = 22;
			Item.useTime = 7;
			Item.useAnimation = 15;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.hammer = 120;
			Item.useStyle = 5;
			Item.knockBack = 6;
			Item.value = 550000;
            Item.UseSound = SoundID.Item23;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("DarkmatterJackhammerPro").Type;
            Item.shootSpeed = 40f;
            Item.tileBoost += 1;
            Item.rare = 9;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }
    }
}
