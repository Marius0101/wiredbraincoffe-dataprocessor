using WiredBrainCoffee.DataProcessor.Data;
using WiredBrainCoffee.DataProcessor.Model;

namespace WiredBrainCoffee.DataProcessor.Processing
{
    public class MachineDataProcessor
    {
        public MachineDataProcessor(ICoffeCountStore coffeCountStore)
        {
            _coffeCountStore = coffeCountStore;
        }
        private readonly Dictionary<string, int> _countPerCoffeeType = new();
        private readonly ICoffeCountStore _coffeCountStore;
        private MachineDataItem? _previosDataItem;

        public void ProcessItems(MachineDataItem[] dataItems)
        {
            _previosDataItem = null;
            _countPerCoffeeType.Clear();

            foreach (var dataItem in dataItems)
            {
                ProcessItem(dataItem);
            }

            SaveCountPerCoffeeType();
        }

        private void ProcessItem(MachineDataItem dataItem)
        {
            if (!NewMethod(dataItem))
            {
                return;
            }
            if (!_countPerCoffeeType.ContainsKey(dataItem.CoffeeType))
            {
                _countPerCoffeeType.Add(dataItem.CoffeeType, 1);
            }
            else
            {
                _countPerCoffeeType[dataItem.CoffeeType]++;
            } 
            _previosDataItem = dataItem;
            
        }

        private bool NewMethod(MachineDataItem dataItem)
        {
            return _previosDataItem == null ||
                _previosDataItem.CreatedAt < dataItem.CreatedAt;
        }

        private void SaveCountPerCoffeeType()
        {
            foreach (var entry in _countPerCoffeeType)
            {
                _coffeCountStore.Save(new CountItem(entry.Key, entry.Value));
            }
        }
    }
}
