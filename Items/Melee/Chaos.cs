using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee
{
    public class Chaos : BaseAAItem
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
			Item.shoot = Mod.Find<ModProjectile>("ChaosShot").Type;
            Item.shootSpeed = 14f;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod, "TrueBlazingDawn", 1);
			recipe.AddIngredient(Mod, "TrueAbyssalTwilight", 1);
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
