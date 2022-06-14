using EmanatTask.Helpers;
using EmanatTask.Interfaces;
using EmanatTask.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmanatTask.Services
{
    public class MovieService : IMovieService
    {
        readonly HttpClientHelper _clientHelper;
        public MovieService(HttpClientHelper clientHelper)
        {
            _clientHelper = clientHelper;
        }

        public async Task<List<Search>> GetAll()
        {
            var searchRes = await _clientHelper.Send<ResultModel>("s=Wonder");

            foreach (var item in searchRes.Search)
            {
                var movie = await _clientHelper.Send<Movie>("&i=" + item.imdbID);
                item.imdb = movie.imdbRating;
            }
            return searchRes.Search;
        }

        public async Task<Movie> GetByImdbID(string imdbID)
        {
            var movie = await _clientHelper.Send<Movie>("&i=" + imdbID);
            return movie;
        }
    }
}
