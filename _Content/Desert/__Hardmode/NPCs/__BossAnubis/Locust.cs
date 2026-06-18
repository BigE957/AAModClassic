using AAModClassic._Unreleased.Content.Desert.__Hardmode.NPCs.__BossAnubis;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.UI.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Desert.__Hardmode.NPCs.__BossAnubis
{
    public class Locust : ModNPC
	{				
		public override void SetStaticDefaults()
		{
            Main.npcFrameCount[NPC.type] = 4;
		}

        public override void SetDefaults()
        {
            NPC.width = 42;
            NPC.height = 38;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.npcSlots = 1;
            NPC.aiStyle = -1;
            NPC.lifeMax = 2500;
            NPC.defense = 130;
            NPC.damage = 5;
            NPC.HitSound = SoundID.NPCHit31;
            NPC.DeathSound = SoundID.NPCDeath35;
            NPC.knockBackResist = 0f;	
			NPC.noTileCollide = true;		
			NPC.defense = 40;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
            ]);
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public override void OnKill()
		{

		}
		
		public int body = -1;
		public float rotValue = -1f;
		public bool spawnedDust = false;

		public override void AI()
		{
			NPC.TargetClosest(true);
			NPC.noGravity = true;
			if(body == -1)
			{
				int npcID;
                if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased))
                    npcID = BaseAI.GetNPC(NPC.Center, ModContent.NPCType<AnubisUnreleased>(), -1, null);
                else
                    npcID = BaseAI.GetNPC(NPC.Center, ModContent.NPCType<Anubis>(), -1, null);
                if (npcID >= 0) body = npcID;
			}
			if(body == -1) return;				
			NPC anubis = Main.npc[body];
			if(anubis == null || anubis.life <= 0 || !anubis.active || anubis.type != ModContent.NPCType<Anubis>() && anubis.type != ModContent.NPCType<AnubisUnreleased>()){ BaseAI.KillNPCWithLoot(NPC); return; }

			for (int m = NPC.oldPos.Length - 1; m > 0; m--)
			{
				NPC.oldPos[m] = NPC.oldPos[m - 1];
			}
			NPC.oldPos[0] = NPC.position;

			int locust = 0;
			if (anubis.ModNPC is Anubis baseAnubis)
                locust = baseAnubis.LocustCount;
			else if (anubis.ModNPC is AnubisUnreleased reworkAnubis)
                locust = reworkAnubis.LocustCount;

            if (rotValue == -1f) rotValue = NPC.ai[0] % locust * ((float)Math.PI * 2f / locust);
			rotValue += 0.05f;
			while(rotValue > (float)Math.PI * 2f) rotValue -= (float)Math.PI * 2f;
			NPC.Center = BaseUtility.RotateVector(anubis.Center, anubis.Center + new Vector2(160f, 0f), rotValue);

			NPC.spriteDirection = NPC.position.X - NPC.oldPos[1].X < 0 ? -1 : 1;
			NPC.rotation = (NPC.position.X - NPC.oldPos[1].X) * 0.05f;

            Player player = Main.player[anubis.target];
            BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, ModContent.ProjectileType<Locust_Spit>(), ref NPC.ai[2], Main.expertMode ? 120 : 80, NPC.damage / 2, 9, true);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Color lightColor = BaseDrawing.GetNPCColor(NPC, null);
			if(Main.player[NPC.target] != null && Main.player[NPC.target].active && !Main.player[NPC.target].dead) 
				BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, 2f, 0.9f, 2, true, 0f, 0f, lightColor);
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, lightColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
			return false;
		}		
	}
}