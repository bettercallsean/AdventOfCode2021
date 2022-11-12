namespace AdventOfCode.Utilities;
internal static class ArrayHelper
{
    public static bool IsValidCoordinate<T>(int x, int y, T[][] array)
    {
        return !(x < 0 || y < 0 || x > array.Length - 1 || y > array[0].Length - 1);
    }

    public static void ArrayPrinter<T>(T[][] array)
    {
        foreach (var row in array)
        {
            foreach (var item in row)
            {
                Console.Write(item);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}
