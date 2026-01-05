namespace WorkerStudy.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string body)
        {
            Console.WriteLine($"Send email to: {to}");

            return Task.CompletedTask; // Simula o envio de email
        }
    }
}
