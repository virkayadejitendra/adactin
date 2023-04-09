using System.ComponentModel.DataAnnotations.Schema;

namespace InsurenceAPI.Entities
{
    public class Occupation
    {
        public int Id { set; get; }
        public string Name { set; get; }
        [ForeignKey("RatingFactor")]
        public  int RatingId { set; get; }
         public virtual RatingFactor Rating { set; get; }
    }
}
