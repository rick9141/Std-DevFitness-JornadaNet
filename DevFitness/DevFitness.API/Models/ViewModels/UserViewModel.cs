using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFitness.API.Models.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(int id, string fullName, decimal height, decimal weight, DateTime birthDate)
        {
            Id = id;
            FullName = fullName;
            Height = height;
            Weight = weight;
            BirthDate = birthDate;
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
