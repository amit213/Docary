using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Docary.MvcExtensions
{
    public class Levenshtein
    {
        public static int CalculateDistance(string str1, string str2) 
        {
            var matrix = new int[str1.Length + 1, str2.Length + 1];

			for (var i = 0; i <= str1.Length; i++)
				matrix[i, 0] = i;
			for (var j = 0; j <= str2.Length; j++)
				matrix[0, j] = j;

			for (var i = 1; i <= str1.Length; i++)
			{
				for (var j = 1; j <= str2.Length; j++)
				{
					var cost = str1[i - 1] == str2[j - 1] ? 0 : 1;

					matrix[i, j] = (new[]
					{
						matrix[i - 1, j] + 1, matrix[i, j - 1] + 1, matrix[i - 1, j - 1] + cost
					}).Min();

					if ((i > 1) && 
						(j > 1) && 
						(str1[i - 1] == str2[j - 2]) &&
						(str1[i - 2] == str2[j - 1]))
					{
						matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + cost);
					}
				}
			}

			return matrix[str1.Length, str2.Length];
		}        
    }
}
