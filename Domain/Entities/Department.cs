using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Enums;

namespace Domain.Entities
{
    public class Department : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string DepartmentName { get; private set; }

        [Required]
        public Guid HeadTeacherId { get; private set; }
        public Guid Id { get; private set; }
        public Teacher HeadTeacher { get; private set; }

        private readonly List<Subject> _subjects = new();
        public IReadOnlyCollection<Subject> Subjects => _subjects.AsReadOnly();

        private Department() { } 

        public Department(Guid id, string departmentName, Guid headTeacherId)
        {
            Id = id;
            SetDepartmentName(departmentName);
            HeadTeacherId = headTeacherId;
        }

        public void SetDepartmentName(string departmentName)
        {
            if (string.IsNullOrWhiteSpace(departmentName))
                throw new ArgumentException("Department name cannot be empty.");
            DepartmentName = departmentName;
            UpdateTimestamp();
        }

        public void SetHeadTeacher(Guid headTeacherId)
        {
            HeadTeacherId = headTeacherId;
            UpdateTimestamp();
        }
    }
}
