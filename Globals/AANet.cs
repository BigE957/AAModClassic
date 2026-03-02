
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Events;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic.Globals
{
    public class AANet
    {
        public const byte SummonNPCFromClient = 0;
	    public const byte UpdateLovecraftianCount = 1;
        public const byte GenOre = 2;
        public const byte DD2EventTime = 3;

        public static bool DEBUG = true;

        public static void HandlePacket(BinaryReader bb, int whoAmI)
        {
            byte msg = bb.ReadByte();
            
			if (DEBUG)
            {
                string mode = Main.netMode == NetmodeID.Server ? "--SERVER-- " : "--CLIENT-- ";
                AAMod.instance.Logger.Info($"{mode} HANDLING MESSAGE: {msg}");
            }

            try
			{
				if (msg == SummonNPCFromClient)
				{
					if (Main.netMode == NetmodeID.Server)
					{
						int playerID = bb.ReadByte();
						int bossType = bb.ReadShort();
						bool spawnMessage = bb.ReadBool();
						int npcCenterX = bb.ReadInt();
						int npcCenterY = bb.ReadInt();
						string overrideDisplayName = bb.ReadString();
						bool namePlural = bb.ReadBool();

						AAModGlobalNPC.SpawnBoss(Main.player[playerID], bossType, spawnMessage, new Vector2(npcCenterX, npcCenterY), overrideDisplayName, namePlural);
					}
				}
                else if (msg == UpdateLovecraftianCount)
				{
					LovecraftianCount(bb, whoAmI);
				}
                else if (msg == DD2EventTime)
                {
                    DD2Event.TimeLeftBetweenWaves = bb.ReadByte();
                }
            }
            catch (Exception e)
            {
                string mode = Main.netMode == NetmodeID.Server ? "--SERVER-- " : "--CLIENT-- ";

                AAMod.instance.Logger.Error($"{mode} ERROR HANDLING MSG: {msg}: {e}");
                AAMod.instance.Logger.Info(e.StackTrace);
                AAMod.instance.Logger.Info("-------");
            }
		}

        private static void LovecraftianCount(BinaryReader reader, int fromWho)
        {
            int whichSquidX = reader.ReadByte();
            switch (whichSquidX)
            {
                case 1:
                    AAWorld.squid1 += 1;
                    break;

                case 2:
                    AAWorld.squid2 += 1;
                    break;

                case 3:
                    AAWorld.squid3 += 1;
                    break;

                case 4:
                    AAWorld.squid4 += 1;
                    break;

                case 5:
                    AAWorld.squid5 += 1;
                    break;

                case 6:
                    AAWorld.squid6 += 1;
                    break;

                case 7:
                    AAWorld.squid7 += 1;
                    break;

                case 8:
                    AAWorld.squid8 += 1;
                    break;

                case 9:
                    AAWorld.squid9 += 1;
                    break;

                case 10:
                    AAWorld.squid10 += 1;
                    break;

                case 11:
                    AAWorld.squid11 += 1;
                    break;

                case 12:
                    AAWorld.squid12 += 1;
                    break;

                case 13:
                    AAWorld.squid13 += 1;
                    break;

                case 14:
                    AAWorld.squid14 += 1;
                    break;

                case 16:
                    AAWorld.squid15 += 1;
                    break;

                case 17:
                    AAWorld.squid16 += 1;
                    break;
            }
        }

        private static void RabbitCount(BinaryReader reader, int fromWho)
        {
            int RabbitKills = reader.ReadByte();
            if (RabbitKills == 1)
            {
                AAWorld.RabbitKills += 1;
            }
            else if (RabbitKills == 2)
            {
                AAWorld.RabbitKills = 0;
            }
        }

        public static void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            if (DEBUG)
            {
                string mode = Main.netMode == NetmodeID.Server ? "--SERVER-- " : "--CLIENT-- ";
                AAMod.instance.Logger.Info($"{mode} SYNC PLAYER CALLED! NEWPLAYER: {newPlayer}. TOWHO:{toWho}. FROMWHO:{fromWho}");
            }

            if (Main.netMode == NetmodeID.Server && (toWho > -1 || fromWho > -1))
            {
                PlayerConnected(toWho == -1 ? fromWho : toWho);
            }
        }

        public static void PlayerConnected(int playerID)
        {
            if (DEBUG)
            {
                AAMod.instance.Logger.Info("--SERVER-- PLAYER JOINED!");
            }
        }

        public static void SendNetMessage(int msg, params object[] param)
        {
            SendNetMessageClient(msg, -1, param);
        }

        public static void SendNetMessageClient(int msg, int client, params object[] param)
        {
            try
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    return;
                }

                BaseNet.WriteToPacket(AAMod.instance.GetPacket(), (byte)msg, param).Send(client);
            }
            catch (Exception e)
            {
                string mode = Main.netMode == NetmodeID.Server ? "--SERVER-- " : "--CLIENT-- ";
                AAMod.instance.Logger.Error($"{mode} ERROR SENDING MSG: {msg}: {e.Message}");
                AAMod.instance.Logger.Info(e.StackTrace);
                AAMod.instance.Logger.Info("-------");

                string param2 = "";
                for (int m = 0; m < param.Length; m++)
                {
                    param2 += param[m];
                }

                AAMod.instance.Logger.Info("PARAMS: " + param2);
                AAMod.instance.Logger.Info("-------");
            }
        }
    }
}