using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.Weapons
{
    public class BaneOfTheBunny : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Bane of the Bunny");
            /* Tooltip.SetDefault(@"Right click to use as a spear
Left click to use as a javelin
Throwing Javelins right after a spear thrust throws javelins faster for a moment"); */
		}

		public override void SetDefaults()
		{
            Item.damage = 100;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 92; 
            Item.height = 92;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 20;
            Item.knockBack = 4f;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.shoot = ModContent.ProjectileType<BaneOfTheBunny_Holdout>();
            Item.shootSpeed = 4f;
            Item.rare = ItemRarityID.Yellow;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useTime = 15;
                Item.useAnimation = 15;
                Item.UseSound = SoundID.Item1;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.shoot = ModContent.ProjectileType<BaneOfTheBunny_Holdout>();  
                Item.shootSpeed = 10f;
                Item.autoReuse = true;
            }
            else
            {
                Item.useAnimation = 13;
                Item.useTime = 13;
                Item.UseSound = SoundID.Item1;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.shoot = ModContent.ProjectileType<BaneOfTheBunny_Proj>();
                Item.shootSpeed = 10f;
                Item.autoReuse = true;
            }
            return base.CanUseItem(player);
        }
    }
}