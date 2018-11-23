using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Message
{
    // Place in Script/Game/Helpers folder
    public enum MessageType
    {
        DAMAGED,
        DEAD,
        RESPAWN
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IMessageReceiver
    {
        void OnReceiveMessage(MessageType type, object sender, object msg);
    }
}
