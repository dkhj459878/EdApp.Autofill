using System.Collections.Generic;

namespace EdApp.AutoFill.BL.Model;

public record Node
{
    public bool IsLeaf { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }

    public Node Parent { get; set; }

    public IEnumerable<Node> Descendants { get; set; } = new List<Node>();

    public Node GetRootNode()
    {
        const string root = "root";
        return new Node
        {
            Name = root,
            Descendants = new List<Node>(),
            IsLeaf = false,
            Parent = this
        };
    }
}