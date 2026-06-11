using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic._Content.Inferno.__Hardmode.Items.Weapons;
using AAModClassic._Content.Mire.__Hardmode.Items.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Chaos.__Hardmode.Items.Weapons
{
    public class Chaos_Item : BaseAAItem
	{
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaos");
			// Tooltip.SetDefault("Wrath and fury upon those struck by this discordian blade");
        }
		public override void SetDefaults()
		{
            
			Item.damage = 105;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 84;
			Item.height = 84;
			Item.useTime = 35;
			Item.useAnimation = 35;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Chaos_ChaosBeam>();
            Item.shootSpeed = 14f;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<DraconianDawn>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DreadTwilight>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 500);
			target.AddBuff(BuffID.Venom, 500);
        }
	}
}
