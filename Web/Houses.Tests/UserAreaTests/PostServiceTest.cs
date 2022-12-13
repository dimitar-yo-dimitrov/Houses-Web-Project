//using Houses.Core.Services.Contracts;
//using Houses.Infrastructure.Data;
//using Houses.Infrastructure.Data.Common.Repositories;
//using Microsoft.EntityFrameworkCore;

//namespace Houses.Tests.UserAreaTests
//{
//    public class Tests
//    {
//        private ApplicationDbContext _context;
//        private IRepository _repository;
//        private IUserService _userService;
//        private IPostService _postService;

//        [SetUp]
//        public void Setup()
//        {
//            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
//                .UseInMemoryDatabase("PostDb")
//                .Options;

//            _context = new ApplicationDbContext(contextOptions);

//            _context.Database.EnsureDeleted();
//            _context.Database.EnsureCreated();

//            //_repository = new Repository<Post>(_context);
//            //_postService = new PostService(_repository);
//        }

//        [Test]
//        public void Test1()
//        {
//            Assert.Pass();
//        }
//    }
//}