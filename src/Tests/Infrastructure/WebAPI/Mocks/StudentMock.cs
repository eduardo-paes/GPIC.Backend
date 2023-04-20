using Moq;
using Domain.Entities;
using Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests.Infrastructure.WebAPI.Mocks
{
    public static class StudentMock
    {
        public static Mock<Student> GetValidMock()
        {
            var mock = new Mock<Student>();
            mock.SetupAllProperties();

            mock.Object.BirthDate = new DateTime(2000, 1, 1);
            mock.Object.RG = 123456789;
            mock.Object.IssuingAgency = "SSP";
            mock.Object.DispatchDate = new DateTime(2015, 1, 1);
            mock.Object.Gender = EGender.F;
            mock.Object.Race = ERace.White;
            mock.Object.HomeAddress = "123 Main St";
            mock.Object.City = "New York";
            mock.Object.UF = "NY";
            mock.Object.CEP = 12345678;
            mock.Object.PhoneDDD = 11;
            mock.Object.Phone = 12345678;
            mock.Object.CellPhoneDDD = 11;
            mock.Object.CellPhone = 123456789;

            return mock;
        }

        public static Mock<Student> GetInvalidMock()
        {
            var mock = new Mock<Student>();
            mock.SetupAllProperties();

            mock.Object.BirthDate = DateTime.Now;
            mock.Object.RG = 0;
            mock.Object.IssuingAgency = null;
            mock.Object.DispatchDate = DateTime.Now;
            mock.Object.Gender = null;
            mock.Object.Race = null;
            mock.Object.HomeAddress = null;
            mock.Object.City = null;
            mock.Object.UF = null;
            mock.Object.CEP = 0;
            mock.Object.PhoneDDD = -1;
            mock.Object.Phone = -1;
            mock.Object.CellPhoneDDD = -1;
            mock.Object.CellPhone = -1;

            return mock;
        }
    }
}