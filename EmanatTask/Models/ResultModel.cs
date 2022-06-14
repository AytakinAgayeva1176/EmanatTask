using System.Collections.Generic;

namespace EmanatTask.Models
{
    public class ResultModel
    {
        public List<Search> Search { get; set; }
        public string totalResults { get; set; }
        public string Response { get; set; }
    }
}
