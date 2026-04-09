using AAModClassic.Globals;
using AAModClassic.Items.Boss;
using AAModClassic.Tiles.Crafters;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee
{
    public class Aerodrake : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aerodrake");
			// Tooltip.SetDefault("Flying Dragon EX");
		}

		public override void SetDefaults()
		{
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.DD2_SonicBoomBladeSlash;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 1250;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.width = 82;
            Item.height = 102;
            Item.knockBack = 5.5f;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.value = Item.sellPrice(1, 0, 0, 0);
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.shoot = ModContent.ProjectileType<Projectiles.Aerodrake>();
            Item.shootSpeed = 17f;
            Item.expert = true; Item.expertOnly = true;

            glowmaskDrawType = GLOWMASKTYPE_SWORD;
            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow";
            glowmaskDrawColor = AAColor.COLOR_WHITEFADE1;
        }

		public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation +=
                new Vector2(-4 * player.direction, 16 * player.gravDir).RotatedBy(player.itemRotation);
        }
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DD2SquireBetsySword, 1);
			recipe.AddIngredient(ModContent.ItemType<EXSoul>(), 1);
			recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
			recipe.Register();
		}

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Torch, 0f, 0f, 46)];
                dust.noGravity = true;
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 400);
        }
	}
}
