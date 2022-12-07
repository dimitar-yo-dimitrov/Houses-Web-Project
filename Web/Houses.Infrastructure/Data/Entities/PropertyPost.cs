//using System.ComponentModel.DataAnnotations.Schema;

//namespace Houses.Infrastructure.Data.Entities
//{
//    public class PropertyPost
//    {
//        public PropertyPost()
//        {
//            PropertyId = new Guid().ToString();
//            PostId = new Guid().ToString();
//        }

//        public string PropertyId { get; set; }

//        [ForeignKey(nameof(PropertyId))]
//        public virtual Property Property { get; set; } = null!;

//        public string PostId { get; set; }

//        [ForeignKey(nameof(PostId))]
//        public virtual Post Post { get; set; } = null!;
//    }
//}
