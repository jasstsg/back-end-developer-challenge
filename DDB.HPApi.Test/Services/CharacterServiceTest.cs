using DDB.HPApi.Enums;
using DDB.HPApi.Models;
using DDB.HPApi.Repositories.Abstractions;
using DDB.HPApi.Services;
using Moq;

namespace DDB.HPApi.Test.Services
{
    public class CharacterServiceTest
    {
        private Mock<ICharacterRepository> MockRepo;
        private CharacterService service;

        public CharacterServiceTest()
        {
            MockRepo = new Mock<ICharacterRepository>();
            service = new CharacterService(MockRepo.Object);
        }

        [Fact]
        public async void GetCharacter_ReturnsCharacterObject()
        {
            // Arrange
            Guid Id = Guid.NewGuid();
            Character TestCharacter = new Character() { Id = Id };
            MockRepo.Setup(m => m.GetByIdAsync(Id)).ReturnsAsync(TestCharacter);

            // Act
            var result = await service.GetCharacter(Id);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Character>(result);
            Assert.Equal(result, TestCharacter);
        }

        [Fact]
        public async void GetCharacters_ReturnsCharacterObject()
        {
            // Arrange
            Guid Id1 = Guid.NewGuid();
            Guid Id2 = Guid.NewGuid();
            IEnumerable<Character> TestCharacters = new[] { new Character() { Id = Id1 }, new Character() { Id = Id2 } };
            MockRepo.Setup(m => m.GetAllAsync()).ReturnsAsync(TestCharacters);

            // Act
            var result = await service.GetCharacters();

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Character>>(result);
            Assert.Equal(result, TestCharacters);
        }

        [Fact]
        public async void DamageCharacter_ReturnsCharacterObject()
        {
            // Arrange
            Guid Id = Guid.NewGuid();
            Character TestCharacter = new Character() { Id = Id };
            MockRepo.Setup(m => m.GetByIdAsync(Id)).ReturnsAsync(TestCharacter);
            MockRepo.Setup(m => m.UpdateAsync(TestCharacter)).ReturnsAsync(TestCharacter);

            // Act
            var result = await service.DamageCharacter(Id, DamageTypes.Bludgeoning, 5);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Character>(result);
            Assert.Equal(TestCharacter, result);
        }

        [Fact]
        public async void HealCharacter_ReturnsCharacterObject()
        {
            // Arrange
            Guid Id = Guid.NewGuid();
            Character TestCharacter = new Character() { Id = Id };
            MockRepo.Setup(m => m.GetByIdAsync(Id)).ReturnsAsync(TestCharacter);
            MockRepo.Setup(m => m.UpdateAsync(TestCharacter)).ReturnsAsync(TestCharacter);

            // Act
            var result = await service.HealCharacter(Id, 5);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Character>(result);
            Assert.Equal(TestCharacter, result);
        }

        [Fact]
        public async void TempHealCharacter_ReturnsCharacterObject()
        {
            // Arrange
            Guid Id = Guid.NewGuid();
            Character TestCharacter = new Character() { Id = Id };
            MockRepo.Setup(m => m.GetByIdAsync(Id)).ReturnsAsync(TestCharacter);
            MockRepo.Setup(m => m.UpdateAsync(TestCharacter)).ReturnsAsync(TestCharacter);

            // Act
            var result = await service.TempHealCharacter(Id, 5);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Character>(result);
            Assert.Equal(TestCharacter, result);
        }
    }
}
