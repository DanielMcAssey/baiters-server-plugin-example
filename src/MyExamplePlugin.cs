using GLOKON.Baiters.Core;
using GLOKON.Baiters.Core.Enums.Networking;
using GLOKON.Baiters.Core.Models.Networking;
using GLOKON.Baiters.Core.Plugins;
using System.Numerics;
using System.Reflection;

namespace MyExamplePluginForBaiters
{
    public sealed class MyExamplePlugin(GameManager gm) : BaitersPlugin(
        gm,
        "My Example Plugin",
        "A plugin to show how to make a plugin!",
        "Daniel McAssey <dan@glokon.me>",
        Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0.0")
    {
        public override void OnInit()
        {
            // Use this to setup your plugin, called when the server is starting up, like hooking up to events
            base.OnInit();

            // Lets listen to some events
            GM.Server.OnPlayerJoined += Server_OnPlayerJoined;
            GM.Server.OnPlayerLeft += Server_OnPlayerLeft;
            GM.Server.OnChatMessage += Server_OnChatMessage;
            GM.Server.OnLobbyChatMessage += Server_OnLobbyChatMessage;
            GM.Server.OnTick += Server_OnTick;
            GM.Server.OnPacket += Server_OnPacket;

            // Lets listen to a custom chat command, exclude the prefix, as its set by the server admin (Defaults to "!")
            GM.Chat.ListenFor("my-cool-command", "My helpful text to explain my command", (fromSteamId, commandParams) =>
            {
                GM.Server.SendSystemMessage("This is a message I am sending", steamId: fromSteamId);

                GM.Server.SendPacket(new("my_cool_packet_two")
                {
                    ["pos"] = Vector3.Zero,
                    ["rot"] = Vector3.Zero,
                    ["potato"] = false,
                    ["plant_scale"] = 4.2f,
                }, DataChannel.Guitar);
            });
        }

        public override void OnDestroy()
        {
            // Use this to clean-up your plugin, called when the server is shutting down, like cleaning up events
            base.OnDestroy();

            // Lets cleanup the events
            GM.Server.OnPlayerJoined -= Server_OnPlayerJoined;
            GM.Server.OnPlayerLeft -= Server_OnPlayerLeft;
            GM.Server.OnChatMessage -= Server_OnChatMessage;
            GM.Server.OnLobbyChatMessage -= Server_OnLobbyChatMessage;
            GM.Server.OnTick -= Server_OnTick;
            GM.Server.OnPacket -= Server_OnPacket;

            // Stop listening to Chat Commands
            GM.Chat.StopListening("my-cool-command");
        }

        public override bool CanPlayerJoin(ulong steamId)
        {
            // Use this to check if a player can join, if you return false, the player is blocked from joining
            return base.CanPlayerJoin(steamId);
        }

        private void Server_OnPlayerJoined(ulong steamId)
        {
            // Called when a player has joined fully
        }

        private void Server_OnPlayerLeft(ulong steamId)
        {
            // Called when the player has left
        }

        private void Server_OnChatMessage(ulong fromSteamId, string message)
        {
            // Called when a new chat message was received
        }

        private void Server_OnLobbyChatMessage(ulong fromSteamId, string message)
        {
            // Called when theres a new message on the Lobby chat
        }

        private void Server_OnTick()
        {
            // Try not to do any intense work here, this is called on every server tick (By default 24 ticks per second)
        }

        private void Server_OnPacket(ulong fromSteamId, Packet packet)
        {
            // Called when the server receives a packet, this could happen very frequently

            // An example below of using a custom packet (For example from a mod)
            if (packet.Type == "my_cool_packet_type")
            {
                GM.Actioner.MovePlayer(fromSteamId, (Vector3)packet["pos"], (Vector3)packet["rot"]);

                GM.Spawner.SpawnFish();
                GM.Spawner.SpawnVoidPortal();

                foreach (var ownedActor in GM.Server.OwnedActors)
                {
                    // Lets be "wild" any ownerID above 1 million gets removed, becase we dont like big numbers ...right?
                    if (ownedActor.Value.OwnerId > 1_000_000)
                    {
                        // The key in OwnedActors and Actors is the ID
                        GM.Server.RemoveActor(ownedActor.Key);
                    }
                }
            }
        }
    }
}
