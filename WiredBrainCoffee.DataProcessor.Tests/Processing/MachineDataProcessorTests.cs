
using WiredBrainCoffee.DataProcessor.Data;
using WiredBrainCoffee.DataProcessor.Model;

namespace WiredBrainCoffee.DataProcessor.Processing;

public class MachineDataProcessorTests:IDisposable
{
    private readonly FakeCoffeCountStore CoffeCountStore;
    private readonly MachineDataProcessor machineDataProcesing;

    public MachineDataProcessorTests()
    {
        CoffeCountStore = new FakeCoffeCountStore();

        machineDataProcesing = new MachineDataProcessor(CoffeCountStore);
    }


    [Fact]
    public void ShouldSaveCountPerCoffeeType()
    {
        var items = new[]
        {
            new MachineDataItem("Cappuccino",new DateTime(2022,10,27,8,0,0)),
            new MachineDataItem("Cappuccino",new DateTime(2022,10,27,9,0,0)),
            new MachineDataItem("Espresso",new DateTime(2022,10,27,10,0,0)),
        };

        // Act

        machineDataProcesing.ProcessItems(items);

        //assert

        Assert.Equal(2,CoffeCountStore.SaveItems.Count);

        var item = CoffeCountStore.SaveItems[0];
        Assert.Equal("Cappuccino",item.CoffeeType);
        Assert.Equal(2, item.count);

         item = CoffeCountStore.SaveItems[1];
        Assert.Equal("Espresso", item.CoffeeType);
        Assert.Equal(1, item.count);

    }
    [Fact]
    public void ShouldIgnorItemsThatAreNotNewer()
    {
        var items = new[]
        {
            new MachineDataItem("Cappuccino",new DateTime(2022,10,27,8,0,0)),
            new MachineDataItem("Cappuccino",new DateTime(2022,10,27,7,0,0)),
            new MachineDataItem("Cappuccino",new DateTime(2022,10,27,7,10,0)),
            new MachineDataItem("Cappuccino",new DateTime(2022,10,27,9,0,0)),
            new MachineDataItem("Espresso",new DateTime(2022,10,27,10,0,0)),
            new MachineDataItem("Cappuccino",new DateTime(2022,10,27,10,0,0)),
        };

        // Act

        machineDataProcesing.ProcessItems(items);

        //assert

        Assert.Equal(2, CoffeCountStore.SaveItems.Count);

        var item = CoffeCountStore.SaveItems[0];
        Assert.Equal("Cappuccino", item.CoffeeType);
        Assert.Equal(2, item.count);

        item = CoffeCountStore.SaveItems[1];
        Assert.Equal("Espresso", item.CoffeeType);
        Assert.Equal(1, item.count);

    }
    [Fact]
    public void ShouldClearPreviosCoffeCount()
    {
        var items = new[]
        {
            new MachineDataItem("Cappuccino",new DateTime(2022,10,27,8,0,0)),
        };

        // Act

        machineDataProcesing.ProcessItems(items);
        machineDataProcesing.ProcessItems(items);

        //assert

        Assert.Equal(2, CoffeCountStore.SaveItems.Count);
        foreach (var item in CoffeCountStore.SaveItems)
        {
            Assert.Equal("Cappuccino", item.CoffeeType);
            Assert.Equal(1, item.count);
        }
    }

    public void Dispose()
    {
        ///
    }
}
public class FakeCoffeCountStore : ICoffeCountStore
{
    public List<CountItem> SaveItems { get; } = new();
    public void Save(CountItem item)
    {
        SaveItems.Add(item);
    }
}
