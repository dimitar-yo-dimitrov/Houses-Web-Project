namespace Houses.Tests
{
    [TestFixture]
    public class PropertyServiceTest
    {
        private IPropertyService _propertyService;
        private ApplicationDbContext _dbContext;

        const string propertyId = "7FC04F86-0B82-4AEC-B634-84A8E57381CB";
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
        }

        [Test]
        public async Task TestCreateAsyncAndExistAsync()
        {
            var repo = new ApplicationDbRepository(_dbContext);
            _propertyService = new PropertyService(repo);

            await _propertyService.CreateAsync(propertyId, new CreatePropertyInputModel
            {
                Id = propertyId,
                Address = "",
                ImageUrl = "",
                Title = "",
                Description = "This house is edited",
                CityId = "",
                PropertyTypeId = "",
                Price = 100m
            });

            var properties = repo.All<Property>();

            await _propertyService.ExistAsync(propertyId);

            Assert.That(properties.Count(), Is.EqualTo(1));
        }

        //[Test]
        //public async Task TestPropertyDetailsByIdAsync()
        //{
        //    var repo = new ApplicationDbRepository(_dbContext);
        //    _propertyService = new PropertyService(repo);

        //    var userId = "D5F34ADA-B2F3-4890-B6BA-C055EBC85703";
        //    var propertyId = "7FC04F86-0B82-4AEC-B634-84A8E57381CB";


        //    await _propertyService.PropertyDetailsByIdAsync("7FC04F86-0B82-4AEC-B634-84A8E57381CB");

        //    Assert.AreEqual("Test", model.Description);
        //}

        [Test]
        public async Task TestRemovePropertyFromCollectionAsync()
        {
            var repo = new ApplicationDbRepository(_dbContext);
            _propertyService = new PropertyService(repo);

            var model = new Property
            {
                Id = propertyId,
                Address = "",
                ImageUrl = "",
                Title = "",
                Description = "",
                CityId = "",
                PropertyTypeId = "",
                OwnerId = "",
                IsActive = true,
            };

            await repo.AddAsync(model);
            await repo.SaveChangesAsync();

            await _propertyService.RemovePropertyFromCollectionAsync(propertyId);

            var product = await repo.GetByIdAsync<Property>(propertyId);

            Assert.That(product.IsActive, Is.EqualTo(false));
        }

        [Test]
        public async Task TestGetAllAsync()
        {
            var repo = new ApplicationDbRepository(_dbContext);
            _propertyService = new PropertyService(repo);

            await repo.AddRangeAsync(new List<Property>()
            {
                new() { Id = propertyId,
                    Address = "", ImageUrl = "", Title = "", Description = "", CityId = "", PropertyTypeId = "", OwnerId = "" },

                new() { Id = "98F5303C-21DB-45A8-B299-3EB934FF4B4A",
                    Address = "", ImageUrl = "", Title = "", Description = "", CityId = "", PropertyTypeId = "", OwnerId = "" },

                new() { Id = "8C6C54CF-8B03-4A2C-B763-29BF02BF3FD9",
                    Address = "", ImageUrl = "", Title = "", Description = "", CityId = "", PropertyTypeId = "", OwnerId = "" }
            });

            await repo.SaveChangesAsync();
            var properties = await _propertyService.GetAllAsync();

            Assert.That(3, Is.EqualTo(properties.TotalPropertyCount));
            Assert.That(properties.Properties.Any(p => p.Id == propertyId), Is.False);
        }

        [Test]
        public async Task TestAllPropertiesByUserIdAsync()
        {
            var repo = new ApplicationDbRepository(_dbContext);
            _propertyService = new PropertyService(repo);

            await repo.AddRangeAsync(new List<Property>()
            {
                new() { Id = propertyId,
                    Address = "", ImageUrl = "", Title = "", Description = "", CityId = "", PropertyTypeId = "", OwnerId = userId },

                new() { Id = "98F5303C-21DB-45A8-B299-3EB934FF4B4A",
                    Address = "", ImageUrl = "", Title = "", Description = "", CityId = "", PropertyTypeId = "", OwnerId = userId },

                new() { Id = "8C6C54CF-8B03-4A2C-B763-29BF02BF3FD9",
                    Address = "", ImageUrl = "", Title = "", Description = "", CityId = "", PropertyTypeId = "", OwnerId = "" }
            });

            await repo.SaveChangesAsync();
            var properties = await _propertyService.AllPropertiesByUserIdAsync(userId);

            Assert.That(2, Is.EqualTo(properties.Count()));
        }

        [Test]
        public async Task TestEditAsync()
        {
            var repo = new ApplicationDbRepository(_dbContext);
            _propertyService = new PropertyService(repo);

            await repo.AddAsync(new Property
            {
                Id = propertyId,
                Address = "",
                ImageUrl = "",
                Title = "",
                Description = "",
                CityId = "",
                PropertyTypeId = "",
                OwnerId = "",
            });

            await repo.SaveChangesAsync();

            await _propertyService.EditAsync(propertyId, new CreatePropertyInputModel
            {
                Id = propertyId,
                Address = "",
                ImageUrl = "",
                Title = "",
                Description = "This house is edited",
                CityId = "",
                PropertyTypeId = "",
            });

            var property = await repo.GetByIdAsync<Property>(propertyId);

            Assert.That(property.Description, Is.EqualTo("This house is edited"));
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }
    }
}
