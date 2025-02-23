using Domain.Primitives;

namespace Domain.UnitTests.Primitives;

public partial class ErrorTests
{
    [Fact]
    public void ErrorWithEmptyCodeAndNonEmptyDescriptionIsCreatedCorrectly()
    {
        // Act
        ErrorType error = new(EMPTY, DESCRIPTION);

        // Assert
        Assert.Equal(EMPTY, error.Code);
        Assert.Equal(DESCRIPTION, error.Description);
    }

    [Fact]
    public void ErrorWithEmptyCodeAndNullDescriptionIsCreatedCorrectly()
    {
        // Act
        ErrorType error = new(CODE, null);

        // Assert
        Assert.Equal(CODE, error.Code);
        Assert.Null(error.Description);
    }

    [Fact]
    public void ErrorWithNonEmptyCodeAndNonEmptyDescriptionIsCreatedCorrectly()
    {
        // Act
        ErrorType error = new(CODE, DESCRIPTION);

        // Assert
        Assert.Equal(CODE, error.Code);
        Assert.Equal(DESCRIPTION, error.Description);
    }

    [Fact]
    public void ErrorWithNonEmptyCodeAndNullDescriptionIsCreatedCorrectly()
    {
        // Act
        ErrorType error = new(CODE, null);

        // Assert
        Assert.Equal(CODE, error.Code);
        Assert.Null(error.Description);
    }

    [Fact]
    public void ErrorWithNoneHasEmptyCodeAndDescription()
    {
        // Act
        ErrorType error = ErrorType.None;

        // Assert
        Assert.Equal("", error.Code);
        Assert.Equal("", error.Description);
    }
}
