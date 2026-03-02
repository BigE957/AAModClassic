using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class Chicken : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rubber Chicken");
        }

		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 54;
			Item.height = 60;
			Item.useTime = 25;
            Item.useAnimation = 25;
			Item.useStyle = 1;
			Item.knockBack = 3;
			Item.value = 1000;
            Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.rare = 11;
		}

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            SoundEngine.PlaySound(Mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Sounds/Chicken"), player.Center);
        }
    }
}
