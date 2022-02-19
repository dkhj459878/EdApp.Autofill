namespace EdApp.AutoFill.BL.Model;

public class JsonNode
{
    public int Number { get; set; }

    public string NodeFullPath { get; set; }

    public string Line { get; set; }

    public bool IsNode { get; set; }

    public string Comment { get; set; }

    public int Depth { get; set; }
}