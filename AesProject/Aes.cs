using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AesProject
{
    public static class Aes
    {

        private String[,] Sbox = { { "a", "b" }, { "c", "d" } };
        
        private String[,] EncryptMatrix;
        
        private String[,] DecryptMatrix;


        public static Block SubBytes(Block block)
        {
            throw new NotImplementedException();
        }

        public static Block ShiftRows(Block block)
        {
            throw new NotImplementedException();
        }

        public static Block MixColumns(Block block)
        {
            throw new NotImplementedException();
        }

        public static Block AddRoundKey(Block block)
        {
            throw new NotImplementedException();
        }
    }
}
