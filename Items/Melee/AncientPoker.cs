using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class AncientPoker : BaseAAItem
    {
        public override void SetStaticDefaults()
		{
		// DisplayName.SetDefault("Aqua Lance");
		// Tooltip.SetDefault("");
		}
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 66;
            Item.height = 64;
            Item.scale = 1.1f;
            Item.maxStack = 1;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useTurn = true;
			Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.shoot = Mod.Find<ModProjectile>("APP").Type;  //put your Spear projectile name
            Item.shootSpeed = 4f;
        }
		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[Item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
		}
    }
}
