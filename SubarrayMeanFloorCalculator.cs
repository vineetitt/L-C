using System;

class Program
{
    static void Main(string[] args)
    {
        var inputDetails = ParseInput();
        long[] prefixSumArray = BuildPrefixSumArray(inputDetails.ArrayElements);

        ProcessQueries(inputDetails.NumberOfQueries, prefixSumArray);
    }

    static (int NumberOfElements, int NumberOfQueries, long[] ArrayElements) ParseInput()
    {
        var firstLine = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        int numberOfElements = firstLine[0];
        int numberOfQueries = firstLine[1];

        var arrayElements = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

        return (numberOfElements, numberOfQueries, arrayElements);
    }

    static long[] BuildPrefixSumArray(long[] array)
    {
        long[] prefixSumArray = new long[array.Length + 1];
        prefixSumArray[0] = 0;

        for (int i = 1; i <= array.Length; i++)
        {
            prefixSumArray[i] = prefixSumArray[i - 1] + array[i - 1];
        }

        return prefixSumArray;
    }

    static void ProcessQueries(int numberOfQueries, long[] prefixSumArray)
    {
        for (int queryIndex = 0; queryIndex < numberOfQueries; queryIndex++)
        {
            var queryRange = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int leftIndex = queryRange[0];
            int rightIndex = queryRange[1];

            long meanFloor = CalculateMeanFloor(prefixSumArray, leftIndex, rightIndex);
            Console.WriteLine(meanFloor);
        }
    }

    static long CalculateMeanFloor(long[] prefixSumArray, int leftIndex, int rightIndex)
    {
        long subarraySum = prefixSumArray[rightIndex] - prefixSumArray[leftIndex - 1];
        int subarrayLength = rightIndex - leftIndex + 1;

        return subarraySum / subarrayLength;
    }
}
