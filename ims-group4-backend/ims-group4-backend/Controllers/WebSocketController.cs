
using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ims_group4_backend.Controllers{
    
    public class WebSocketController : ControllerBase {
        private static WebSocket? appSocket;
        private static WebSocket? mowerSocket;

        [Route("/ws/{type}")]
        public async Task Get(string type)
        {
            
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                switch (type)
                {
                    case "app":
                        if(appSocket != null){
                            await appSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                            Console.WriteLine("Closed old app socket");
                        }
                            
                        appSocket = webSocket;
                        await Echo(appSocket);
                        appSocket = null;
                        Console.WriteLine("Closed old app socket");
                        break;
                    case "mower":
                        if(mowerSocket != null){
                            await mowerSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                            Console.WriteLine("Closed old mower socket");
                        }

                        mowerSocket = webSocket;
                        await Echo(mowerSocket);
                        mowerSocket = null;
                        Console.WriteLine("Closed old mower socket");
                        break;
                    default:
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                        break;
                }
                
                
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        private static async Task Echo(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);
            
            while (!receiveResult.CloseStatus.HasValue)
            {
                string bufferStr = Encoding.UTF8.GetString(buffer);
                Console.WriteLine(bufferStr);

                if(webSocket == appSocket && mowerSocket != null){
                    await mowerSocket.SendAsync(
                        new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                        receiveResult.MessageType,
                        receiveResult.EndOfMessage,
                        CancellationToken.None);
                } else if(webSocket == mowerSocket && appSocket != null){
                    await appSocket.SendAsync(
                        new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                        receiveResult.MessageType,
                        receiveResult.EndOfMessage,
                        CancellationToken.None);
                }
                
                receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(
                receiveResult.CloseStatus.Value,
                receiveResult.CloseStatusDescription,
                CancellationToken.None
            );

        }

    }

}



