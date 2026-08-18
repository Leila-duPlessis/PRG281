using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerMatchMaking.Exceptions
{
    internal class MatchMakingExceptions
    {
        // Thrown when a player tries to join a queue that has reached max capacity
        public class QueueFullException : Exception
        {
            public QueueFullException(string message) : base(message) { }
        }

        // Thrown when all available game servers are at 100% capacity
        public class ServerOverloadException : Exception
        {
            public ServerOverloadException(string message) : base(message) { }
        }

        // Thrown if a player attempts to enter the queue twice
        public class PlayerAlreadyInQueueException : Exception
        {
            public PlayerAlreadyInQueueException(string message) : base(message) { }
        }

        // Thrown if a player attempts to leave a queue they are not in
        public class PlayerNotInQueueException : Exception
        {
            public PlayerNotInQueueException(string message) : base(message) { }
        }
    }
}

