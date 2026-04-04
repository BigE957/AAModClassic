using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Serpent
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
            Item.maxStack = 9999;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 10;
            Item.height = 10;
			Item.useTime = 20;
			Item.useAnimation = 20;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 0;
			Item.value = 100;
			Item.rare = ItemRarityID.Orange;
			Item.shootSpeed = 12f;
			Item.shoot = ModContent.ProjectileType<SS>();
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
        }
    }
}
