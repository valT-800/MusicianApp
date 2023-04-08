using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicianApp.Models
{
 public class StatusModel
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string StatusName { get; set; }
    public string StatusDescription { get; set; }
  }
}
