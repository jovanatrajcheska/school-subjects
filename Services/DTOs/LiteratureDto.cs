using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DTOs
{
    public class LiteratureDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;

        public ICollection<AuthorDto> Authors { get; set; } = new List<AuthorDto>();

        public List<LiteratureDto> Literature { get; set; } = new List<LiteratureDto>();

    }


}
