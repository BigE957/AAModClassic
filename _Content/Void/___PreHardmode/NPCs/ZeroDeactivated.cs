using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero;
using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero.Awakened;
using AAModClassic._Content.Void.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace AAModClassic._Content.Void.___PreHardmode.NPCs
{
	public class ZeroDeactivated : ModNPC
	{
        public static int ZeroShieldStrength = 0;

        public static Asset<Texture2D> Glowmask;
        public static Asset<Texture2D> ShieldTex => Zero.ShieldTex;
        public static Asset<Texture2D> ShieldRing => Zero.ShieldRing;
        public static Asset<Texture2D> ShieldRingGlowmask => Zero.ShieldRingGlowmask;

        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Strange Machine");

            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
        }
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.lifeMax = 20000;
			NPC.damage = 0;
			NPC.defense = 20;
			NPC.knockBackResist = 0f;
            NPC.width = 206;
            NPC.height = 208;
            NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.alpha = 0;
			NPC.dontTakeDamage = true;
			NPC.boss = false;
            NPC.npcSlots = 0;
            SpawnModBiomes = [ModContent.GetInstance<VoidBiome>().Type];
        }

		public override bool CheckActive()
		{
			return false;
		}		

		public override void AI()
		{
            if(AAWorld.downedZero || NPC.AnyNPCs(ModContent.NPCType<Zero>()) || NPC.AnyNPCs(ModContent.NPCType<ZeroA>()))
            {
                NPC.active = false;
                return;
            }

            RingRoatation += .01f;
            if (Main.netMode != NetmodeID.MultiplayerClient && AAWorld.zeroUS == true)
            {
                NPC.Transform(ModContent.NPCType<Zero>());
                return;
            }
            NPC.timeLeft = 10;
			if(NPC.ai[0] == 0)
			{
				NPC.velocity.Y += 0.003f;	
				if(NPC.velocity.Y > .3f)
				{
					NPC.ai[0] = 1f;
					NPC.netUpdate = true;
				}	
			}else
			if(NPC.ai[0] == 1)
			{
				NPC.velocity.Y -= 0.003f;	
				if(NPC.velocity.Y < -.3f)
				{
					NPC.ai[0] = 0f;
					NPC.netUpdate = true;
				}				
			}
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;
        public bool saythelinezero = false;
        public float ShieldScale = 0.5f;
        public float RingRoatation = 0;

        public static Color GetGlowAlpha()
        {
            return AAColor.ZeroShield;
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }

            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, drawColor);
            if (NPC.downedMoonlord)
            {
                BaseDrawing.DrawTexture(spriteBatch, Glowmask.Value, 0, NPC, GetGlowAlpha());
            }
            BaseDrawing.DrawTexture(spriteBatch, ShieldTex.Value, 0, NPC.position, NPC.width, NPC.height, ShieldScale, 0, 0, 1, new Rectangle(0, 0, ShieldTex.Value.Width, ShieldTex.Value.Height), GetGlowAlpha(), true);
            BaseDrawing.DrawTexture(spriteBatch, ShieldRing.Value, 0, NPC.position, NPC.width, NPC.height, 1, RingRoatation, 0, 1, new Rectangle(0, 0, ShieldRing.Value.Width, ShieldRing.Value.Height), drawColor, true);
            BaseDrawing.DrawTexture(spriteBatch, ShieldRingGlowmask.Value, 0, NPC.position, NPC.width, NPC.height, 1, RingRoatation, 0, 1, new Rectangle(0, 0, ShieldRingGlowmask.Value.Width, ShieldRingGlowmask.Value.Height), AAColor.COLOR_WHITEFADE1, true);
            
            return false;
        }

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            modifiers.TargetDamageMultiplier *= 0;
        }
    }

    public class ZeroHandler : ModSystem
    {
        public static int ZX = -1;
        public static int ZY = -1;
        public static int Shield;

        public override void OnWorldLoad()/* tModPorter Suggestion: Also override OnWorldUnload, and mirror your worldgen-sensitive data initialization in PreWorldGen */
        {
            ZX = -1;
            ZY = -1;
        }

        public override void SaveWorldData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
        {
            if (ZX != -1)
            {
                tag.Add("ZX", ZX);
                tag.Add("ZY", ZY);
            }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            Reset(); //reset it so it doesn't fuck up between world loads	
            if (tag.ContainsKey("ZX"))
            {
                ZX = tag.GetInt("ZX");
                ZY = tag.GetInt("ZY");
				if(!AAWorld.downedZero)			
					NPC.NewNPC(Entity.GetSource_NaturalSpawn(), ZX, ZY, ModContent.NPCType<ZeroDeactivated>());
            }
        }

        public override void PostUpdateWorld()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient && 
                !AAWorld.downedZero && 
                !NPC.AnyNPCs(ModContent.NPCType<ZeroTransition>()) &&
                !NPC.AnyNPCs(ModContent.NPCType<Zero>()) && 
                !NPC.AnyNPCs(ModContent.NPCType<ZeroA>()) && 
                !Main.projectile.Any(p => p.active && p.type == ModContent.ProjectileType<ZeroDeath1>()) &&
                !Main.projectile.Any(p => p.active && p.type == ModContent.ProjectileType<ZeroDeath2>()))
            {
                SpawnDeactivatedZero();
            }
        }

		public static void Reset()
		{
			ZX = -1;
			ZY = -1;
		}

        public void SpawnDeactivatedZero()
        {
            int VoidHeight = 140;
			
			Point spawnTilePos = new Point(Main.maxTilesX / 15 * 14 + Main.maxTilesX / 15 / 2 - 100, VoidHeight);				
			Vector2 spawnPos = new Vector2(spawnTilePos.X * 16, spawnTilePos.Y * 16);
			bool anyZerosExist = NPC.AnyNPCs(ModContent.NPCType<ZeroDeactivated>()) || NPC.AnyNPCs(ModContent.NPCType<Zero>()) || NPC.AnyNPCs(ModContent.NPCType<ZeroA>());			
			if (!anyZerosExist)
			{
                int whoAmI = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<ZeroDeactivated>());
                ZX = (int)spawnPos.X;
				ZY = (int)spawnPos.Y;				
				if (Main.netMode == NetmodeID.Server && whoAmI != -1 && whoAmI < 200)
				{					
					NetMessage.SendData(MessageID.SyncNPC, number: whoAmI);
				}			
			}
        }
    }
}
