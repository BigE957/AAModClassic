using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class Pyrosphere : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Pyrosphere");			
		}		
		
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Green;
            Item.value = BaseUtility.CalcValue(0, 0, 90, 50);
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.UseSound = SoundID.Item1;
            Item.damage = 15;
            Item.knockBack = 7;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.shoot = Terraria.ModLoader.ModContent.ProjectileType<Projectiles.Pyrosphere>();
            Item.shootSpeed = 10;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.channel = true;		
        }
	}
}