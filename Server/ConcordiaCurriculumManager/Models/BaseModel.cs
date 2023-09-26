using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace ConcordiaCurriculumManager.Models;

/* Check https://www.npgsql.org/doc/types/datetime.html#timestamps-and-timezones
   to understand the restriction with having a timezone in the datetime
*/
public class BaseModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [NotMapped]
    private DateTime _createdDate = DateTime.UtcNow;

    [NotMapped]
    private DateTime _modifiedDate = DateTime.UtcNow;

    public DateTime CreatedDate
    {
        get { return _createdDate; }
        set
        {
            if (value.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("Created Date must have DateTimeKind.Utc");
            }

            _createdDate = value;
        }
    }

    public DateTime ModifiedDate
    {
        get { return _modifiedDate; }
        set
        {
            if (value.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("Modified Date must have DateTimeKind.Utc");
            }

            _modifiedDate = value;
        }
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
