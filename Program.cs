// C# in-class sorting exercise vitiated starter code
// Includes code below from Jamro textbook for InsertionSort,
// SelectionSort, BubbleSort, QuickSort, and code from
// Deitel textbook for MergeSort.

using System.Diagnostics;
using System.Collections;

namespace sortingLab
{
    // from Jamro p.66, need to modify this class
    //   so that arrays of Person objects can be
    //   sorted (hint: consider System.IComparable,
    //   which requires you to add one function)
    public class Person : IComparable
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Person(string n, int a)
        {
            Name = n;
            Age = a;
        }

        // implementing CompareTo()
        public int CompareTo(object? myObj)
        {
            if (myObj == null)
            {
                return -1;
            }
            return Name.CompareTo(((Person)myObj).Name);
        }

        // Overriding toString()
        public override string ToString()
        {
            return Name + " " + Age;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // first declare variables
            const int N = 100;
            int[] data = new int[N];
            Random random = new Random();
            Stopwatch stopwatch = new Stopwatch();
            Person[] dataP = new Person[3];
            dataP[0] = new Person("Tim", 5);
            dataP[1] = new Person("Ted", 6);
            dataP[2] = new Person("Justus", 7);

            // next sort the array of Person objects, and print the results
            // or view results in the Debugger
            SelectionSort<Person>(dataP);
            foreach(Person p in dataP)
            {
                Console.WriteLine(dataP.ToString());
            }



            // next fill up the integer data array with random integers
            for(int i = 0; i < N; i++)
            {
                data[i] = random.Next();
            }

            // next start the stopwatch
            stopwatch.Start();

            // next sort the data array (use one of the pre-written functions
            // from below)
            InsertionSort(data);


            // next stop the stopwatch and display the time
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Console.WriteLine("{0} milliseconds", ts.TotalMilliseconds);
        }

        // YOU DON'T HAVE TO MODIFY ANYTHING BELOW THIS LINE

        public static void InsertionSort<T>(T[] array) where T : IComparable
        {
            for (int i = 1; i < array.Length; i++)
            {
                int j = i;
                while (j > 0 && array[j].CompareTo(array[j - 1]) < 0)
                {
                    Swap(array, j, j - 1);
                    j--;
                }
            }
        }


        public static void SelectionSort<T>(T[] array) where T : IComparable
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int minIndex = i;
                T minValue = array[i];
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j].CompareTo(minValue) < 0)
                    {
                        minIndex = j;
                        minValue = array[j];
                    }
                }
                Swap(array, i, minIndex);
            }
        }


        public static void BubbleSort<T>(T[] array) where T : IComparable
        {
            for (int i = 0; i < array.Length; i++)
            {
                bool isAnyChange = false;
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0)
                    {
                        isAnyChange = true;
                        Swap(array, j, j + 1);
                    }
                }
                if (!isAnyChange)
                {
                    break;
                }
            }
        }



        // Code that implements Quicksort, e.g.    QuickSort<int>(data);
        public static void QuickSort<T>(T[] array) where T : IComparable
        {
            QuickSort(array, 0, array.Length - 1);
        }
        private static void QuickSort<T>(T[] array, int lower, int upper) where T : IComparable
        {
            if (lower < upper)
            {
                int p = Partition(array, lower, upper);
                QuickSort(array, lower, p);
                QuickSort(array, p + 1, upper);
            }
        }
        private static int Partition<T>(T[] array, int lower, int upper) where T : IComparable
        {
            int i = lower;
            int j = upper;
            T pivot = array[lower]; //using first element as the pivot
            // T pivot = array[(lower + upper) / 2];
            do
            {
                while (array[i].CompareTo(pivot) < 0) { i++; }
                while (array[j].CompareTo(pivot) > 0) { j--; }
                if (i >= j) { break; }
                Swap(array, i, j);
            }
            while (i <= j);
            return j;
        }

        // Code that implements Quicksort, e.g.    MergeSort(data);
        public static void MergeSort(int[] values)
        {
            MergeSort(values, 0, values.Length - 1); // sort entire array
        }
        // splits array, sorts subarrays and merges subarrays into sorted array
        private static void MergeSort(int[] values, int low, int high)
        {
            // test base case; size of array equals 1                        
            if ((high - low) >= 1) // if not base case                   
            {
                int middle1 = (low + high) / 2; // calculate middle of array
                int middle2 = middle1 + 1; // calculate next element over     

                // split array in half; sort each half (recursive calls)      
                MergeSort(values, low, middle1); // first half of array 
                MergeSort(values, middle2, high); // second half of array   

                // merge two sorted arrays after split calls return           
                Merge(values, low, middle1, middle2, high);
            }
        }
        // merge two sorted subarrays into one sorted subarray             
        private static void Merge(int[] values, int left, int middle1,
           int middle2, int right)
        {
            int leftIndex = left; // index into left subarray               
            int rightIndex = middle2; // index into right subarray          
            int combinedIndex = left; // index into temporary working array 
            int[] combined = new int[values.Length]; // working array       

            // merge arrays until reaching end of either                    
            while (leftIndex <= middle1 && rightIndex <= right)
            {
                // place smaller of two current elements into result         
                // and move to next space in arrays                          
                if (values[leftIndex] <= values[rightIndex])
                {
                    combined[combinedIndex++] = values[leftIndex++];
                }
                else
                {
                    combined[combinedIndex++] = values[rightIndex++];
                }
            }
            // if left array is empty                                       
            if (leftIndex == middle2)
            {
                // copy in rest of right array                               
                while (rightIndex <= right)
                {
                    combined[combinedIndex++] = values[rightIndex++];
                }
            }
            else // right array is empty                                    
            {
                // copy in rest of left array                                
                while (leftIndex <= middle1)
                {
                    combined[combinedIndex++] = values[leftIndex++];
                }
            }
            // copy values back into original array                         
            for (int i = left; i <= right; ++i)
            {
                values[i] = combined[i];
            }
        }
        private static void Swap<T>(T[] array, int first, int second)
        {
            T temp = array[first];
            array[first] = array[second];
            array[second] = temp;
        }
    }
}

