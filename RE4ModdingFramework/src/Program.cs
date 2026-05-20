using RE4ModdingFramework.src.Events;
using RE4ModdingFramework.src.Logging;

namespace RE4ModdingFramework.src
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player.PlayerJoined += OnPlayerJoined;
            Player.PlayerLeft += OnPlayerLeft;
            Player.PlayerDamaged += OnPlayerDamaged;

            var player = new Player();

            Player.PlayerJoined -= OnPlayerJoined;
            Player.PlayerLeft -= OnPlayerLeft;
            Player.PlayerDamaged -= OnPlayerDamaged;
        }

        private static void OnPlayerDamaged(OnPlayerDamagedEventArgs ev)
        {
            Log.Info($"Player Took: {ev.Damage} Damage, And current health is: {ev.Health}");
        }

        private static void OnPlayerLeft(OnPlayerLeftEventArgs ev)
        {
            Log.Info($"Player: {ev.PlayerName}, Has Left The Game!");
        }

        private static void OnPlayerJoined(OnPlayerJoinedEventArgs ev)
        {
            Log.Info($"Player: {ev.PlayerName}, Has Joined The Game!");
        }
    }
}
