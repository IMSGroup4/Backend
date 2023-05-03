
using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ims_group4_backend.Controllers{
    
    public class WebSocketController : ControllerBase {
        private static WebSocket? m_appSocket;
        private static WebSocket? m_mowerSocket;

        private readonly ILogger<WebSocketController> m_logger;

        public WebSocketController(ILogger<WebSocketController> logger)
        {
            m_logger = logger;
        }

        [Route("/ws/{type}")]
        public async Task get(string type)
        {
            try
            {
                
                if(m_appSocket != null && m_appSocket.State == WebSocketState.Aborted){
                    await m_appSocket.CloseAsync(
                        WebSocketCloseStatus.NormalClosure,
                        null,
                        CancellationToken.None
                    );

                    m_appSocket = null;
                }

                if(m_mowerSocket != null && m_mowerSocket.State == WebSocketState.Aborted){
                    await m_mowerSocket.CloseAsync(
                        WebSocketCloseStatus.NormalClosure,
                        null,
                        CancellationToken.None
                    );

                    m_mowerSocket = null;
                }

                if (HttpContext.WebSockets.IsWebSocketRequest)
                {
                    
                    using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                    switch (type)
                    {
                        case "app":
                            if(m_appSocket != null){
                                await m_appSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                                Console.WriteLine("Closed old app socket");
                                m_logger.LogInformation("Closed old app socket");
                            }
                                
                            m_appSocket = webSocket;
                            await Echo(m_appSocket, m_logger);
                            m_appSocket = null;
                            Console.WriteLine("Closed old app socket");
                            m_logger.LogInformation("Closed old app socket");
                            break;
                        case "mower":
                            if(m_mowerSocket != null){
                                await m_mowerSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                                Console.WriteLine("Closed old mower socket");
                                m_logger.LogInformation("Closed old mower socket");
                            }

                            m_mowerSocket = webSocket;
                            await Echo(m_mowerSocket, m_logger);
                            m_mowerSocket = null;
                            Console.WriteLine("Closed old mower socket");
                            m_logger.LogInformation("Closed old mower socket");
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
            catch (WebSocketException ex)
            {
                if (ex.WebSocketErrorCode == WebSocketError.InvalidState)
                {
                    Console.WriteLine("get() Exception message: " + ex.Message);
                    m_logger.LogInformation("get() Exception message: " + ex.Message);

                    
                }
                else
                {
                    throw;
                }
            }
            
        }

        private static async Task Echo(WebSocket webSocket, ILogger<object> logger)
        {
            try
            {
                var buffer = new byte[1024 * 4];
                var receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);
                
                while (!receiveResult.CloseStatus.HasValue)
                {

                    if(webSocket.State == WebSocketState.Aborted){
                        await webSocket.CloseAsync(
                            WebSocketCloseStatus.NormalClosure,
                            null,
                            CancellationToken.None
                        );
                        return;
                    }

                    string bufferStr = Encoding.UTF8.GetString(buffer);
                    Console.WriteLine(bufferStr);
                    logger.LogInformation(bufferStr);

                    if(webSocket.State == WebSocketState.Open){
                        if (webSocket == m_appSocket && m_mowerSocket != null && m_mowerSocket.State == WebSocketState.Open)
                        {
                            await m_mowerSocket.SendAsync(
                                new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                                receiveResult.MessageType,
                                receiveResult.EndOfMessage,
                                CancellationToken.None);
                        }
                        else if (webSocket == m_mowerSocket && m_appSocket != null && m_appSocket.State == WebSocketState.Open)
                        {
                            await m_appSocket.SendAsync(
                                new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                                receiveResult.MessageType,
                                receiveResult.EndOfMessage,
                                CancellationToken.None);
                        }
                    }
                    
                    receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                }

                await webSocket.CloseAsync(
                    receiveResult.CloseStatus.Value,
                    receiveResult.CloseStatusDescription,
                    CancellationToken.None
                );
            }
            catch (WebSocketException ex)
            {
                if (ex.WebSocketErrorCode == WebSocketError.InvalidState)
                {
                    Console.WriteLine("Echo() Exception message: " + ex.Message);
                    logger.LogInformation("Echo() Exception message: " + ex.Message);

                    if (webSocket.State == WebSocketState.Aborted)
                    {
                        await webSocket.CloseAsync(
                            WebSocketCloseStatus.NormalClosure,
                            null,
                            CancellationToken.None
                        );
                    }
                }
                else
                {
                    throw;
                }
            }
            

        }

    }

}


// if(webSocket == m_appSocket && m_mowerSocket != null){
//                     await m_mowerSocket.SendAsync(
//                         new ArraySegment<byte>(buffer, 0, receiveResult.Count),
//                         receiveResult.MessageType,
//                         receiveResult.EndOfMessage,
//                         CancellationToken.None);
//                     } else if(webSocket == m_mowerSocket && m_appSocket != null){
//                         await m_appSocket.SendAsync(
//                         new ArraySegment<byte>(buffer, 0, receiveResult.Count),
//                         receiveResult.MessageType,
//                         receiveResult.EndOfMessage,
//                         CancellationToken.None);
//     }


