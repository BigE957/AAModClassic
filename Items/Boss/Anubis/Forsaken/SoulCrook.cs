using AAModClassic.Base.BaseMod.Base;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Boss.Anubis.Forsaken
{
    public class SoulCrook : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Crook of the Soul Judge");	
            BaseUtility.AddTooltips(Item, new string[] { "Phases through tiles", "Every hit the crook makes heals you when it returns" });			
		}

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.UseSound = SoundID.Item1;
            Item.damage = 160;
            Item.knockBack = 10;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = Mod.ProjType("Crook");
            Item.shootSpeed = 15;
            Item.rare = ItemRarityID.Purple;
        }

        public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
	}
}