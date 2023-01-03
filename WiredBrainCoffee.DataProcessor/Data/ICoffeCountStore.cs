using WiredBrainCoffee.DataProcessor.Model;

namespace WiredBrainCoffee.DataProcessor.Data
{
    public interface ICoffeCountStore
    {
        void Save(CountItem item);
    }
}
