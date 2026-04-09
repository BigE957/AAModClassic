using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Materials;
using AAModClassic.Projectiles.Tools;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Tools
{
	public class DoomiteMiningLaser : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Doomite Mining Laser");
            BaseUtility.AddTooltips(Item, new string[] { "Mines with an antimatter laser" });			
		}		

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 25;
            Item.useTime = 15;
            Item.shootSpeed = 36f;
            Item.knockBack = 1f;
            Item.width = 20;
            Item.height = 12;
            Item.damage = 10;
            Item.pick = 100;
            Item.axe = 30;
            Item.UseSound = SoundID.Item23;
            Item.shoot = ModContent.ProjectileType<MiningLaser>();
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(0, 0, 54, 0);
            Item.tileBoost = 2;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.channel = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override bool CanUseItem(Player player)
        {
            for (int m = 0; m < Main.projectile.Length; m++)
            {
                Projectile p = Main.projectile[m];
                if (p != null && p.active && p.owner == player.whoAmI && p.type == Item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
	}
}