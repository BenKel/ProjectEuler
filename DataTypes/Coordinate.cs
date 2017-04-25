namespace ProjectEuler.DataTypes
{
    internal class Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static readonly Coordinate Zero = new Coordinate(0, 0);

        public int X { get; set; }
        public int Y { get; set; }
    }
}
