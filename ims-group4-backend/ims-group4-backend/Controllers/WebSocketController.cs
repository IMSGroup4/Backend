
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
                if (HttpContext.WebSockets.IsWebSocketRequest)
                {
                    
                    using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                    switch (type)
                    {
                        case "app":
                            if(m_appSocket != null){
                                await m_appSocket.CloseAsync(WebSocketCloseStatus.EndpointUnavailable, null, CancellationToken.None);
                                Console.WriteLine("Closed old app socket");
                            }
                            m_appSocket = webSocket;
                            await Echo(m_appSocket, m_logger);
                            m_appSocket = null;
                            Console.WriteLine("Closed old app socket");
                            break;
                        case "mower":
                            if(m_mowerSocket != null){
                                await m_mowerSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                                Console.WriteLine("Closed old mower socket");
                            }

                            m_mowerSocket = webSocket;
                            await Echo(m_mowerSocket, m_logger);
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
            catch (WebSocketException ex)
            {
                if (ex.WebSocketErrorCode == WebSocketError.InvalidState)
                {
                    Console.WriteLine("OUR MESSAGE: Exception message: " + ex.Message);
                    m_logger.LogInformation("OUR MESSAGE: 1, Exception message: " + ex.Message);
                    m_logger.LogInformation("OUR MESSAGE: 2, WebSocketErrorCode: " + ex.WebSocketErrorCode);
                }
                else
                {
                    m_logger.LogInformation("OUR MESSAGE: 3, Exception message: " + ex.Message);
                    m_logger.LogInformation("OUR MESSAGE: 4, WebSocketErrorCode: " + ex.WebSocketErrorCode);
                    //throw;
                }
            }
            
        }

        private static async Task Echo(WebSocket webSocket, ILogger<object> logger)
        {
            try
            {
                var buffer = new byte[1024 * 4];
                var receiveResult = new WebSocketReceiveResult(1, WebSocketMessageType.Text, false);
                try{
                    receiveResult = await webSocket.ReceiveAsync(
                        new ArraySegment<byte>(buffer), CancellationToken.None
                    );

                }catch(WebSocketException ex){
                    logger.LogInformation("OUR MESSAGE: 5, Exception message: " + ex.Message);
                    logger.LogInformation("OUR MESSAGE: 6, WebSocketErrorCode: " + ex.WebSocketErrorCode);
                    await webSocket.CloseAsync(
                        WebSocketCloseStatus.EndpointUnavailable,
                        "Unexpected websocket closure",
                        CancellationToken.None
                    );
                }
                
                while (!receiveResult.CloseStatus.HasValue)
                {
                    string bufferStr = Encoding.UTF8.GetString(buffer);
                    logger.LogInformation("OUR MESSAGE: AFTER CLOSING BEFOR SENDING MESSAGE");
                    if(webSocket.State == WebSocketState.Open){
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
                    }
                    
                    try {
                        receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    }catch{
                        logger.LogInformation("OUR MESSAGE: 7");
                        await webSocket.CloseAsync(
                            WebSocketCloseStatus.EndpointUnavailable,
                            "Unexpected websocket closure",
                            CancellationToken.None
                        );
                    }
                    
                }
                if(receiveResult.CloseStatus.HasValue){
                    logger.LogInformation("OUR MESSAGE: 8");
                    await webSocket.CloseAsync(
                        receiveResult.CloseStatus.Value,
                        receiveResult.CloseStatusDescription,
                        CancellationToken.None
                    );
                }else{
                    logger.LogInformation("OUR MESSAGE: 9");
                    await webSocket.CloseAsync(
                        WebSocketCloseStatus.EndpointUnavailable,
                        "Unexpected websocket closure",
                        CancellationToken.None
                    );
                }
            }
            catch (WebSocketException ex)
            {
                if (ex.WebSocketErrorCode == WebSocketError.InvalidState)
                {
                    Console.WriteLine("OUR MESSAGE: Exception message: " + ex.Message);
                    logger.LogInformation("OUR MESSAGE: 10, Exception message: " + ex.Message);
                    logger.LogInformation("OUR MESSAGE: 11, WebSocketErrorCode: " + ex.WebSocketErrorCode);
                }
                else
                {
                    logger.LogInformation("OUR MESSAGE: 12, Exception message: " + ex.Message);
                    logger.LogInformation("OUR MESSAGE: 13, WebSocketErrorCode: " + ex.WebSocketErrorCode);
                    //throw;
                }
            }
                //throw;
        }

    }

}



