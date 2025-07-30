using Domain.Enums;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DTOs
{
    public class SubjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int WeeklyClasses { get; set; }
        public bool IsMandatory { get; set; }
        public Semester Semester { get; set; }
        public EvaluationMethod EvaluationMethod { get; set; } 
        public DepartmentDto Department { get; set; } = null!;

        public ICollection<TeacherDto> Teachers { get; set; } = new List<TeacherDto>();
        public ICollection<SubjectDto> Prerequisites { get; set; } = new List<SubjectDto>();
        public ICollection<GradingCriteriaDto> GradingCriteria { get; set; } = new List<GradingCriteriaDto>();
        public ICollection<LiteratureDto> Literature { get; set; } = new List<LiteratureDto>();
    }
}
