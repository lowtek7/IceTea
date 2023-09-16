using System.Text;

namespace Base
{
	public static class Encoding
	{
		public static UTF8Encoding Current { get; } = new UTF8Encoding(false);
	}
}
