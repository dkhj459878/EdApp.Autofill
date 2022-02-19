using System.Collections.Generic;

namespace EdApp.AutoFill.BL.Model;

public class SemiNode
{
    public Node Node { get; set; }

    public IEnumerable<UnwrappedNode> UnwrappedNodes { get; set; }
}