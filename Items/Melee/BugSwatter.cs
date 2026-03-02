using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class BugSwatter : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bug Swatter");
			// Tooltip.SetDefault(@"Does extra damage to creepy crawlies");
		}

		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 64;
			Item.height = 64;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 2;
			Item.value = Item.sellPrice (0, 1, 0, 0);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.type == NPCID.Bee
                || target.type == NPCID.BeeSmall
                || target.type == NPCID.BigHornetFatty
                || target.type == NPCID.BigHornetHoney
                || target.type == NPCID.BigHornetLeafy
                || target.type == NPCID.BigHornetSpikey
                || target.type == NPCID.BigHornetStingy
                || target.type == NPCID.GiantMossHornet
                || target.type == NPCID.Hornet
                || target.type == NPCID.HornetFatty
                || target.type == NPCID.HornetHoney
                || target.type == NPCID.HornetSpikey
                || target.type == NPCID.LittleHornetStingy
                || target.type == NPCID.LittleMossHornet
                || target.type == NPCID.MossHornet
                || target.type == NPCID.TinyMossHornet
                || target.type == NPCID.VortexHornet
                || target.type == NPCID.VortexHornetQueen
                || target.type == NPCID.QueenBee
                || target.type == NPCID.LightningBug
                || target.type == NPCID.StardustSpiderBig
                || target.type == NPCID.StardustSpiderSmall
                || target.type == NPCID.WallCreeper
                || target.type == NPCID.WallCreeperWall
                || target.type == NPCID.BlackRecluse
                || target.type == NPCID.BlackRecluseWall)
            {
                Item.damage = damage * 3;
            }
            else
            {
                Item.damage = 30;
            }
        }
	}
}
