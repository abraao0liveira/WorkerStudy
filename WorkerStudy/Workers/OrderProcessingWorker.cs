
using WorkerStudy.Services;

namespace WorkerStudy.Workers
{
    // Versão 1 - Usando Delay para controle de intervalo
    public class OrderProcessingWorker : BackgroundService
    {
        private readonly ILogger<OrderProcessingWorker> _logger;
        public OrderProcessingWorker(ILogger<OrderProcessingWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("OrderProcessingWorker running"); // Loga a hora atual

                await Task.Delay(5000, stoppingToken); // Aguarda por 5 segundos antes de processar novamente
            }
        }
    }

    // Versão 2 - Usando Timer e PeriodicTimer para controle de intervalo
    public class OrderProcessingWorkerV2 : BackgroundService
    {
        private readonly ILogger<OrderProcessingWorker> _logger;
        public OrderProcessingWorkerV2(ILogger<OrderProcessingWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TimeSpan interval = TimeSpan.FromSeconds(10); // Define o intervalo de 10 segundos
            using PeriodicTimer timer = new PeriodicTimer(interval); // Cria um timer periódico

            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken)) // Aguarda o próximo tick do timer
            {
                _logger.LogInformation($"OrderProcessingWorker running at: {DateTime.Now.TimeOfDay}"); // Loga a hora atual
            }
        }
    }

    // Versão 3 - Usando Timer, PeriodicTimer e Injeção de Dependência para enviar email
    public class OrderProcessingWorkerV3 : BackgroundService
    {
        private readonly ILogger<OrderProcessingWorker> _logger;
        private readonly IServiceProvider _provider;
        public OrderProcessingWorkerV3(ILogger<OrderProcessingWorker> logger, IServiceProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TimeSpan interval = TimeSpan.FromSeconds(10); // Define o intervalo de 10 segundos
            using PeriodicTimer timer = new PeriodicTimer(interval); // Cria um timer periódico

            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken)) // Aguarda o próximo tick do timer
            {
                _logger.LogInformation($"OrderProcessingWorker running at: {DateTime.Now.TimeOfDay}"); // Loga a hora atual

                using var scope = _provider.CreateScope(); // Cria um escopo de serviço

                var service = scope.ServiceProvider.GetRequiredService<IEmailService>(); // Resolve o serviço de email

                await service.SendEmailAsync("teste@mail.com", "subject", "body"); // Envia um email
            }
        }
    }
}
