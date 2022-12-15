using System.Linq;

namespace Loto.Script
{
    internal static class Common
    {
        /// <summary>
        /// Greatest common divisor.
        /// </summary>
        /// <param name="nums">Numbers.</param>
        /// <returns>Greatest common divisor number.</returns>
        internal static int GCD(params int[] nums) => nums.Aggregate(GCD);

        /// <summary>
        /// Greatest common divisor.
        /// </summary>
        /// <param name="a">First number.</param>
        /// <param name="b">Second number.</param>
        /// <returns>Greatest common divisor number.</returns>
        internal static int GCD(int a, int b) => b == 0 ? a : GCD(b, a % b);
    }
}
