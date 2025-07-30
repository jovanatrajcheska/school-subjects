using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs
{
    public class GradingCriteriaDto
    {
        public Guid Id { get; set; }
        public int MinScore { get; set; }
        public int MaxScore { get; set; }
        public string GradeLabel { get; set; } = "";
    }
}
