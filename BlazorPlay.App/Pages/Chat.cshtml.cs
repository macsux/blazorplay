using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPlay.App.Services;
using BlazorPlay.Server;
using Microsoft.AspNetCore.Blazor.Components;

namespace BlazorPlay.App.Pages
{
    public class ChatModel : BlazorComponent
    {

        [Inject]
        public ChatServer ChatServer { get; set; }
        public List<ChatMsg> Messages { get; set; } = new List<ChatMsg>();
        public string MessageToSend { get; set; }
        public string Username { get; set; }
        public bool SendMsgDisabled => Username == null;
        
        protected override void OnParametersSet()
        {
            
            ChatServer.Join("default", msg =>
            {
                Messages.Add(msg);
                this.StateHasChanged();
            });
            
        }

        protected void SendMsg()
        {
            ChatServer.SendMessage("default", new ChatMsg(){Message = MessageToSend, Sender = Username, Timestamp = DateTime.Now});
        }
//
//        public void IncrementCount()
//        {
//            Counter.Count++;
//            this.StateHasChanged();
//        }
    }
}