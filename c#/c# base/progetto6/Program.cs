using System;


namespace progetto6
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = input.Where(character => Char.IsLetter(character))
                     .GroupBy(alphabet=>alphabet)
                     .Select(alphabet=> new 
                                 {
                                   Letter = alphabet.Key,
                                   Count = alphabet.Count()
                                 });
        }
    }
}
