using AAModClassic._Content.Desert.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Ocean.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Void.___PreHardmode.Items.Weapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Weapons
{
    public class BlazingDawn : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Blazing Dawn");
            // Tooltip.SetDefault("The Radiant Dawn calls");
        }
		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 62;
			Item.height = 62;
			Item.useTime = 26;
			Item.useAnimation = 26;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = false;
        }
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if(Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.AshRain>(), 0f, 0f, 46, default, 1.381579f)];
                dust.noGravity = true;
            }
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<FlamingFury>(), 1);
			recipe.AddIngredient(ModContent.ItemType<OceanRazor>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DoomiteSaber>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DesertScimitar>(), 1);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 400);
        }
	}
}
