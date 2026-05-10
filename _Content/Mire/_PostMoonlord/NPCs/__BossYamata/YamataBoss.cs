using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Base.NPCs;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata
{
    public abstract class YamataBoss : ParentNPC
	{
		public int frameWidth = 0;

		public int frameHeight = 0;

		public float nextFrameCounter = 0f;

		public int frameCount = 0;

		public bool invertFrames = false;

		public bool showHealthBar = true;

		public bool realLifeHealthBar = false;

		public bool invasionSpawn = false;

		public bool specialBiomeSpawn = false;

		public bool drawCentered = false;

		public bool drawCenteredX = false;

		public Vector2 oldDrawPos = default;

        protected override bool CloneNewInstances => true;

        public string name
		{
			get
			{
				return NPC.TypeName;
			}
			set
			{
			}
		}

		public string displayName
		{
			get
			{
				return DisplayName.ToString();
			}
			set
			{
                // DisplayName.SetDefault(value);
			}
		}

		public override Vector4 GetFrameV4()
		{
			return new Vector4(0f, 0f, frameWidth, frameHeight + 2);
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			SendMaster(writer);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			RecieveMaster(reader);
		}

		public virtual void SetMaster(params object[] args)
		{
		}

		public virtual void SendMaster(BinaryWriter writer)
		{
		}

		public virtual void RecieveMaster(BinaryReader reader)
		{
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			bool? result;
			if (!showHealthBar)
			{
				NPC.position -= NPC.netOffset;
				result = new bool?(false);
			}
			else if (realLifeHealthBar)
			{
				if (NPC.realLife == -1)
				{
					result = new bool?(false);
				}
				else
				{
					float alpha = Lighting.Brightness((int)(NPC.Center.X / 16f), (int)(NPC.Center.Y / 16f));
					Main.instance.DrawHealthBar(position.X, position.Y, Main.npc[NPC.realLife].life, Main.npc[NPC.realLife].lifeMax, alpha, scale);
					NPC.position -= NPC.netOffset;
					result = new bool?(false);
				}
			}
			else
			{
				if (NPC.boss)
				{
					scale = 1.5f;
				}
				result = null;
			}
			return result;
		}

		public override void FindFrame(int dummy)
		{
			if (frameWidth > 0 && frameHeight > 0)
			{
				NPC.frame = BaseDrawing.GetFrame(frameCount, frameWidth, frameHeight, 0, 2);
			}
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			G_HitEffect(hit.HitDirection, hit.Damage, NPC.life <= 0 || !NPC.active);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			float result;
			if (!invasionSpawn && (Main.invasionType > 0 || Main.pumpkinMoon || Main.snowMoon || Main.bloodMoon || Main.eclipse || DD2Event.Ongoing || spawnInfo.Player.ZoneTowerAny()))
			{
				result = 0f;
			}
			else if (!specialBiomeSpawn && spawnInfo.Player.ZoneTowerAny() || spawnInfo.Player.ZoneDungeon || spawnInfo.Player.ZoneMeteor || spawnInfo.Lihzahrd)
			{
				result = 0f;
			}
			else
			{
				result = G_CanSpawn(spawnInfo.SpawnTileX, spawnInfo.SpawnTileY, NPC.type, spawnInfo.Player, spawnInfo) ? 1f : 0f;
			}
			return result;
		}

		public virtual void G_HitEffect(int hitDirection, double damage, bool isDead)
		{
		}

		public virtual bool G_CanSpawn(int x, int y, int type, Player player, NPCSpawnInfo info)
		{
			return G_CanSpawn(x, y, type, player);
		}

		public virtual bool G_CanSpawn(int x, int y, int type, Player player)
		{
			return false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			if (drawCentered || drawCenteredX)
			{
				oldDrawPos = NPC.position;
				if (drawCenteredX)
				{
					NPC expr_48_cp_0 = NPC;
					expr_48_cp_0.position.X += NPC.Center.X - NPC.position.X;
				}
				else
				{
					NPC.position += NPC.Center - NPC.position;
				}
			}
			return true;
		}

		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			if (drawCentered || drawCenteredX)
			{
				NPC.position = oldDrawPos;
			}
		}

        internal void DrawYamataLeg(SpriteBatch spritebatch, NPC yamata, Vector2 start, Vector2 middle, Vector2 end, bool left, bool front)
        {
            bool awakened = (NPC.ModNPC is YamataABody);

            Texture2D legSegment;
            Texture2D legCap;
            Texture2D foot;

            if (awakened)
            {
                legSegment = ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/_PostMoonlord/NPCs/__BossYamata/Awakened/YamataABody_LegSegmentL").Value;
                legCap = ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/_PostMoonlord/NPCs/__BossYamata/Awakened/YamataABody_LegCapL").Value;
                foot = ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/_PostMoonlord/NPCs/__BossYamata/Awakened/YamataABody_Foot").Value;
            }
            else
            {
                legSegment = ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/_PostMoonlord/NPCs/__BossYamata/YamataBody_LegSegmentL").Value;
                legCap = ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/_PostMoonlord/NPCs/__BossYamata/YamataBody_LegCapL").Value;
                foot = ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/_PostMoonlord/NPCs/__BossYamata/YamataBody_Foot").Value;
            }
            SpriteEffects effects = left ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            float colorMult = front ? 1f : 0.75f;

            float dist = (int)middle.Distance(end);
            Vector2 dir = middle.DirectionTo(end);
            for (int i = 0; i < dist; i += legSegment.Height)
            {
                Vector2 worldPos = middle + dir * i;
                spritebatch.Draw(legSegment, worldPos - Main.screenPosition, null, Lighting.GetColor(worldPos.ToTileCoordinates()).MultiplyRGB(Color.White * colorMult) * NPC.Opacity, dir.ToRotation() - MathHelper.PiOver2, legSegment.Size() * 0.5f, 1f, effects, 0);
            }

            spritebatch.Draw(legCap, middle - Main.screenPosition, null, Lighting.GetColor(middle.ToTileCoordinates()).MultiplyRGB(Color.White * colorMult) * NPC.Opacity, dir.ToRotation() - MathHelper.PiOver2, legCap.Size() * 0.5f, 1f, effects, 0);

            dist = (int)start.Distance(middle);
            dir = start.DirectionTo(middle);
            for (int i = 0; i < dist; i += legSegment.Height)
            {
                Vector2 worldPos = start + dir * i;
                spritebatch.Draw(legSegment, worldPos - Main.screenPosition, null, Lighting.GetColor(worldPos.ToTileCoordinates()).MultiplyRGB(Color.White * colorMult) * NPC.Opacity, dir.ToRotation() - MathHelper.PiOver2, legSegment.Size() * 0.5f, 1f, effects, 0);
            }

            spritebatch.Draw(foot, end - Main.screenPosition, null, Lighting.GetColor(end.ToTileCoordinates()).MultiplyRGB(Color.White * colorMult) * NPC.Opacity, 0f, foot.Size() * 0.5f, 1f, effects, 0);
        }

    }
}
