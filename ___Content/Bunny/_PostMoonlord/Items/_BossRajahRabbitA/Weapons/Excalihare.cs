using AAModClassic.Buffs;
using AAModClassic.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Weapons
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
			Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(0, 50, 0, 0);
            Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Excalihare_Proj>();
            Item.scale = 1.1f;
            Item.shootSpeed = 14f;
            Item.knockBack = 6.5f;
            Item.expert = true; Item.expertOnly = true;
		}

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity14;
                }
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<InfinityOverload_Buff>(), 120);
        }
    }
}
