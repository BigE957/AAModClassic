
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Globals
{
    public class AANet : ModSystem
    {
        private static readonly List<AAPacket> instances = [];
        private static readonly Dictionary<Type, byte> typeToId = [];

        public override void PostSetupContent()
        {
            var packets = Mod.GetContent<AAPacket>();

            foreach (var p in packets)
            {
                p.MessageType = instances.Count;
                typeToId[p.GetType()] = (byte)instances.Count;
                instances.Add(p);
            }
        }

        public override void Unload()
        {
            instances.Clear();
            typeToId.Clear();
        }

        public static void HandlePacket(BinaryReader bb, int sender)
        {
            byte msg = bb.ReadByte();

            AAMod.instance.Logger.Info($"[AANet] Received msg id {msg} from {sender}");

            if (msg >= instances.Count)
            {
                AAMod.instance.Logger.Warn("Recieved packet with an invalid msg id of " + msg);
                return;
            }

            try
			{
                instances[msg].HandlePacket(bb, sender);
            }
            catch (Exception e)
            {
                string mode = Main.netMode == NetmodeID.Server ? "--SERVER-- " : "--CLIENT-- ";

                AAMod.instance.Logger.Error($"{mode} ERROR HANDLING MSG: {msg}: {e}");
                AAMod.instance.Logger.Info(e.StackTrace);
                AAMod.instance.Logger.Info("-------");
            }
		}

        public static void SendNetMessage<T>(params object[] param) where T : AAPacket
        {
            SendNetMessageClient<T>(-1, param);
        }

        public static void SendNetMessageClient<T>(int client, params object[] param) where T : AAPacket
        {
            if (!typeToId.TryGetValue(typeof(T), out byte msg))
            {
                AAMod.instance.Logger.Warn($"[AANet] No packet ID registered for {typeof(T).Name}");
                return;
            }

            try
            {
                instances[msg].Send(client, param);
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

    public abstract class AAPacket : ILoadable
    {
        public virtual void Load(Mod mod) { }

        public virtual void Unload() { }

        public int MessageType = -1;

        public abstract void HandlePacket(BinaryReader reader, int sender);

        // The "Internal" write logic
        protected abstract void Write(BinaryWriter writer, object[] args);

        // The clean helper for the caller
        public void Send(int toClient = -1, params object[] args)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                return;

            ModPacket packet = AAMod.instance.GetPacket();
            packet.Write((byte)MessageType);
            Write(packet, args);
            packet.Send(toClient);
        }
    }

    public sealed class SummonNPCFromClient : AAPacket
    {
        protected override void Write(BinaryWriter w, object[] args)
        {
            w.Write((byte)args[0]);  // playerID
            w.Write((short)args[1]); // bossType
            w.Write((bool)args[2]);  // spawnMessage
            w.Write((int)args[3]);  // npcCenterX
            w.Write((int)args[4]); // npcCenterY
            w.Write((string)args[5]);  // overrideDisplayName
            w.Write((bool)args[6]);  // namePlural
        }

        public override void HandlePacket(BinaryReader packet, int sender)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                int playerID = packet.ReadByte();
                int bossType = packet.ReadInt16();
                bool spawnMessage = packet.ReadBoolean();
                int npcCenterX = packet.ReadInt32();
                int npcCenterY = packet.ReadInt32();
                string overrideDisplayName = packet.ReadString();
                bool namePlural = packet.ReadBoolean();

                AAModGlobalNPC.SpawnBoss(Main.player[playerID], bossType, spawnMessage, new Vector2(npcCenterX, npcCenterY), overrideDisplayName, namePlural);
            }
        }
    }

    public sealed class UpdateLovecraftianCount : AAPacket
    {
        protected override void Write(BinaryWriter w, object[] args)
        {
            w.Write(Convert.ToByte(args[0]));  // whichSquidX
        }

        public override void HandlePacket(BinaryReader packet, int sender)
        {
            int whichSquidX = packet.ReadByte();
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
    }
}