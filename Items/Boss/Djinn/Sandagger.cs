using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Djinn
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
            Item.maxStack = 999;
			Item.useAnimation = 8;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
			Item.knockBack = 0;
			Item.value = 8;
			Item.rare = 3;
			Item.shootSpeed = 9f;
			Item.shoot = Mod.Find<ModProjectile>("Sandagger").Type;
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
