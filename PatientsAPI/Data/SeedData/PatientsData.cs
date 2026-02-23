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
                    LastName = "Martin",
                    FirstName = "Sophie",
                    DateOfBirth = new DateTime(1966, 12, 31),
                    Gender = GenderEnum.Female,
                    City = "Villeurbanne",
                    Phone = "04 78 01 23 45"
                },
                new()
                {
                    Id = 2,
                    LastName = "Durand",
                    FirstName = "Jean",
                    DateOfBirth = new DateTime(1945, 6, 24),
                    Gender = GenderEnum.Male,
                    City = "Lyon",
                    Phone = "04 72 34 56 78"
                },
                new()
                {
                    Id = 3,
                    LastName = "Moreau",
                    FirstName = "Lucas",
                    DateOfBirth = new DateTime(2004, 6, 18),
                    Gender = GenderEnum.Male,
                    City = "Lyon",
                    Phone = "04 78 56 78 90"
                },
                new()
                {
                    Id = 4,
                    LastName = "Lefevre",
                    FirstName = "Emma",
                    DateOfBirth = new DateTime(2002, 6, 28),
                    Gender = GenderEnum.Female,
                    City = "Vénissieux",
                    Phone = "04 71 23 45 67"
                }
        );
    }
}
