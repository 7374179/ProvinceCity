using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProvinceCity.Models;

namespace ProvinceCity.Data;

public static class SeedData {
    // this is an extension method to the ModelBuilder class
    public static void Seed(this ModelBuilder modelBuilder) {
        modelBuilder.Entity<Province>().HasData(
            GetProvinces()
        );
        modelBuilder.Entity<City>().HasData(
            GetCities()
        );
    }
    
        public static List<City> GetCities() {
        List<City> cities = new List<City>() {
            new City {
                CityId = 1,
                CityName = "Vancouver",
                Population = 3,
                ProvinceCode = "BC"
            },
            new City {
                CityId = 2,
                CityName = "Victoria",
                Population = 3,
                ProvinceCode = "BC"
            },
            new City {
                CityId = 3,
                CityName = "Kelowna",
                Population = 3,
                ProvinceCode = "BC"
            },
            new City {
                CityId = 4,
                CityName = "Toronto",
                Population = 3,
                ProvinceCode = "ON"
            },
            new City {
                CityId = 5,
                CityName = "Ottawa",
                Population = 3,
                ProvinceCode = "ON"
            },
            new City {
                CityId = 6,
                CityName = "Mississauga",
                Population = 3,
                ProvinceCode = "ON"
            },
            new City {
                CityId = 7,
                CityName = "Montreal",
                Population = 3,
                ProvinceCode = "QC"
            },
            new City {
                CityId = 8,
                CityName = "Quebec City",
                Population = 3,
                ProvinceCode = "QC"
            },
            new City {
                CityId = 9,
                CityName = "Laval",
                Population = 3,
                ProvinceCode = "QC"
            },
        };

        return cities;
    }

    public static List<Province> GetProvinces() {
        List<Province> provinces = new List<Province>() {
            new Province() {    // 1
                ProvinceCode="BC",
                ProvinceName="British Columbia ",
                // Cities=new List<City>() {
                //     new City() { CityName = "Vancouver"},
                //     new City() { CityName = "Victoria"},
                //     new City() { CityName = "Surrey"},
                // }
            },
            new Province() {    //2
                ProvinceCode="ON",
                ProvinceName="Ontario",
                // Cities=new List<City>() {
                //     new City() { CityName = "Toronto"},
                //     new City() { CityName = "Ottawa"},
                //     new City() { CityName = "Mississauga"},
                // }
            },
            new Province() {    // 3
                ProvinceCode="QC",
                ProvinceName="Quebec",
                // Cities=new List<City>() {
                //     new City() { CityName = "Montreal"},
                //     new City() { CityName = "Quebec"},
                //     new City() { CityName = "Laval"},
                // }
            },
        };

        return provinces;
    }

}
