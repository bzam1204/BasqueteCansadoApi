using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BasqueteCansadoApi.Controllers
{
    public class SocketController
    {
        private Timer _interval;
        private bool _running = false;
        private int _currentTime = 10 * 60; // 10 minutos
        private int _possessionTime = 24;
        private readonly IHubContext<NotificationHub> _hubContext;

        public SocketController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public void SetUpTimer()
        {
            _running = false;
            _currentTime = 10 * 60;
            _possessionTime = 24;

            EmitUpdatedInfo();

            _interval?.Dispose();
        }

        public void AddMinute()
        {
            _currentTime += 60;
            EmitUpdatedInfo();
        }

        public void RemoveMinute()
        {
            if (_currentTime >= 60)
            {
                _currentTime -= 60;
                EmitUpdatedInfo();
            }
            else
            {
                _hubContext.Clients.All.SendAsync("alert",
                    new
                    {
                        type = "error",
                        message = "Não foi possível concluir a operação, pois o tempo restante é menor do que um minuto"
                    });
            }
        }

        public void PlayPauseTimer()
        {
            if (!_running && _currentTime != 0)
            {
                _running = true;
                _interval = new Timer(_ =>
                {
                    _currentTime--;
                    _possessionTime--;

                    if (_currentTime == 0 || _possessionTime == 0)
                    {
                        PlayPauseTimer();
                        _possessionTime = 24;
                    }

                    EmitUpdatedInfo();
                }, null, 0, 1000);
            }
            else
            {
                _interval?.Dispose();
                _running = false;
                EmitUpdatedInfo();
            }
        }

        public void ResetPossessionTime()
        {
            if (_running && _currentTime > 24)
            {
                _possessionTime = 24;
                EmitUpdatedInfo();
            }
            else if (_running && _currentTime < 24)
            {
                _possessionTime = _currentTime;
                EmitUpdatedInfo();
            }
        }

        private void EmitUpdatedInfo()
        {
            _hubContext.Clients.All.SendAsync("update",
                new { currentTime = _currentTime, possessionTime = _possessionTime, isRunning = _running });
        }

        public void Apitar()
        {
            _hubContext.Clients.All.SendAsync("apito");
        }

        public async Task EmitMatchScoreboard(int partidaId)
        {
            try
            {
                // Simulate async operation
                var pontuacao = await Task.Run(() => EstatisticaController.CalculaPontuacaoPartida(partidaId));
                await _hubContext.Clients.All.SendAsync("pontuacao", new { pontuacao });
                Console.WriteLine("Emitindo pontuação");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao emitir pontuação: " + ex.Message);
            }
        }

        public void HandleServiceCall(int id)
        {
            switch (id)
            {
                case 11:
                    RemoveMinute();
                    break;
                case 13:
                    AddMinute();
                    break;
                case 14:
                    SetUpTimer();
                    break;
                case 15:
                    ResetPossessionTime();
                    break;
                case 16:
                    Apitar();
                    break;
                case 17:
                    PlayPauseTimer();
                    break;
                default:
                    Console.WriteLine("Evento não reconhecido");
                    break;
            }
        }

        public void Disconnect()
        {
            Console.WriteLine("Um usuário se desconectou...");
        }
    }

    public static class EstatisticaController
    {
        public static int CalculaPontuacaoPartida(int partidaId)
        {
            // Implementação fictícia
            return 42;
        }
    }
}