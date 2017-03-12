using Microsoft.AspNet.SignalR;
using WebServer.Models;

namespace WebServer.Hubs
{
    public class ChatHub : Hub
    {
        // Отправка сообщений
        public void Send(ChatMessage msg)
        {
            Clients.All.broadcastMessage(msg);
        }

        //// Подключение нового пользователя
        //public void Connect(string userName)
        //{
        //    var id = Context.ConnectionId;


        //    if (Users.All(x => x.ConnectionId != id))
        //    {
        //        Users.Add(new User { ConnectionId = id, Name = userName });

        //        // Посылаем сообщение текущему пользователю
        //        Clients.Caller.onConnected(id, userName, Users);

        //        // Посылаем сообщение всем пользователям, кроме текущего
        //        Clients.AllExcept(id).onNewUserConnected(id, userName);
        //    }
        //}

        //// Отключение пользователя
        //public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        //{
        //    var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
        //    if (item != null)
        //    {
        //        Users.Remove(item);
        //        var id = Context.ConnectionId;
        //        Clients.All.onUserDisconnected(id, item.Name);
        //    }

        //    return base.OnDisconnected(stopCalled);
        //}
    }
}