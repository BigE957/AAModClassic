using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Serpent
{
    public class SnowflakeShuriken : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Snowflake Shuriken");
        }
        public override void SetDefaults()
		{
            Item.damage = 20;
            Item.maxStack = 999;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 10;
            Item.height = 10;
			Item.useTime = 20;
			Item.useAnimation = 20;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
			Item.knockBack = 0;
			Item.value = 100;
			Item.rare = 3;
			Item.shootSpeed = 12f;
			Item.shoot = Mod.Find<ModProjectile>("SS").Type;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
        }
    }
}
