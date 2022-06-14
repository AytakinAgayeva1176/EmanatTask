using EmanatTask.Helpers;
using EmanatTask.Interfaces;
using EmanatTask.Models;
using System.IO;
using System.Threading.Tasks;

namespace EmanatTask.Services
{
    public class EmailService : IEmailService
    {
        readonly HttpClientHelper _clientHelper;
        readonly EmailHelper _emailHelper;
        public EmailService(HttpClientHelper clientHelper, EmailHelper emailHelper)
        {
            _clientHelper = clientHelper;
            _emailHelper = emailHelper;
        }

        public async Task SendEmailAsync(string emailAdress, string imdbID)
        {
            var movie = await _clientHelper.Send<Movie>("&i=" + imdbID);
            var subject = "Received Order";

            string templatePath = "..\\EmanatTask\\EmailTemplates\\Order.html";
            var templateText = File.ReadAllText(templatePath);
            var body = templateText.Replace($"{{{nameof(movie.Poster)}}}", movie.Poster)
                .Replace($"{{{nameof(movie.Title)}}}", movie.Title)
                .Replace($"{{{nameof(movie.Director)}}}", movie.Director)
                .Replace($"{{{nameof(movie.Writer)}}}", movie.Writer);

            await _emailHelper.Send(emailAdress, subject, body);
        }
    }
}
