using DDB.HPApi.Enums;
using DDB.HPApi.Models;
using DDB.HPApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.HPApi.Test.Services
{
    public class CharacterServiceHelperTest
    {
        #region AddTemporaryHitPoints
        [Fact]
        public void AddTemporaryHitPoints_ToCharacterWithNone()
        {
            // Arrange
            Character TestCharacter = new Character() { TempHitPoints = 0 };

            // Act
            TestCharacter.AddTemporaryHitPoints(5);

            // Assert
            Assert.Equal(5, TestCharacter.TempHitPoints);
        }

        [Fact]
        public void AddTemporaryHitPoints_ToCharacterWithSomeButLessThanCurrentAdd()
        {
            // Arrange
            Character TestCharacter = new Character() { TempHitPoints = 5 };

            // Act
            TestCharacter.AddTemporaryHitPoints(10);

            // Assert
            Assert.Equal(10, TestCharacter.TempHitPoints);
        }

        [Fact]
        public void AddTemporaryHitPoints_ToCharacterWithSomeButMoreThanCurrentAdd()
        {
            // Arrange
            Character TestCharacter = new Character() { TempHitPoints = 20 };

            // Act
            TestCharacter.AddTemporaryHitPoints(10);

            // Assert
            Assert.Equal(20, TestCharacter.TempHitPoints);
        }
        #endregion

        #region Heal
        [Fact]
        public void Heal_CharacterAtMaxHP()
        {
            // Arrange
            Character TestCharacter = new Character() { HitPoints = 10, CurrentHitPoints = 10 };

            // Act
            TestCharacter.Heal(10);

            // Assert
            Assert.Equal(10, TestCharacter.CurrentHitPoints);
        }

        [Fact]
        public void Heal_CharacterWithMorePointsThanNeeded()
        {
            // Arrange
            Character TestCharacter = new Character() { HitPoints = 10, CurrentHitPoints = 5 };

            // Act
            TestCharacter.Heal(10);

            // Assert
            Assert.Equal(10, TestCharacter.CurrentHitPoints);
        }

        [Fact]
        public void Heal_CharacterWithLessPointsThanNeeded()
        {
            // Arrange
            Character TestCharacter = new Character() { HitPoints = 10, CurrentHitPoints = 5 };

            // Act
            TestCharacter.Heal(2);

            // Assert
            Assert.Equal(7, TestCharacter.CurrentHitPoints);
        }
        #endregion

        #region Damage

        [Fact]
        public void Damage_CharacterLessThanTheirFullHP()
        {
            // Arrange
            Character TestCharacter = new Character() { HitPoints = 10, CurrentHitPoints = 10 };

            // Act
            TestCharacter.Damage(DamageTypes.Bludgeoning, 5);

            // Assert
            Assert.Equal(5, TestCharacter.CurrentHitPoints);
        }

        [Fact]
        public void Damage_CharacterMoreThanTheirFullHP()
        {
            // Arrange
            Character TestCharacter = new Character() { HitPoints = 10, CurrentHitPoints = 10 };

            // Act
            TestCharacter.Damage(DamageTypes.Bludgeoning, 20);

            // Assert
            Assert.Equal(0, TestCharacter.CurrentHitPoints);
        }

        [Fact]
        public void Damage_CharacterLessThanTheirTemporaryHP()
        {
            // Arrange
            Character TestCharacter = new Character() { HitPoints = 10, CurrentHitPoints = 10, TempHitPoints = 5 };

            // Act
            TestCharacter.Damage(DamageTypes.Bludgeoning, 3);

            // Assert
            Assert.Equal(2, TestCharacter.TempHitPoints);
            Assert.Equal(10, TestCharacter.CurrentHitPoints);
        }

        [Fact]
        public void Damage_CharacterMoreThanTheirTemporaryHP()
        {
            // Arrange
            Character TestCharacter = new Character() { HitPoints = 10, CurrentHitPoints = 10, TempHitPoints = 5 };

            // Act
            TestCharacter.Damage(DamageTypes.Bludgeoning, 10);

            // Assert
            Assert.Equal(0, TestCharacter.TempHitPoints);
            Assert.Equal(5, TestCharacter.CurrentHitPoints);
        }

        [Fact]
        public void Damage_CharacterWithAResistance()
        {
            // Arrange
            Character TestCharacter = new Character() 
            {
                HitPoints = 10, 
                CurrentHitPoints = 10,
                Defenses = new[]
                {
                    new DefenseType()
                    {
                        Type = DamageTypes.Bludgeoning,
                        Defense = DefensePotency.Resistance
                    }
                }
               
            };

            // Act
            TestCharacter.Damage(DamageTypes.Bludgeoning, 10);

            // Assert
            Assert.Equal(5, TestCharacter.CurrentHitPoints);
        }

        [Fact]
        public void Damage_CharacterWithAnImmunity()
        {
            // Arrange
            Character TestCharacter = new Character()
            {
                HitPoints = 10,
                CurrentHitPoints = 10,
                Defenses = new[]
                {
                    new DefenseType()
                    {
                        Type = DamageTypes.Bludgeoning,
                        Defense = DefensePotency.Immunity
                    }
                }

            };

            // Act
            TestCharacter.Damage(DamageTypes.Bludgeoning, 10);

            // Assert
            Assert.Equal(10, TestCharacter.CurrentHitPoints);
        }

        #endregion
    }
}
