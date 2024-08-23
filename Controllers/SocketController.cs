public class SocketController
{
    private System.Timers.Timer _interval;
    private bool _running = false;
    private int _currentTime = 10 * 60; // 10 minutos
    private int _possessionTime = 24;
    private readonly Dictionary<string, Action> _commandHandlers;

    public SocketController()
    {
        _commandHandlers = new Dictionary<string, Action>
        {
            { "RemoveMinute", RemoveMinute },
            { "AddMinute", AddMinute },
            { "SetUpTimer", SetUpTimer },
            { "ResetPossessionTime", ResetPossessionTime },
            { "Apitar", Apitar },
            { "PlayPauseTimer", PlayPauseTimer }
        };
    }

    public void HandleServiceCall(string command)
    {
        if (_commandHandlers.TryGetValue(command, out var handler))
        {
            handler();
        }
        else
        {
            Console.WriteLine("Comando não reconhecido");
        }
    }

    public void SetUpTimer()
    {
        _running = false;
        _currentTime = 10 * 60; // 10 minutos
        _possessionTime = 24;

        EmitUpdatedInfo();

        _interval?.Stop();
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
            Console.WriteLine("Não foi possível concluir a operação, pois o tempo restante é menor do que um minuto");
        }
    }

    public void PlayPauseTimer()
    {
        if (!_running && _currentTime != 0)
        {
            _running = true;
            _interval = new System.Timers.Timer(1000);
            _interval.Elapsed += (sender, e) =>
            {
                _currentTime--;
                _possessionTime--;

                if (_currentTime == 0 || _possessionTime == 0)
                {
                    PlayPauseTimer();
                    _possessionTime = 24;
                }
                EmitUpdatedInfo();
            };
            _interval.Start();
        }
        else
        {
            _interval?.Stop();
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

    public void EmitUpdatedInfo()
    {
        Console.WriteLine($"Update: CurrentTime={_currentTime}, PossessionTime={_possessionTime}, IsRunning={_running}");
    }

    public void Apitar()
    {
        Console.WriteLine("Apito");
    }

    public async Task EmitMatchScoreboard(int partidaId)
    {
        try
        {
            // Simulate async operation
            var pontuacao = await Task.Run(() => EstatisticaController.CalculaPontuacaoPartida(partidaId));
            Console.WriteLine($"Emitindo pontuação: {pontuacao}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro ao emitir pontuação: " + ex.Message);
        }
    }

    public void Disconnect()
    {
        Console.WriteLine("Um usuário se desconectou...");
    }
}

// Supondo que EstatisticaController está definido em outro lugar
public static class EstatisticaController
{
    public static int CalculaPontuacaoPartida(int partidaId)
    {
        // Implementação fictícia
        return 42;
    }
}