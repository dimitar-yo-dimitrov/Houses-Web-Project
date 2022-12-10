//using System.ComponentModel.DataAnnotations.Schema;

//namespace Houses.Infrastructure.Data.Entities
//{
//    public class PropertyPost
//    {
//        public PropertyPost()
//        {
//            PropertyId = Guid.NewGuid().ToString();
//            PostId = Guid.NewGuid().ToString();
//        }

//        [ForeignKey(nameof(Property))]
//        public string PropertyId { get; set; }
//        public virtual Property Property { get; set; } = null!;

//        [ForeignKey(nameof(Post))]
//        public string PostId { get; set; }
//        public virtual Post Post { get; set; } = null!;
//    }
//}
