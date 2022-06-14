using System.Threading.Tasks;

namespace EmanatTask.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string emailAdress,  string imdbID);
    }
}
