using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Rajah.Supreme
{
    public class Excalihare : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.damage = 500;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 80;
			Item.height = 80;
			Item.useTime = 27;
			Item.useAnimation = 27;
			Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 50, 0, 0);
            Item.rare = 9;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("Excalihare").Type;
            Item.scale = 1.1f;
            Item.shootSpeed = 14f;
            Item.knockBack = 6.5f;
            Item.expert = true; Item.expertOnly = true;
		}

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> list)
        {
            foreach (Terraria.ModLoader.TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity14;
                }
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(Terraria.ModLoader.ModContent.BuffType<Buffs.InfinityOverload>(), 120);
        }
    }
}
