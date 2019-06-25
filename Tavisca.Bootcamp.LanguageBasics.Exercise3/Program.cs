using System;
using System.Collections.Generic;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            var calorie = new int[protein.Length];
            var dietList = new List<int>();
            for(int i=0;i<protein.Length;i++)
            {
                calorie[i] = (protein[i] + carbs[i])*4 + fat[i]*9;
            }
            var index = new List<int>();
            foreach (var item in dietPlans)
            {
               index.Clear();
               if(string.IsNullOrWhiteSpace(item))
                {
                    index.Clear();
                    index.Add(0);
                }
               else
                {
                    foreach (var ch in item)//CcPpFfTt
                    {
                        if (index.Count == 1)
                            break;
                        UpdateIndexList(protein, carbs, fat, calorie, ch,index);
                    }
                }
                dietList.Add(index[0]);
            }
            int[] diet = new int[dietPlans.Length];
            dietList.CopyTo(diet);
            dietList.Clear();
            return diet;
        }

        public static void UpdateIndexList(int[] protein, int[] carbs, int[] fat,int [] calorie,char ch,List<int> index)
        {
            switch (ch)
            {
                case 'C':
                    FindIndex(carbs, index, "max");
                    break;
                case 'c':
                    FindIndex(carbs, index, "min");
                    break;
                case 'P':
                    FindIndex(protein, index, "max");
                    break;
                case 'p':
                    FindIndex(protein, index, "min");
                    break;
                case 'F':
                    FindIndex(fat, index, "max");
                    break;
                case 'f':
                    FindIndex(fat, index, "min");
                    break;
                case 'T':
                    FindIndex(calorie, index, "max");
                    break;
                case 't':
                    FindIndex(calorie, index, "min");
                    break;
            }
        }

        private static void FindIndex(int[] arr, List<int> index,string typeOfIndex)
        {
            var indexList = new List<int>();
            int arrayValue;
            if(typeOfIndex.Equals("min"))
            {
                arrayValue = int.MaxValue;
            }
            else
            {
                arrayValue = int.MinValue;
            }
            if(index.Count==0)
            {
                indexList.Clear();
                for(int i=0;i<arr.Length;i++)
                {
                    if(arrayValue == arr[i])
                    {
                        indexList.Add(i);
                    }
                    if(CheckMaxMin(arrayValue, arr[i], typeOfIndex))
                    {
                        indexList.Clear();
                        arrayValue = arr[i];
                        indexList.Add(i);
                    }
                }
            }
            else
            {
                indexList.Clear();
                foreach(var i in index)
                {
                    if (arrayValue == arr[i])
                    {
                        indexList.Add(i);
                    }
                    if (CheckMaxMin(arrayValue,arr[i],typeOfIndex))
                    {
                        indexList.Clear();
                        arrayValue = arr[i];
                        indexList.Add(i);
                    }
                }
            }
            index.Clear();
            for (int i = 0; i < indexList.Count; i++)
                index.Add(indexList[i]);
        }
        
        private static bool CheckMaxMin(int arrayValue,int arrayElement,string operation)
        {
            if(operation.Equals("min"))
            {
                if (arrayValue > arrayElement)
                    return true;
                else
                    return false;
            }
            else
            {
                if (arrayValue < arrayElement)
                    return true;
                else
                    return false;
            }
        }
    }
}
