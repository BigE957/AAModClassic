using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee
{
    public class CarnalCrusher : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.damage = 260;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 90;
            Item.height = 90;
            Item.useTime = 45;
            Item.useAnimation = 45;     
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.value = 200000;        
            Item.rare = ItemRarityID.LightPurple;
            Item.crit = 10;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Carnal Crusher");
            // Tooltip.SetDefault("Critical Hits heal you");
        }
		
		public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation +=
                new Vector2(-8 * player.direction, 16 * player.gravDir).RotatedBy(player.itemRotation);
        }

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Blood);
			}
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
            if (target.type == NPCID.TargetDummy)
            {
                return;
            }
            float num = damageDone * 0.075f;
            if ((int)num == 0)
            {
                return;
            }
            if (Main.LocalPlayer.lifeSteal <= 0f)
            {
                return;
            }
            Main.LocalPlayer.lifeSteal -= num;
            int num2 = Item.playerIndexTheItemIsReservedFor;
            if (hit.Crit)
            {
                Projectile.NewProjectile(target.GetSource_OnHurt(player), target.position.X, target.position.Y, 0f, 0f, ProjectileID.VampireHeal, 0, 0f, Item.playerIndexTheItemIsReservedFor, num2, num);
            }
        }
		
        public override void AddRecipes()  //How to craft this sword
        {
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<FleshrendClaymore>());
			recipe.AddIngredient(ItemID.LunarTabletFragment, 30);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
        }
    }
}
