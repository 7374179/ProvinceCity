using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProvinceCity.Models;

public class City
{
    public int CityId { get; set; }

    [Display(Name = "City")]
    public string? CityName { get; set; }
    public int Population { get; set; }

    [Required]
    public string? ProvinceCode { get; set; }
    

    [ForeignKey("ProvinceCode")]
    public Province? Province { get; set; }
}
