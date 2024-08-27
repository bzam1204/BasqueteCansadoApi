using BasqueteCansadoApi.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/socket")]
public class SocketRoutesController : ControllerBase
{
    private readonly SocketController _socketController;

    public SocketRoutesController(SocketController socketController)
    {
        _socketController = socketController;
    }

    [HttpPost("{id}")]
    public IActionResult HandleServiceCall(int id)
    {
        try
        {
            _socketController.HandleServiceCall(id);
            return Ok(new { message = "Chamada de procedimento realizada com sucesso" });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Erro ao realizar chamada de procedimento" });
        }
    }
}














// using System;
// using System.Collections.Generic;
// using System.Net.WebSockets;
// using System.Text;
// using System.Threading;
// using System.Threading.Tasks;
//
// namespace BasqueteCansadoApi.Routes
// {
//     public class SocketRoutes
//     {
//         private readonly Dictionary<string, Func<WebSocket, string, Task>> _eventHandlers;
//
//         public SocketRoutes()
//         {
//             _eventHandlers = new Dictionary<string, Func<WebSocket, string, Task>>
//             {
//                 { "AddMinute", HandleAddMinuteAsync },
//                 { "RemoveMinute", HandleRemoveMinuteAsync },
//                 { "SetUpTimer", HandleSetUpTimerAsync },
//                 { "ResetPossessionTime", HandleResetPossessionTimeAsync },
//                 { "Apitar", HandleApitarAsync },
//                 { "PlayPauseTimer", HandlePlayPauseTimerAsync }
//             };
//         }
//
//         public async Task HandleWebSocketAsync(WebSocket webSocket)
//         {
//             var buffer = new byte[1024 * 4];
//             while (webSocket.State == WebSocketState.Open)
//             {
//                 var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
//                 if (result.MessageType == WebSocketMessageType.Text)
//                 {
//                     var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
//                     await RouteMessageAsync(webSocket, message);
//                 }
//                 else if (result.MessageType == WebSocketMessageType.Close)
//                 {
//                     await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client",
//                         CancellationToken.None);
//                 }
//             }
//         }
//
//         private async Task RouteMessageAsync(WebSocket webSocket, string message)
//         {
//             // Suponha que a mensagem seja no formato "EventName:Payload"
//             var parts = message.Split(':', 2);
//             if (parts.Length == 2 && _eventHandlers.TryGetValue(parts[0], out var handler))
//             {
//                 await handler(webSocket, parts[1]);
//             }
//             else
//             {
//                 Console.WriteLine("Evento não reconhecido");
//             }
//         }
//
//         private Task HandleAddMinuteAsync(WebSocket webSocket, string payload)
//         {
//             // Implementar lógica para adicionar minuto
//             Console.WriteLine("Adicionando minuto");
//             return Task.CompletedTask;
//         }
//
//         private Task HandleRemoveMinuteAsync(WebSocket webSocket, string payload)
//         {
//             // Implementar lógica para remover minuto
//             Console.WriteLine("Removendo minuto");
//             return Task.CompletedTask;
//         }
//
//         private Task HandleSetUpTimerAsync(WebSocket webSocket, string payload)
//         {
//             // Implementar lógica para remover minuto
//             Console.WriteLine("Iniciando o temporizador");
//             return Task.CompletedTask;
//         }
//
//         private Task HandleApitarAsync(WebSocket webSocket, string payload)
//         {
//             // Implementar lógica para remover minuto
//             Console.WriteLine("apitando...");
//             return Task.CompletedTask;
//         }
//
//         private Task HandlePlayPauseTimerAsync(WebSocket webSocket, string payload)
//         {
//             // Implementar lógica para remover minuto
//             Console.WriteLine("play/pause");
//             return Task.CompletedTask;
//         }
//
//         private Task HandleResetPossessionTimeAsync(WebSocket webSocket, string payload)
//         {
//             // Implementar lógica para remover minuto
//             Console.WriteLine("resetando os 24s");
//             return Task.CompletedTask;
//         }
//     }
// }