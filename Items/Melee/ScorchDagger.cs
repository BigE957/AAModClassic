using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class ScorchDagger : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Scorch Dagger");
		}
		public override void SetDefaults()
		{
			Item.damage = 26;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 34;
			Item.height = 34;
			Item.useTime = 7;
			Item.useAnimation = 7;
			Item.useStyle = 3;
			Item.knockBack = 3;
			Item.value = 2000;
			Item.rare = 1;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
        }
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }
	}
}
