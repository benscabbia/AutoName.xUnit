using System;

namespace AutoName.xUnit
{
	public static class Guard
	{
		public static void ArgumentIsNotNullOrWhiteSpace(object input)
		{
			if (input == null)
			{
				throw new ArgumentException();
			}
		}
	}
}
