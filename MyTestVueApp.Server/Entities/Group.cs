namespace MyTestVueApp.Server.Entities
{

    public class Group(string groupName)
    {
        public string Name { get; set; } = groupName;
        public int CanvasSize { get; set; } = 32;
        public string BackgroundColor { get; set; } = "#FFFFFF";
        public List<Artist> Members { get; set; } = new();
        public string[][] Pixels { get; set; } = new string[32][];

        
        public Group(string groupName, string[][] canvas, int canvasSize, string backgroundColor): this(groupName)
        {
            Name = groupName;
            Pixels = canvas;
            CanvasSize = canvasSize;
            BackgroundColor = backgroundColor;
            Members = new();
        }

        public bool IsEmpty()
        {
            return Members.Count == 0;
        }

        public void AddMember(Artist member)
        {
            if (!Members.Contains(member))
            {
                Members.Add(member);
            }
        }

        public void RemoveMember(Artist member)
        {
            Members.Remove(member);
        }

        public void PaintPixels(string color, Coordinate[] coords)
        {
            foreach (Coordinate coord in coords)
            {
                Pixels[coord.X][coord.Y] = color;
            }
        }

        public List<Pixel> GetPixelsAsList()
        {
            List<Pixel> pixelVec = new();
            for (int i = 0; i < Pixels.GetLength(0); i++)
            {
                for (int j = 0; j < Pixels.GetLength(0); j++)
                {
                    if (Pixels[i][j] != null)
                    {
                        pixelVec.Add(new Pixel(Pixels[i][j], i, j));
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
