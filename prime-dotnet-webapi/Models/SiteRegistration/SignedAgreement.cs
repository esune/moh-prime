using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("SignedAgreement")]
    public class SignedAgreement : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public Guid DocumentGuid { get; set; }

        public string FileName { get; set; }

        public int OrganizationId { get; set; }

        [JsonIgnore]
        public Organization Organization { get; set; }

        public DateTimeOffset UploadedDate { get; set; }
    }
}
