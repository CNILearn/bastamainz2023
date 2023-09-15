using Microsoft.Extensions.Logging;

namespace LoggingSample;

internal class Runner
{
    private readonly ILogger _logger;

    public Runner(ILogger<Runner> logger)
    {
        _logger = logger;
    }
    public void Run()
    {
        _logger.GameStarted("a new game");
        _logger.MismatchMoveNumber(4, 5);
        _logger.MoveWithInvalidGuesses(4, 5);
        _logger.SetMove("black red blue green", "black white");
    }
}
