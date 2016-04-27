using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AesProject
{
    public class Block
	{
		#region members

		private byte[,] _data;

		#endregion

		#region constructors

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

		#endregion

		#region operators
		public byte this[int x, int y]
		{
			get { return _data[x, y]; }
			set { _data[x, y] = value; }
		}
		
		public static bool operator ==(Block a, Block b)
		{
			if (System.Object.ReferenceEquals(a, b))
				return true;

			if ((object)a == null || (object)b == null)
				return false;

			for (int i = 0; i < 4; i++)
				for (int j = 0; j < 4; j++)
					if (a[i, j] != b[i, j])
						return false;
			return true;
		}

		public static bool operator !=(Block a, Block b)
		{
			if (System.Object.ReferenceEquals(a, b))
				return false;

			if ((object)a == null || (object)b == null)
				return true;

			for (int i = 0; i < 4; i++)
				for (int j = 0; j < 4; j++)
					if (a[i, j] != b[i, j])
						return true;
			return false;
		}

		#endregion

		public byte[] toByteArray()
		{
			byte[] res = new byte[16];
			for (int i = 0; i < 4; i++)
				for (int j = 0; j < 4; j++)
					res[i * 4 + j] = this._data[i, j];
			return res;
		}
	}
}
