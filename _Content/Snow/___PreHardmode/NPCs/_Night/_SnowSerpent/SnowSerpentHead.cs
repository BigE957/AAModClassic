using AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent;
using AAModClassic._Content.Snow.___PreHardmode.NPCs.__BossSubzeroSerpent;
using AAModClassic.Globals;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.___PreHardmode.NPCs._Night._SnowSerpent
{
    public class SnowSerpentHead : ModNPC
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Snow Serpent");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                PortraitPositionXOverride = 0,
                Position = new Vector2(24, 12),
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
        }

		public override void SetDefaults()
		{
			NPC.damage = 20;
			NPC.npcSlots = 5f;
            NPC.damage = 35;
            NPC.width = 20;
            NPC.height = 20;
            NPC.defense = 13;
            NPC.lifeMax = 250;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            AnimationType = NPCID.GiantWormHead;
            NPC.behindTiles = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.DeathSound = SoundID.NPCDeath7;
            NPC.netAlways = true;
            NPC.value = Item.buyPrice(0, 0, 10, 0);
            NPC.buffImmune[BuffID.Frostburn] = true;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<AAModClassic.Items.Banners.SnakeBanner>();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneSnow &&
                NPC.downedBoss3 && 
                !Main.dayTime ? .1f : 0f;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.SnowSerpent")
            ]);
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
			AAAI.AIWorm(NPC, new int[]{ ModContent.NPCType<SnowSerpentHead>(), ModContent.NPCType<SnowSerpentBody>(), ModContent.NPCType<SnowSerpentTail>() }, 9, 8f, 12f, 0.1f, false, false);
            
            if (NPC.velocity.X < 0f)
            {
                NPC.spriteDirection = 1;

            }
            else
            {
                NPC.spriteDirection = -1;
            }
        }
        
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.Chilled, 200, true);
			}
			else
			{
				target.AddBuff(BuffID.Chilled, 100, true);
			}
		}

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.IceDust>(), hit.HitDirection, -1f, 0);
            }
            if (NPC.life == 0)
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.SnowDustLight>(), hit.HitDirection, -1f, 0);
                }
            }
        }

        public override bool PreKill()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<SubzeroSerpent_Head>()))
            {
                return false;
            }
            return base.PreKill();
        }
        public override void OnKill()
        {
            if (Main.rand.NextBool(4))
            {
                NPC.DropLoot(ModContent.ItemType<SubzeroCrystal>());
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.IsABestiaryIconDummy)
                return DrawingUtils.DrawAnimatedBestiaryWorm(spriteBatch, NPC, drawColor, TextureAssets.Npc[Type].Value, TextureAssets.Npc[ModContent.NPCType<SnowSerpentBody>()].Value, 4, 28, 0.25f, Vector2.Zero, 2, 10, headOffset: -24);
            return true;
        }
    }
}