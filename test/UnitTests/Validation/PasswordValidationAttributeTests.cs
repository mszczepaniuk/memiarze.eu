using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using memiarzeEu.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using memiarzeEu.Models;

namespace UnitTests.Validation
{
    public class PasswordValidationAttributeTests
    {
        private IdentityOptions allOptions;
        private IdentityOptions emptyOptions;

        public PasswordValidationAttributeTests()
        {
            allOptions = new IdentityOptions();
            allOptions.Password.RequiredUniqueChars = 3;

            emptyOptions = new IdentityOptions();
            emptyOptions.Password.RequiredUniqueChars = 0;
            emptyOptions.Password.RequireDigit = false;
            emptyOptions.Password.RequireLowercase = false;
            emptyOptions.Password.RequiredLength = 0;
            emptyOptions.Password.RequireNonAlphanumeric = false;
            emptyOptions.Password.RequireUppercase = false;
        }

        [Fact]
        public void EmptyPassword_AllOptions_NotValid()
        {
            PasswordOptionsStore.IdentityOptions = allOptions;
            string password = "";
            var mockAttribute = new PasswordValidationAttribute();

            Assert.True(!mockAttribute.IsValid(password));
        }

        [Fact]
        public void EmptyPassword_EmptyOptions_Valid()
        {
            PasswordOptionsStore.IdentityOptions = emptyOptions;
            string password = "";
            var mockAttribute = new PasswordValidationAttribute();

            Assert.True(mockAttribute.IsValid(password));
        }

        [Fact]
        public void EmptyPassword_AllOptions_AllErrorMessages()
        {
            PasswordOptionsStore.IdentityOptions = allOptions;
            string password = "";
            var mockAttribute = new PasswordValidationAttribute();

            mockAttribute.IsValid(password);

            string allErrorsMessage = "";
            allErrorsMessage += ("Hasło powinno posiadać przynajmniej jedną cyfrę." + Environment.NewLine);
            allErrorsMessage += ("Hasło powinno posiadać przynajmniej jeden znak niealfanumeryczny." + Environment.NewLine);
            allErrorsMessage += ("Hasło powinno posiadać przynajmniej jedną małą litere." + Environment.NewLine);
            allErrorsMessage += ("Hasło powinno posiadać przynajmniej jedną wielką litere." + Environment.NewLine);
            allErrorsMessage += ("Hasło jest za krótkie." + Environment.NewLine);
            allErrorsMessage += ($"Hasło powinno posiadać przynajmniej {allOptions.Password.RequiredUniqueChars} unikalnych znaków." + Environment.NewLine);

            Assert.Equal(allErrorsMessage, mockAttribute.ErrorMessage);
        }

        [Fact]
        public void ProperPassword_AllOptions_Valid()
        {
            PasswordOptionsStore.IdentityOptions = allOptions;
            string password = "Abcde_6";
            var mockAttribute = new PasswordValidationAttribute();

            Assert.True(mockAttribute.IsValid(password));
        }
    }
}
