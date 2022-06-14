using EmanatTask.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmanatTask.Interfaces
{
    public interface IMovieService
    {
        Task<List<Search>> GetAll();
        Task<Movie> GetByImdbID(string imdbID);
    }
}
