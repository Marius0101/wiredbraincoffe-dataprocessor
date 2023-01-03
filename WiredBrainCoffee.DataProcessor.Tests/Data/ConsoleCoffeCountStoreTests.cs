using WiredBrainCoffee.DataProcessor.Model;

namespace WiredBrainCoffee.DataProcessor.Data;

public class ConsoleCoffeCountStoreTests
{
    [Fact]
    public void ShouldWriteOutputToConsole()
    {
        // arrange

        var item = new CountItem("Cappuccino",5);
        var stringWriter = new StringWriter();
        var consoleCoffeCountStore = new ConsoleCoffeCountStore(stringWriter);

        // act

        consoleCoffeCountStore.Save(item);
        var result = stringWriter.ToString();

        //Assert
        Assert.Equal($"{item.CoffeeType}:{item.count}{Environment.NewLine}", result);
    }
}
