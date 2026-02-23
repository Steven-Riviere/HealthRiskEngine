using System;
using Microsoft.EntityFrameworkCore;
using PatientsAPI.Models;
using Shared.Models;

namespace PatientsAPI.Data.SeedData;

public class PatientsData
{
    public static void SeedPatients(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>().HasData(
                new()
                {
                    Id = 1,
                    LastName = "TestNone",
                    FirstName = "Test",
                    DateOfBirth = new DateTime(1966, 12, 31),
                    Gender = GenderEnum.Female,
                    City = "Villeurbanne",
                    Phone = "100-222-3333"
                },
                new()
                {
                    Id = 2,
                    LastName = "TestBorderline",
                    FirstName = "Test",
                    DateOfBirth = new DateTime(1945, 6, 24),
                    Gender = GenderEnum.Male,
                    City = "Lyon",
                    Phone = "200-333-4444"
                },
                new()
                {
                    Id = 3,
                    LastName = "TestInDanger",
                    FirstName = "Test",
                    DateOfBirth = new DateTime(2004, 6, 18),
                    Gender = GenderEnum.Male,
                    City = "Lyon",
                    Phone = "300-444-5555"
                },
                new()
                {
                    Id = 4,
                    LastName = "TestEarlyOnset",
                    FirstName = "Test",
                    DateOfBirth = new DateTime(2002, 6, 28),
                    Gender = GenderEnum.Female,
                    City = "Vénissieux",
                    Phone = "400-555-6666"
                }
            );
    }
}
}
