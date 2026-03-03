using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Base.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Yamata
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
			if (!invasionSpawn && (Main.invasionType > 0 || Main.pumpkinMoon || Main.snowMoon || Main.bloodMoon || Main.eclipse || DD2Event.Ongoing || BaseExtensions.InZone(spawnInfo.Player, "TowerAny", null)))
			{
				result = 0f;
			}
			else if (!specialBiomeSpawn && (BaseExtensions.InZone(spawnInfo.Player, "TowerAny", null) || BaseExtensions.InZone(spawnInfo.Player, "Dungeon", null) || BaseExtensions.InZone(spawnInfo.Player, "Meteor", null) || spawnInfo.Lihzahrd))
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
	}
}
