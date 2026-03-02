using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Rajah.Supreme
{
    public class BaneOfTheBunnyEX : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Bane of the Slaughterer");
            /* Tooltip.SetDefault(@"Right click to use as a spear
Left click to use as a javelin
Throwing Javelins right after a spear thrust throws javelins faster for a moment
Bane of the Bunny EX"); */
		}

		public override void SetDefaults()
		{
            Item.damage = 400;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 92; 
            Item.height = 92;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.useStyle = 5;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(1, 0, 0, 0);
            Item.shoot = Mod.Find<ModProjectile>("BaneEX").Type;
            Item.rare = 9;
            Item.expert = true; Item.expertOnly = true;
            Item.useAnimation = 13;
            Item.useTime = 13;
            Item.autoReuse = true;
            Item.shootSpeed = 12f;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useAnimation = 11;
                Item.useTime = 11;
                Item.useStyle = 5;
                Item.shoot = Mod.Find<ModProjectile>("BaneEX").Type;  
            }
            else
            {
                Item.useAnimation = 13;
                Item.useTime = 13;
                Item.useStyle = 1;
                Item.shoot = Mod.Find<ModProjectile>("BaneTEX").Type;
            }
            return base.CanUseItem(player);
        }
    }
}