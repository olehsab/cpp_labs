using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var file = File.OpenText("INPUT.txt");
            var values = file.ReadLine().Split(' ');
            var n = int.Parse(values[0]);
            var k = int.Parse(values[1]);
            var p = int.Parse(values[2]);
            var f = new string[n];
            var word = file.ReadLine();
            for (var i = 0; i < n; i++)
            {
                f[i] = file.ReadLine();
            }

            var result = string.Join("", fk(word, f, k).ToArray());
            try
            {
                File.WriteAllText("OUTPUT.txt", result[p - 1].ToString());
            }
            catch (IndexOutOfRangeException)
            {
                File.WriteAllText("OUTPUT.txt", "-");
            }
        }

        public static IEnumerable<string> f(string word, string subword, string[] fvalues)
        {
            for (var i = word.IndexOf(subword); i < word.IndexOf(subword) + subword.Length; i++)
            {
                yield return fvalues[i];
            }
        }
        
        public static IEnumerable<string> fk(string world, string[] fvalues, int k)
        {
            return k == 0 ? new[] { world } : f(world, string.Join("", fk(world, fvalues, --k).ToArray()), fvalues);
        }
    }
}