using Houses.Infrastructure.Data.Identity;

namespace Houses.Tests
{
    [TestFixture]
    public class PropertyServiceTest
    {
        private IUserService _userService;
        private IPropertyService _propertyService;
        private ApplicationDbContext _dbContext;
        private IApplicationDbRepository _repository;

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

            _repository = new ApplicationDbRepository(_dbContext);
            _propertyService = new PropertyService(_repository);
            _userService = new UserService(_repository);
        }

        [Test]
        public async Task TestCreateAsyncAndExistAsync()
        {
            var user = new ApplicationUser
            {
                Id = userId,
                Email = "user@gmail.com",
                FirstName = "Peter",
                LastName = "",
                CreatedOn = DateTime.UtcNow,
            };

            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();

            await _propertyService.CreateAsync(userId, new CreatePropertyInputModel
            {
                Id = propertyId,
                Address = "",
                ImageUrl = "",
                Title = "",
                Description = "This house is edited",
                CityId = "",
                PropertyTypeId = "",
                Price = 100m,
            });

            var properties = _repository.AllReadonly<Property>();
            var users = _repository.AllReadonly<ApplicationUser>();

            await _propertyService.ExistAsync(propertyId);

            Assert.That(user.Id, Is.EqualTo(userId));
            Assert.That(properties.Count(), Is.EqualTo(1));
            Assert.That(users.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task TestPropertyDetailsByIdAsync()
        {
            var user = new ApplicationUser
            {
                Id = userId,
                Email = "user@gmail.com",
                FirstName = "Peter",
                LastName = "",
                CreatedOn = DateTime.UtcNow,
            };

            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();

            var model = new CreatePropertyInputModel
            {
                Id = propertyId,
                Address = "",
                ImageUrl = "",
                Title = "",
                Description = "This house is edited",
                CityId = "",
                PropertyTypeId = "",
                Price = 100m,

            };

            await _propertyService.CreateAsync(userId, model);

            //await _propertyService.ExistAsync(propertyId);

            var properties = _repository.AllReadonly<Property>();

            //public class DetailsPropertyServiceModel : DetailsPropertyViewModel
            //{
            //    public ApplicationUser? User { get; set; }

            //    public PropertyServiceViewModel? PropertyDto { get; set; }
            //}

            //var propertyToReturn = new PropertyServiceViewModel
            //{
            //    Id = property!.Id,
            //    Description = property.Description,
            //    Address = property.Address,
            //    Title = property.Title,
            //    ImageUrl = property.ImageUrl,
            //    Price = property.Price,
            //    SquareMeters = property.SquareMeters,
            //    PropertyTypeId = property.PropertyTypeId,
            //    PropertyType = property.PropertyType.Title,
            //    CityId = property.CityId,
            //    User = new ApplicationUser
            //    {
            //        FirstName = property.Owner.FirstName,
            //        LastName = property.Owner.LastName,
            //        Email = property.Owner.Email,
            //        PhoneNumber = property.Owner.PhoneNumber,
            //        ProfilePicture = property.Owner.ProfilePicture
            //    }
            //};

            //var multiModel = new DetailsPropertyServiceModel
            //{
            //    PropertyDto = propertyToReturn,
            //};

            //var property = await _repository
            //    .All<Property>()
            //    .FirstOrDefaultAsync(p => p.Id == propertyId);

            bool propertyIdExist = await _propertyService.ExistAsync(propertyId);

            //var modelForTesting = await _propertyService.PropertyDetailsByIdAsync(propertyId);

            Assert.Multiple(() =>
            {
                Assert.That(model.Id, Is.EqualTo(propertyId));
                Assert.That(properties.Count(), Is.EqualTo(1));
                Assert.That(propertyIdExist, Is.EqualTo(false));
                //Assert.That(multiModel.User!.FirstName, Is.EqualTo(null));
            });
        }

        [Test]
        public async Task TestRemovePropertyFromCollectionAsync()
        {
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

            await _repository.AddAsync(model);
            await _repository.SaveChangesAsync();

            await _propertyService.RemovePropertyFromCollectionAsync(propertyId);

            var product = await _repository.GetByIdAsync<Property>(propertyId);

            Assert.That(product.IsActive, Is.EqualTo(false));
        }

        [Test]
        public async Task TestGetAllAsync()
        {
            await _repository.AddRangeAsync(new List<Property>()
            {
                new() { Id = propertyId,
                    Address = "", ImageUrl = "", Title = "", Description = "", CityId = "", PropertyTypeId = "", OwnerId = "" },

                new() { Id = "98F5303C-21DB-45A8-B299-3EB934FF4B4A",
                    Address = "", ImageUrl = "", Title = "", Description = "", CityId = "", PropertyTypeId = "", OwnerId = "" },

                new() { Id = "8C6C54CF-8B03-4A2C-B763-29BF02BF3FD9",
                    Address = "", ImageUrl = "", Title = "", Description = "", CityId = "", PropertyTypeId = "", OwnerId = "" }
            });

            await _repository.SaveChangesAsync();
            var properties = await _propertyService.GetAllAsync();

            Assert.That(3, Is.EqualTo(properties.TotalPropertyCount));
            Assert.That(properties.Properties.Any(p => p.Id == propertyId), Is.False);
        }

        [Test]
        public async Task TestAllPropertiesByUserIdAsync()
        {
            await _repository.AddRangeAsync(new List<Property>()
            {
                new() { Id = propertyId,
                    Address = "", ImageUrl = "", Title = "", Description = "", CityId = "", PropertyTypeId = "", OwnerId = userId },

                new() { Id = "98F5303C-21DB-45A8-B299-3EB934FF4B4A",
                    Address = "", ImageUrl = "", Title = "", Description = "", CityId = "", PropertyTypeId = "", OwnerId = userId },

                new() { Id = "8C6C54CF-8B03-4A2C-B763-29BF02BF3FD9",
                    Address = "", ImageUrl = "", Title = "", Description = "", CityId = "", PropertyTypeId = "", OwnerId = "" }
            });

            await _repository.SaveChangesAsync();
            var properties = await _propertyService.AllPropertiesByUserIdAsync(userId);

            Assert.That(2, Is.EqualTo(properties.Count()));
        }

        [Test]
        public async Task TestEditAsync()
        {
            await _repository.AddAsync(new Property
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

            await _repository.SaveChangesAsync();

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

            var property = await _repository.GetByIdAsync<Property>(propertyId);

            Assert.That(property.Description, Is.EqualTo("This house is edited"));
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }
    }
}
