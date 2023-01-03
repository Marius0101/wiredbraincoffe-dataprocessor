

using System.ComponentModel.DataAnnotations;

namespace WiredBrainCoffee.DataProcessor.Parsing;

public class CsvLineParserTests
{
    [Fact]
    public void ShouldParseValidLine()
    {
        string[] csvLine = new[] { "Cappuccino;10/27/2022 8:06:04 AM" };
        var machineDataImems = CsvLineParser.Parse(csvLine);

        Assert.NotNull(machineDataImems);
        Assert.Single(machineDataImems);
        Assert.Equal("Cappuccino", machineDataImems[0].CoffeeType);
        Assert.Equal(new DateTime(2022,10,27,8,6,4), machineDataImems[0].CreatedAt);
    }
    [Fact]
    public void ShouldSkipEmptyLine()
    {
        string[] csvLine = new[] { "", " "};
        var machineDataImems = CsvLineParser.Parse(csvLine);

        Assert.NotNull(machineDataImems);
        Assert.Empty(machineDataImems);
    }
    [InlineData("Cappuccino", "Invalid csv line")]
    [InlineData("Cappuccino;InvalidDateTime", "Invalid date time csv line")]
    [Theory]
    public void ShouldThrowExceptioforInvalidLine(string csvLine, string ExpectedMsg)
    {
        string[] csvLines = new[] { csvLine };


        var exception = Assert.Throws<Exception>(()=> CsvLineParser.Parse(csvLines));


        Assert.Equal($"{ExpectedMsg}: {csvLine}", exception.Message);
    }

}
