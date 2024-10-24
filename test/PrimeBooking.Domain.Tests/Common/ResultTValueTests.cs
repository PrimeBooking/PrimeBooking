namespace PrimeBooking.Domain.Tests.Common;

public class ResultTValueTests
{
    [Fact]
    public void Success_WithValue_ShouldCreateSuccessResult()
    {
        var valueBody = new ValueBody() { Value = "test" };
        
        var result = Result.Success(valueBody);
        
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().BeNull();
        result.Value.Should().Be(valueBody);
    }
    
    [Fact]
    public void Failure_WithError_ShouldCreateFailedResult()
    {
        Error error = ErrorFactory.BuildError(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), HttpStatusCode.BadRequest);
        
        var result = Result.Failure<ValueBody>(error);
        
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }
    
    [Fact]
    public void Create_WithBody_ShouldCreateSuccessResult()
    {
        var valueBody = new ValueBody() { Value = "test" };

        var result = Result.Create(valueBody);
        
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().BeNull();
    }
    
    [Fact]
    public void Create_WithNullableBody_ShouldCreateFailedResult()
    {
        Error error = ErrorFactory.BuildError(ErrorCode.UnhandledRequest,
            ErrorType.InvalidFormat,
            "Unable to create a Result object",
            HttpStatusCode.InternalServerError);
        
        var result = Result.Create<ValueBody>(null);
        
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }
}
