using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Model
{
    public class Teacher
    {
        #region system generated properties
        [Key]
        public Guid ID { get; set; }
        public DateTime AddedDT { get; set; }
        public DateTime? EditedDT { get; set; }
        #endregion

        
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(25, ErrorMessage = "First Name can't be longer than 25 characters")]
        [MinLength(2, ErrorMessage = "First Name can't be shorter than 2 characters")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(25, ErrorMessage = "Last Name can't be longer than 25 characters")]
        [MinLength(2, ErrorMessage = "Last Name can't be shorter than 2 characters")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [MaxLength(50, ErrorMessage = "Email can't be longer than 50 characters")]
        [MinLength(5, ErrorMessage = "Email can't be shorter than 5 characters")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Phone Number is required")]
        [MaxLength(12, ErrorMessage = "Phone Number can't be longer than 12 characters")]
        [MinLength(5, ErrorMessage = "Phone Number can't be shorter than 5 characters")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Address is required")]
        [MaxLength(50, ErrorMessage = "Address can't be longer than 50 characters")]
        [MinLength(5, ErrorMessage = "Address can't be shorter than 5 characters")]
        public string Address { get; set; }
        
    }
}
