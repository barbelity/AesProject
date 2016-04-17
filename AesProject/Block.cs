using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AesProject
{
    public class Block
    {
        private byte[,] _data;

        public Block()
        {
            this._data = new byte[4, 4];
        }

		public Block(byte[] data)
		{
			if (data.Length != 16)
			{
				throw new Exception("data length given is not supported");
			}

			this._data = new byte[4, 4];
			Buffer.BlockCopy(data, 0, _data, 0, 16);
		}

		public byte this[int x, int y]
		{
			get { return _data[x, y]; }
			set { _data[x, y] = value; }
		}

    }
}
