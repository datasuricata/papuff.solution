//using Microsoft.AspNetCore.SignalR;
//using System;
//using System.Threading.Tasks;

//namespace papuff.webapi.Hubs {
//    public class NotifyHub : Hub {

//        /// <summary>
//        /// join into group for city general view
//        /// </summary>
//        /// <param name="cityId"></param>
//        /// <returns></returns>
//        public Task JoinCity(string cityId) 
//            => Groups.AddToGroupAsync(Context.ConnectionId, cityId);

//        /// <summary>
//        /// join into ticket for general view by group ticket
//        /// </summary>
//        /// <param name="siegeId"></param>
//        /// <returns></returns>
//        public Task JoinGroup(string siegeId) 
//            => Groups.AddToGroupAsync(Context.ConnectionId, siegeId);

//        /// <summary>
//        /// send message for all clients connecteds
//        /// </summary>
//        /// <param name="message"></param>
//        /// <returns></returns>
//        public Task MessageForAll(string message) 
//            => Clients.All.SendAsync("ReceiveMessage", message);

//        /// <summary>
//        /// send message for caller connected
//        /// </summary>
//        /// <param name="message"></param>
//        /// <returns></returns>
//        public Task MessageToCaller(string message) 
//            => Clients.Caller.SendAsync("ReceiveMessage", message);

//        /// <summary>
//        /// send message for user connected
//        /// </summary>
//        /// <param name="connectionId"></param>
//        /// <param name="message"></param>
//        /// <returns></returns>
//        public Task MessageToUser(string connectionId, string message) 
//            => Clients.Client(connectionId).SendAsync("ReceiveMessage", message);

//        /// <summary>
//        /// info when a new user connect into hub
//        /// </summary>
//        /// <returns></returns>
//        public override async Task OnConnectedAsync() {
//            await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
//            await base.OnConnectedAsync();
//        }

//        /// <summary>
//        /// info when a current user disconnect from hub
//        /// </summary>
//        /// <param name="ex"></param>
//        /// <returns></returns>
//        public override async Task OnDisconnectedAsync(Exception ex) {
//            await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
//            await base.OnDisconnectedAsync(ex);
//        }
//    }
//}
