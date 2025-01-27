using AuthServiceBulgakov.Domain.ValueObjects;

namespace AuthServiceBulgakov.Tests.Domain.ValueObjects
{
    public class EmailTest
    {
        [Fact]
        public void Create_Email_Success()
        {
            //Arrange
            string email = "test@gmail.com";

            //Act
            var emailResult = Email.Create(email);

            //Assert
            Assert.True(emailResult.IsSuccess);
        }

        [Fact]
        public void Create_Email_Failure_By_Empty_Email()
        {
            //Arrange
            string email = "";

            //Act
            var emailResult = Email.Create(email);

            //Assert
            Assert.True(emailResult.IsFailure);
            Assert.Equal("Пустой email", emailResult.Error);
        }

        [Fact]
        public void Create_Email_Failure_By_Incorrect_Email()
        {
            //Arrange
            string email = "sdfs@dfgds";

            //Act
            var emailResult = Email.Create(email);

            //Assert
            Assert.True(emailResult.IsFailure);
            Assert.Equal("Введенная строка не является электронной почтой", emailResult.Error);
        }
    }
}
