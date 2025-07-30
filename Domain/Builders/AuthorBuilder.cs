using Domain.Entities;
using System;

namespace Domain.Builders
{
    public class AuthorBuilder
    {
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;

        public AuthorBuilder WithFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public AuthorBuilder WithLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        public Author Build()
        {
            return new Author(_firstName, _lastName);
        }
    }
}
