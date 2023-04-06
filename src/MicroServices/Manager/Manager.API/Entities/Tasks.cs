using Manager.API.Custom;
using System;
using System.ComponentModel.DataAnnotations;

namespace Manager.API.Entities
{
    public class Tasks
    {
        private readonly CustomValidate validate = new CustomValidate();

        [Required]
        [Range(10000,99999, ErrorMessage = "MemberId should be 5 digit number.")]
        public int MemberId { get; set; }

        [Required]
        public string MemberName { get; set; }

        [Required]
        public string TaskName { get; set; }

        [Required]
        public string Deliverables { get; set; }

        [Required]
        public string TaskStartDate { get; set; }

        private string _taskEndDate;
        [Required]
        public string TaskEndDate 
        {
            get { return _taskEndDate; } 
            set
            {
                var endDate = DateTime.Parse(value);

                var startDate = DateTime.Parse(TaskStartDate);

                if (endDate <= startDate)
                {
                    validate.IsValid(null);
                }
                else
                {
                    _taskEndDate = value;
                }

            }
        }
    }
}
