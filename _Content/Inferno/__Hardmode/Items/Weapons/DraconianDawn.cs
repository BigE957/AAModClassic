using AAModClassic._Content.Inferno.___PreHardmode.Items.Weapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Weapons
{
    public class DraconianDawn : BaseAAItem
	{
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Draconian Dawn");
			// Tooltip.SetDefault("The True blade of the Rising Sun");
        }
		public override void SetDefaults()
		{
            
			Item.damage = 130;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 86;
			Item.height = 86;
			Item.useTime = 32;
			Item.useAnimation = 32;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = false;
			Item.shoot = ModContent.ProjectileType<DraconianDawn_DawnRay>();
            Item.shootSpeed = 12f;
        }


        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 46, default, 1.381579f)];
                dust.noGravity = true;
            }
        }

        public override void AddRecipes()
		{
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<BlazingDawn>(), 1);
                recipe.AddIngredient(ItemID.SoulofFright, 20);
                recipe.AddIngredient(ItemID.SoulofMight, 20);
                recipe.AddIngredient(ItemID.SoulofSight, 20);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
		}
		
		 public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 500);
        }
	}
}
