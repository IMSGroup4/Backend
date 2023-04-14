
using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ims_group4_backend.Controllers{
    
    public class WebSocketController : ControllerBase {
        private static WebSocket? m_appSocket;
        private static WebSocket? m_mowerSocket;

        [Route("/ws/{type}")]
        public async Task get(string type)
        {
            
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                switch (type)
                {
                    case "app":
                        if(m_appSocket != null){
                            await m_appSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                            Console.WriteLine("Closed old app socket");
                        }
                            
                        m_appSocket = webSocket;
                        await Echo(m_appSocket);
                        m_appSocket = null;
                        Console.WriteLine("Closed old app socket");
                        break;
                    case "mower":
                        if(m_mowerSocket != null){
                            await m_mowerSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                            Console.WriteLine("Closed old mower socket");
                        }

                        m_mowerSocket = webSocket;
                        await Echo(m_mowerSocket);
                        m_mowerSocket = null;
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

                if(webSocket == m_appSocket && m_mowerSocket != null){
                    await m_mowerSocket.SendAsync(
                        new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                        receiveResult.MessageType,
                        receiveResult.EndOfMessage,
                        CancellationToken.None);
                } else if(webSocket == m_mowerSocket && m_appSocket != null){
                    await m_appSocket.SendAsync(
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



