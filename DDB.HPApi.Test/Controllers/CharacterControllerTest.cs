using DDB.HPApi.Controllers;
using DDB.HPApi.Enums;
using DDB.HPApi.Exceptions;
using DDB.HPApi.Models;
using DDB.HPApi.Models.RequestData;
using DDB.HPApi.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace DDB.HPApi.Test.Controllers
{
    public class CharacterControllerTest
    {
        private Mock<ICharacterService> MockService;
        private Mock<ILogger<CharactersController>> MockLogger;
        private CharactersController Controller;

        public CharacterControllerTest()
        {
            MockService = new Mock<ICharacterService>();
            MockLogger = new Mock<ILogger<CharactersController>>();
            Controller = new CharactersController(MockService.Object, MockLogger.Object);
        }

        [Fact]
        public async void GetCharacter_ReturnsOkResultWithCharacterObject()
        {
            // Arrange
            Guid Id = Guid.NewGuid();
            Character TestCharacter = new Character() { Id = Id };
            MockService.Setup(m => m.GetCharacter(Id)).ReturnsAsync(TestCharacter);

            // Act
            var result = await Controller.GetCharacter(Id);
            var testResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(testResult);
            Assert.IsType<OkObjectResult>(testResult);
            Assert.NotNull(testResult.Value);
            Assert.IsType<Character>(testResult.Value);
            Assert.Equal(testResult.Value, TestCharacter);
        }

        [Fact]
        public async void GetCharacter_WithUnknownId_ReturnsNotFound()
        {
            // Arrange
            Guid Id = Guid.NewGuid();
            Guid Id2 = Guid.NewGuid();
            Character TestCharacter = new Character() { Id = Id };
            CharacterWithIdNotFound TestException = new CharacterWithIdNotFound(Id2);
            MockService.Setup(m => m.GetCharacter(Id)).ReturnsAsync(TestCharacter);
            MockService.Setup(m => m.GetCharacter(Id2)).ThrowsAsync(TestException);

            // Act
            var result = await Controller.GetCharacter(Id2);
            var testResult = result as NotFoundObjectResult;

            //Assert
            Assert.NotNull(testResult);
            Assert.NotNull(testResult.Value);
            Assert.IsType<string>(testResult.Value);
            Assert.Equal($"Character with id '{Id2}' was not found", testResult.Value);
        }

        [Fact]
        public async void GetCharacters_ReturnsOkResultWithCharactersEnumerable()
        {
            // Arrange
            Guid Id1 = Guid.NewGuid();
            Guid Id2 = Guid.NewGuid();
            IEnumerable<Character> TestCharacters = new[] { new Character() { Id = Id1 }, new Character() { Id = Id2 } };
            MockService.Setup(m => m.GetCharacters()).ReturnsAsync(TestCharacters);

            // Act
            var result = await Controller.GetCharacters();
            var testResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(testResult);
            Assert.NotNull(testResult.Value);
            Assert.IsAssignableFrom<IEnumerable<Character>>(testResult.Value);
            Assert.Equal(testResult.Value, TestCharacters);
        }

        [Fact]
        public async void DamageCharacter_ReturnsOkResultWithCharacterObject()
        {

            // Arrange
            Guid Id = Guid.NewGuid();
            Character TestCharacter = new Character() { Id = Id, HitPoints = 10 };
            MockService.Setup(m => m.DamageCharacter(Id, It.IsAny<DamageTypes>(), It.IsAny<int>()))
                .ReturnsAsync(TestCharacter);

            // Act
            var result = await Controller.DamageCharacter(Id, new Damage()
            {
                DamageType = DamageTypes.Bludgeoning,
                Value = 5
            });
            var testResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(testResult);
            Assert.NotNull(testResult.Value);
            Assert.IsType<Character>(testResult.Value);
            Assert.Equal(testResult.Value, TestCharacter);
        }

        [Fact]
        public async void DamageCharacter_WithNegativeDamage_ReturnsBadResult()
        {

            // Arrange
            Guid Id = Guid.NewGuid();
            Character TestCharacter = new Character() { Id = Id, HitPoints = 10 };
            MockService.Setup(m => m.DamageCharacter(Id, It.IsAny<DamageTypes>(), It.IsAny<int>()))
                .ReturnsAsync(TestCharacter);

            // Act
            var result = await Controller.DamageCharacter(Id, new Damage()
            {
                DamageType = DamageTypes.Bludgeoning,
                Value = -5
            });
            var testResult = result as BadRequestObjectResult;

            //Assert
            Assert.NotNull(testResult);
            Assert.NotNull(testResult.Value);
            Assert.IsType<string>(testResult.Value);
            Assert.Equal("Damage value must be a positive integer", testResult.Value);
        }

        [Fact]
        public async void DamageCharacter_WithUnknownId_ReturnsNotFound()
        {
            // Arrange
            Guid Id = Guid.NewGuid();
            Guid Id2 = Guid.NewGuid();
            Character TestCharacter = new Character() { Id = Id };
            CharacterWithIdNotFound TestException = new CharacterWithIdNotFound(Id2);
            MockService.Setup(m => m.DamageCharacter(Id, It.IsAny<DamageTypes>(), It.IsAny<int>()))
                .ReturnsAsync(TestCharacter);
            MockService.Setup(m => m.DamageCharacter(Id2, It.IsAny<DamageTypes>(), It.IsAny<int>()))
                .ThrowsAsync(TestException);

            // Act
            var result = await Controller.DamageCharacter(Id2, new Damage()
            {
                DamageType = DamageTypes.Bludgeoning,
                Value = 5
            });
            var testResult = result as NotFoundObjectResult;

            //Assert
            Assert.NotNull(testResult);
            Assert.NotNull(testResult.Value);
            Assert.IsType<string>(testResult.Value);
            Assert.Equal($"Character with id '{Id2}' was not found", testResult.Value);
        }

        [Fact]
        public async void HealCharacter_ReturnsOkResultWithCharacterObject()
        {

            // Arrange
            Guid Id = Guid.NewGuid();
            Character TestCharacter = new Character() { Id = Id, HitPoints = 10 };
            MockService.Setup(m => m.HealCharacter(Id, It.IsAny<int>())).ReturnsAsync(TestCharacter);

            // Act
            var result = await Controller.HealCharacter(Id, new Heal() { Value = 5 });
            var testResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(testResult);
            Assert.NotNull(testResult.Value);
            Assert.IsType<Character>(testResult.Value);
            Assert.Equal(testResult.Value, TestCharacter);
        }

        [Fact]
        public async void HealCharacter_WithNegativeValue_ReturnsBadResult()
        {

            // Arrange
            Guid Id = Guid.NewGuid();
            Character TestCharacter = new Character() { Id = Id, HitPoints = 10 };
            MockService.Setup(m => m.HealCharacter(Id, It.IsAny<int>())).ReturnsAsync(TestCharacter);

            // Act
            var result = await Controller.HealCharacter(Id, new Heal()
            {
                Value = -5
            });
            var testResult = result as BadRequestObjectResult;

            //Assert
            Assert.NotNull(testResult);
            Assert.NotNull(testResult.Value);
            Assert.IsType<string>(testResult.Value);
            Assert.Equal("Heal value must be a positive integer", testResult.Value);
        }

        [Fact]
        public async void HealCharacter_WithUnknownId_ReturnsNotFound()
        {
            // Arrange
            Guid Id = Guid.NewGuid();
            Guid Id2 = Guid.NewGuid();
            Character TestCharacter = new Character() { Id = Id };
            CharacterWithIdNotFound TestException = new CharacterWithIdNotFound(Id2);
            MockService.Setup(m => m.HealCharacter(Id, It.IsAny<int>())).ReturnsAsync(TestCharacter);
            MockService.Setup(m => m.HealCharacter(Id2, It.IsAny<int>())).ThrowsAsync(TestException);

            // Act
            var result = await Controller.HealCharacter(Id2, new Heal()
            {
                Value = 5
            });
            var testResult = result as NotFoundObjectResult;

            //Assert
            Assert.NotNull(testResult);
            Assert.NotNull(testResult.Value);
            Assert.IsType<string>(testResult.Value);
            Assert.Equal($"Character with id '{Id2}' was not found", testResult.Value);
        }

        [Fact]
        public async void TempHealCharacter_ReturnsOkResultWithCharacterObject()
        {

            // Arrange
            Guid Id = Guid.NewGuid();
            Character TestCharacter = new Character() { Id = Id, HitPoints = 10 };
            MockService.Setup(m => m.TempHealCharacter(Id, It.IsAny<int>())).ReturnsAsync(TestCharacter);

            // Act
            var result = await Controller.TempHealCharacter(Id, new Heal() { Value = 5 });
            var testResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(testResult);
            Assert.NotNull(testResult.Value);
            Assert.IsType<Character>(testResult.Value);
            Assert.Equal(testResult.Value, TestCharacter);
        }

        [Fact]
        public async void TempHealCharacter_WithNegativeValue_ReturnsBadResult()
        {

            // Arrange
            Guid Id = Guid.NewGuid();
            Character TestCharacter = new Character() { Id = Id, HitPoints = 10 };
            MockService.Setup(m => m.TempHealCharacter(Id, It.IsAny<int>())).ReturnsAsync(TestCharacter);

            // Act
            var result = await Controller.TempHealCharacter(Id, new Heal()
            {
                Value = -5
            });
            var testResult = result as BadRequestObjectResult;

            //Assert
            Assert.NotNull(testResult);
            Assert.NotNull(testResult.Value);
            Assert.IsType<string>(testResult.Value);
            Assert.Equal("Temporary heal value must be a positive integer", testResult.Value);
        }

        [Fact]
        public async void TempHealCharacter_WithUnknownId_ReturnsNotFound()
        {
            // Arrange
            Guid Id = Guid.NewGuid();
            Guid Id2 = Guid.NewGuid();
            Character TestCharacter = new Character() { Id = Id };
            CharacterWithIdNotFound TestException = new CharacterWithIdNotFound(Id2);
            MockService.Setup(m => m.TempHealCharacter(Id, It.IsAny<int>())).ReturnsAsync(TestCharacter);
            MockService.Setup(m => m.TempHealCharacter(Id2, It.IsAny<int>())).ThrowsAsync(TestException);

            // Act
            var result = await Controller.TempHealCharacter(Id2, new Heal()
            {
                Value = 5
            });
            var testResult = result as NotFoundObjectResult;

            //Assert
            Assert.NotNull(testResult);
            Assert.NotNull(testResult.Value);
            Assert.IsType<string>(testResult.Value);
            Assert.Equal($"Character with id '{Id2}' was not found", testResult.Value);
        }
    }
}
