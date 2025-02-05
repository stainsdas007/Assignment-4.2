namespace DelegateAssignment3C
{
    public delegate void TemperatureEventHandler(string message);

    public class Program
    {
        // Declare the event at the class level
        public static event TemperatureEventHandler CriticalTemperatureReached;

        // Method to trigger the event
        public static void SetTemperature(int temperature)
        {
            Console.WriteLine($"Current Temperature: {temperature}°C");

            if (temperature > 100 || temperature < 0)
            {
                // Raise the event if the temperature is critical
                CriticalTemperatureReached?.Invoke("Critical temperature reached!");
            }
        }

        static void Main(string[] args)
        {
            // Subscribe to the event
            CriticalTemperatureReached += message => Console.WriteLine(message);

            // Simulate temperature changes
            Console.WriteLine("Enter temperatures (Press Enter without typing to quit):");

            while (true)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) // Quit on pressing Enter without input
                {
                    break; // Exit the loop
                }

                if (int.TryParse(input, out int temp))
                {
                    SetTemperature(temp);  // Set temperature and check if it's critical
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }

            Console.WriteLine("Program has ended.");
        }
    }
}