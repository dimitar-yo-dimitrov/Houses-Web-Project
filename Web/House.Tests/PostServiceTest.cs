namespace Houses.Tests
{
    [TestFixture]
    public class Tests
    {
        private IUserService _userService;
        private IPostService _postService;
        private ApplicationDbContext _dbContext;
        private IApplicationDbRepository _repository;

        const string propertyId = "7FC04F86-0B82-4AEC-B634-84A8E57381CB";
        const string postId = "79249221-890C-4C2A-AF82-D7C279C7A2BB";
        const string userId = "D5F34ADA-B2F3-4890-B6BA-C055EBC85703";

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Houses.bg")
                .Options;

            _dbContext = new ApplicationDbContext(contextOptions);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            _repository = new ApplicationDbRepository(_dbContext);
            _postService = new PostService(_repository, _userService);
            _userService = new UserService(_repository);
        }

        [Test]
        public async Task TestCreateAsync()
        {
            var content = "Test";

            var post = new Post
            {
                Sender = "Peter",
                Content = content,
                CreatedOn = DateTime.UtcNow,
                AuthorId = userId,
                PropertyId = propertyId
            };

            await _repository.AddAsync(post);
            await _repository.SaveChangesAsync();

            var posts = _repository.AllReadonly<Post>();

            Assert.That(posts.Count(), Is.EqualTo(expected: 1));
        }

        [Test]
        public async Task TestGetAllByUserIdAsync()
        {
            var content = "Test";

            Post post = new Post
            {
                Id = postId,
                Sender = "Peter",
                Content = content,
                AuthorId = userId,
                PropertyId = propertyId
            };

            await _repository.AddAsync(post);
            await _repository.SaveChangesAsync();

            var result = _postService.GetAllByIdAsync(userId);

            Assert.That(result.Result.Count(), Is.EqualTo(expected: 1));
        }

        [Test]
        public async Task TestDeletePostAsync()
        {
            var content = "Test";

            Post post = new Post
            {
                Id = postId,
                Sender = "Peter",
                Content = content,
                AuthorId = userId,
                PropertyId = propertyId,
                IsActive = true
            };

            await _repository.AddAsync(post);
            await _repository.SaveChangesAsync();

            await _postService.ExistsAsync(postId);

            var postById = _repository.GetByIdAsync<Post>(postId);

            postById.Result.IsActive = false;

            await _repository.SaveChangesAsync();

            var result = _postService.GetAllByIdAsync(userId);

            Assert.That(result.Result.Count(), Is.EqualTo(expected: 0));
        }

        [Test]
        public async Task TestGetPostAsync()
        {
            var content = "Test";

            Post post = new Post
            {
                Id = postId,
                Sender = "Peter",
                Content = content,
                AuthorId = userId,
                PropertyId = propertyId
            };

            await _repository.AddAsync(post);
            await _repository.SaveChangesAsync();

            var result = _postService.GetPostAsync(postId);

            Assert.That(result.Result.Sender, Is.EqualTo(expected: "Peter"));
        }
    }
}