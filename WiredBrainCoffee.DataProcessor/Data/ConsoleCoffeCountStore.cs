using WiredBrainCoffee.DataProcessor.Model;

namespace WiredBrainCoffee.DataProcessor.Data
{
    public class ConsoleCoffeCountStore : ICoffeCountStore
    {
        private readonly TextWriter _textWriter;

        public ConsoleCoffeCountStore() : this(Console.Out) { }
        public ConsoleCoffeCountStore(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }
        public void Save(CountItem item)
        {
            var line = $"{item.CoffeeType}:{item.count}";
            _textWriter.WriteLine(line);
        }
    }
}
