using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Djinn
{
    public class Sandagger : BaseAAItem
	{
		public override void SetDefaults()
		{

            Item.damage = 15;            
            Item.DamageType = DamageClass.Ranged;
            Item.width = 14;
            Item.height = 14;
			Item.useTime = 8;
            Item.maxStack = 9999;
			Item.useAnimation = 8;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 0;
			Item.value = 8;
			Item.rare = ItemRarityID.Orange;
			Item.shootSpeed = 9f;
			Item.shoot = ModContent.ProjectileType<Projectiles.Djinn.Sandagger>();
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.consumable = true;
		}

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sandagger");
            // Tooltip.SetDefault("");
        }
    }
}
