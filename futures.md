## Get records by month

```csharp
public async Task<List<BlogPost>> GetBlogPostsByMonth(int month)
{
    return await _context.BlogPosts
        .Where(post => post.PublishDate.Month == month)
        .ToListAsync();
}
```

## This is a function that calculates how often a task is completed by a user in a TODO list:

```csharp
public static double CalculateTaskCompletionRate(List<Task> todoList, string userId)
{
    int totalTasks = todoList.Count;
    int completedTasks = todoList.Count(task => task.UserId == userId && task.IsCompleted);

    if (totalTasks == 0)
    {
        return 0.0;
    }

    return (double)completedTasks / totalTasks;
}
```

## Mean - The mean (average) of a data set is found by adding all numbers in the data set and then dividing by the number of values in the set.

```csharp
public static double CalculateMean(List<double> numbers)
{
    if (numbers == null || numbers.Count == 0)
    {
        throw new ArgumentException("The list of numbers cannot be null or empty.");
    }

    double sum = 0;
    foreach (double number in numbers)
    {
        sum += number;
    }

    return sum / numbers.Count;
}
```

You can use this function by passing a list of numbers to it, and it will return the mean (average) of those numbers.

## Median - The median is the middle value when a data set is ordered from least to greatest.

a C# function that calculates the median of a list of numbers:

```csharp
public static double CalculateMedian(List<double> numbers)
{
    numbers.Sort();

    int count = numbers.Count;
    int middleIndex = count / 2;

    if (count % 2 == 0)
    {
        // If the count is even, average the two middle numbers
        return (numbers[middleIndex - 1] + numbers[middleIndex]) / 2;
    }
    else
    {
        // If the count is odd, return the middle number
        return numbers[middleIndex];
    }
}
```

You can use this function by passing a list of numbers to it, and it will return the median value. For example:

```csharp
List<double> numbers = new List<double> { 1, 2, 3, 4, 5 };
double median = CalculateMedian(numbers);
Console.WriteLine("Median: " + median);
```

This will output:

```
Median: 3
```

Note that the function assumes the input list is already sorted in ascending order. If the list is not sorted, you can call the `Sort()` method on the list before passing it to the function.

## Mode - The mode is the number that occurs most often in a data set
```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static int CalculateMode(List<int> numbers)
    {
        Dictionary<int, int> frequency = new Dictionary<int, int>();

        foreach (int number in numbers)
        {
            if (frequency.ContainsKey(number))
            {
                frequency[number]++;
            }
            else
            {
                frequency[number] = 1;
            }
        }

        int maxFrequency = frequency.Values.Max();
        int mode = frequency.FirstOrDefault(x => x.Value == maxFrequency).Key;

        return mode;
    }

    public static void Main(string[] args)
    {
        List<int> numbers = new List<int> { 1, 2, 2, 3, 3, 3, 4, 4, 4, 4 };
        int mode = CalculateMode(numbers);
        Console.WriteLine("Mode: " + mode);
    }
}
```

This function uses a dictionary to keep track of the frequency of each number in the list. It then finds the number with the highest frequency and returns it as the mode. In the example usage in the `Main` method, it calculates the mode of the list `{ 1, 2, 2, 3, 3, 3, 4, 4, 4, 4 }` and prints the result.

## Match percentage
a C# function that calculates the percentage of matches between two lists:

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static double CalculateMatchPercentage<T>(List<T> list1, List<T> list2)
    {
        if (list1.Count == 0 || list2.Count == 0)
        {
            return 0.0;
        }

        int matches = list1.Intersect(list2).Count();
        double percentage = (double)matches / Math.Max(list1.Count, list2.Count) * 100;

        return percentage;
    }

    public static void Main()
    {
        // Example usage
        List<int> list1 = new List<int> { 1, 2, 3, 4, 5 };
        List<int> list2 = new List<int> { 3, 4, 5, 6, 7 };

        double matchPercentage = CalculateMatchPercentage(list1, list2);
        Console.WriteLine($"Match Percentage: {matchPercentage}%");
    }
}
```

This function takes two generic lists as input and calculates the percentage of matches between them. It uses the `Intersect` method to find the common elements between the two lists and then calculates the percentage based on the count of matches and the maximum count of the two lists. The result is returned as a double value representing the match percentage.

In the example usage, two integer lists `list1` and `list2` are provided, and the match percentage is calculated and printed to the console. You can modify the lists as per your requirements.
