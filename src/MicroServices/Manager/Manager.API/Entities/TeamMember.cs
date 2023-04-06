using Manager.API.Custom;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Manager.API.Entities
{
    public class TeamMember
    {
        private readonly CustomValidate validate = new CustomValidate();
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("Id")]
        public string Id { get; set; }

        [Required]
        [Key]
        [BsonElement("MemberId")]
        [Range(10000, 99999, ErrorMessage = "Member Id should be 5 digit")]
        public int MemberId { get; set; }

        [Required]
        [BsonElement("MemberName")]
        public string MemberName { get; set; }

        [Required]
        [BsonElement("Experience")]
        [Range(4, float.MaxValue)]
        public float Experience { get; set; }

        private List<string> _skillSet;

        [Required]
        [BsonElement("SkillSet")]
        [CustomValidate(ErrorMessage = "Minimum skill set should be 3")]
        public List<string> SkillSet
        {
            get
            {
                return _skillSet;
            }
            set
            {
                if (value.Count < 3)
                {
                    validate.IsValid(null);
                }
                else
                {
                    _skillSet = value;
                }
            }
        }

        [Required]
        [BsonElement("CurrentProfileDescription")]
        [MaxLength(300)]
        public string CurrentProfileDescription { get; set; }

        [Required]
        [BsonElement("ProjectStartDate")]
        public string ProjectStartDate { get; set; }

        private string _projectEndDate;
        [BsonElement("ProjectEndDate")]
        [CustomValidate(ErrorMessage = "Project end date should be greater than project start date")]
        public string ProjectEndDate
        {
            get
            {
                return _projectEndDate;
            }
            set
            {
                var endDate = DateTime.Parse(value);

                var startDate = DateTime.Parse(ProjectStartDate);

                if (endDate <= startDate)
                {
                    validate.IsValid(null);
                }
                else
                {
                    _projectEndDate = value;
                }

            }
        }

        [Required]
        [BsonElement("AllocationPercentage")]
        [Range(0,100)]
        public int AllocationPercentage { get; set; }
    }
}
