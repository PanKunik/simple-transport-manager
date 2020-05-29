using System;
using System.ComponentModel.DataAnnotations;

namespace STMConsoleUI.Library.BusinessLogic
{
    public class Driver
    {
        public static int freeId { get; private set; } = 0; // TODO: change

        [Required(ErrorMessage = "Id of the driver is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Id of the driver must be integer number greater than 0.")]
        public int Id { get; set; } = 1;

        [Required(ErrorMessage = "First name of driver is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 30 letters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name of driver is required.")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 40 letters.")]
        public string LastName { get; set; }

        public Driver()
        {
            Id = ++freeId;
        }

        public Driver(string firstName, string lastName)
        {
            Id = ++freeId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
