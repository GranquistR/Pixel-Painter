namespace MyTestVueApp.Server.Entities
{

    public class Group(string groupName, int canvasSize = 32, string backgroundColor = "#FFFFFF")
    {
        public string Name { get; set; } = groupName;
        public int CanvasSize { get; set; } = canvasSize;
        public string BackgroundColor { get; set; } = backgroundColor;
        public List<string> Members { get; set; } = new();
        public string[,] Pixels { get; set; } = new string[canvasSize, canvasSize];

        public bool IsEmpty()
        {
            return Members.Count == 0;
        }

        public void AddMember(string member)
        {
            Members.Add(member);
        }

        public void RemoveMember(string member)
        {
            Members.Remove(member);
        }

        public void PaintPixels(string color, Coordinate[] coords)
        {
            foreach (Coordinate coord in coords)
            {
                Pixels[coord.X, coord.Y] = color;
            }
        }

        public List<Pixel> GetPixelsAsList()
        {
            List<Pixel> pixelVec = new();
            for (int i = 0; i < Pixels.GetLength(0); i++)
            {
                for (int j = 0; j < Pixels.GetLength(1); j++)
                {
                    if (Pixels[i, j] != null)
                    {
                        pixelVec.Add(new Pixel(Pixels[i, j], i, j));
                    } else
                    {
                        pixelVec.Add(new Pixel(BackgroundColor, i, j));
                    }
                }
            }
            return pixelVec;
        }

        public int GetMemberCount()
        {
            return Members.Count;
        }
    }

}
