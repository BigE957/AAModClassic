using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee
{
    public class BloodyMary : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bloody Mary");
		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 46;
			Item.height = 52;
			Item.useTime = 19;
			Item.useAnimation = 19;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
		}
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<BloodyDust>());
			}
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffID.Bleeding, 120);
		}
	}
}