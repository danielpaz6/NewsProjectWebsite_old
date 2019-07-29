using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsProject.Models
{
    public class Rsa
    {
        private uint d = 2962847977;
        private uint e = 2321424373;
        private uint n = 2979713129;

        public string encrpytion(string s)
        {
            //string[] fullText = s.Split(new char[] { ' ' });
            string final = "";

            int len = s.Length;
            for (int i = 0; i < len; i++)
            {

                //final += powermod((uint)s[i], e, n) + " "; // M^e % n
                final += (uint)s[i] + " ";
            }

            return final;
        }

        public string decryption(string s)
        {
            return "";
        }

        private uint powermod(uint tbase, double exponent, uint modulus)
        {
            if (tbase < 1 || exponent < 0 || modulus < 1)
                throw new Exception("Invalid parameters.");

            uint result = 1;
            while (exponent > 0)
            {
                if ((exponent % 2) == 1)
                {
                    result = (result * tbase) % modulus;
                }

                tbase = (tbase * tbase) % modulus;
                exponent = Math.Floor(exponent / 2);
            }

            return result;
        }
    }
}