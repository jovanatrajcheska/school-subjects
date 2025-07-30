using Domain.Entities;
using Domain.Enums;
using System;

namespace Domain.Builders
{
    public class TeacherBuilder
    {
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private AcademicTitle _title;
        private string _email = string.Empty;

        public TeacherBuilder WithFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public TeacherBuilder WithLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        public TeacherBuilder WithTitle(AcademicTitle title)
        {
            _title = title;
            return this;
        }

        public TeacherBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public Teacher Build()
        {
            return new Teacher(_firstName, _lastName, _title, _email);
        }
    }
}
