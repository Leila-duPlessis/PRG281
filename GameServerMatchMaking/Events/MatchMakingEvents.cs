using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerMatchMaking.Events
{
    public class MatchMakingEvents
    {

        // Custom Events to pass details when a match is found
        public class MatchFoundEvent : EventArgs
        {
            public string MatchId { get; }
            public string ServerIp { get; }
            public int PlayerCount { get; }

            public MatchFoundEvent(string matchId, string serverIp, int playerCount)
            {
                MatchId = matchId;
                ServerIp = serverIp;
                PlayerCount = playerCount;
            }
        }

        // Custom Evenst for system notifications (e.g. The warnings, queue updates etc)
        public class NotificationEvent : EventArgs
        {
            public string Message { get; }
            public DateTime Timestamp { get; }

            public NotificationEvent(string message)
            {
                Message = message;
                Timestamp = DateTime.Now;
            }
        }

        // Eventclass that holds and triggers the events
        public class MatchmakingEventManager
        {
            public event EventHandler<MatchFoundEvent> OnMatchFound;
            public event EventHandler<NotificationEvent> OnNotificationReceived;

            // Method to trigger the Match Found event
            public void MatchFound(string matchId, string serverIp, int playerCount)
            {
                OnMatchFound?.Invoke(this, new MatchFoundEvent(matchId, serverIp, playerCount));
            }

            // Method to trigger a notification
            public void Notification(string message)
            {
                OnNotificationReceived?.Invoke(this, new NotificationEvent(message));
            }
        }
    }
}
