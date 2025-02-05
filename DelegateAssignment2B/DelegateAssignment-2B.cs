using System;

namespace DelegateAssignment_2B
{
    // Declare a delegate for array operations
    public delegate int[] ArrayOperation(int[] numbers);

    internal class Program
    {
        // Method to sort an array
        static int[] SortArray(int[] array)
        {
            Array.Sort(array);
            return array;
        }

        // Method to reverse an array
        static int[] ReverseArray(int[] array)
        {
            Array.Reverse(array);
            return array;
        }

        static void Main(string[] args)
        {
            while (true) // Infinite loop until user chooses to exit
            {
                try
                {
                    Console.WriteLine("\nEnter numbers separated by spaces:");
                    int[] numbers = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

                    Console.WriteLine("Choose an operation: 1 for Sort, 2 for Reverse, 3 to Exit");
                    int choice = int.Parse(Console.ReadLine());

                    if (choice == 3)
                    {
                        Console.WriteLine("Exiting program...");
                        break; // Exit the loop
                    }

                    ArrayOperation arrayDelegate = choice switch
                    {
                        1 => SortArray,
                        2 => ReverseArray,
                        _ => throw new InvalidOperationException("Invalid choice. Please enter 1, 2, or 3.")
                    };

                    // Call the delegate and print the result
                    int[] result = arrayDelegate(numbers);
                    Console.WriteLine("Result: " + string.Join(" ", result));
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter numbers correctly.");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
