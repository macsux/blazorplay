using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace BlazorPlay.Server
{
    public class ChatServer
    {
        public ConcurrentDictionary<string, ChatRoom> _chatRooms = new ConcurrentDictionary<string, ChatRoom>();

        public IDisposable Join(string roomName, Action<ChatMsg> callback)
        {
            var chatRoom = _chatRooms.GetOrAdd(roomName, name => new ChatRoom());
            return chatRoom.Join(callback);
        }

        public void SendMessage(string roomName, ChatMsg msg)
        {
            var chatRoom = _chatRooms.GetOrAdd(roomName, name => new ChatRoom());
            chatRoom.Send(msg);
        }
    }

    public class ChatMsg
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class ChatRoom
    {
        private ISubject<ChatMsg> _chatBus = new Subject<ChatMsg>();

        public IDisposable Join(Action<ChatMsg> callback)
        {
            return _chatBus.Subscribe(callback);
        }

        public void Send(ChatMsg msg)
        {
            _chatBus.OnNext(msg);
        }
    }
}
