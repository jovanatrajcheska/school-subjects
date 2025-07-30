using Domain.Entities;
using System;
using System.Security.Cryptography;

namespace Domain.Builders
{
    public class DepartmentBuilder
    {
        private string _departmentName = string.Empty;
        private Guid _headTeacherId;
        private Guid _id = Guid.Empty;

        public DepartmentBuilder WithName(string name)
        {
            _departmentName = name;
            return this;
        }

        public DepartmentBuilder WithHeadTeacherId(Guid headTeacherId)
        {
            _headTeacherId = headTeacherId;
            return this;
        }
        public DepartmentBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }
        public Department Build()
        {
            return new Department(_id, _departmentName, _headTeacherId);
        }
    }
}
