using Bpf.Api.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bpf.Api.Entities.{!AREA}
{
    [Table("utb{!FIELDS}", Schema = "{!AREA}")]
    public class {!FIELD} : Audit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int {!FIELD}Id { get; set; }

        [Required]
        [StringLength(50)]
        public string {!FIELD}Name { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        public bool IsSystemDefault { get; set; }

        [Required]
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
