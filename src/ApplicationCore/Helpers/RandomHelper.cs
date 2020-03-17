using System;

namespace Vnit.ApplicationCore.Helpers
{
    public static class RandomHelper
    {
      
        public static string GetRandomCode(int l)
        {
            const string key = "123456789abcdefghijklmnopqrstuvxyz ABCDEFGHIJKLMNPQRSTUVXYZ";
            int keyLenght = key.Length;
            var rnd = new Random();
            string s = String.Empty;
            for (int i = 0; i < l; i++)
                s = s + key[rnd.Next(keyLenght)];
            return s;
        }
    }
}
