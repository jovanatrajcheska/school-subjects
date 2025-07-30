using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DTOs
{
    public class TeacherDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public AcademicTitle Title { get; set; } 
        public string Email { get; set; } = string.Empty;

        public string FormattedName => Title == AcademicTitle.BSc
            ? $"{FirstName} {LastName}"
            : $"{Title}. {FirstName} {LastName}";

        public List<TeacherDto> Teachers { get; set; } = new List<TeacherDto>();
    }
}
