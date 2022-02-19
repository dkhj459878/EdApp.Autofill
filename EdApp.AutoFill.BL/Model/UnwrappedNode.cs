using System;
using System.Collections.Generic;
using System.Linq;

namespace EdApp.AutoFill.BL.Model;

/// <summary>
///     To be used as an intermediate step for converting meta data about attributes,
///     placed at the mapping table, into full functioning tree.
/// </summary>
public class UnwrappedNode : ICloneable
{
    private const string Dot = ".";
    private IEnumerable<UnwrappedNode> _descendants;

    /// <summary>
    ///     Shows either descendants were generated.
    /// </summary>
    protected bool AreDescendantsGenerated { get; private set; }

    /// <summary>
    ///     Unwrapped full path of attribute.
    ///     For example:
    ///     'root.BaseCalculation.Tra.BgRotorSheet.Id'.
    ///     All unwrapped data notation started from
    ///     root.
    /// </summary>
    public string FullQualifiedPath { get; set; }

    /// <summary>
    ///     Parent unwrapped node.
    /// </summary>
    public UnwrappedNode Parent { get; set; }

    /// <summary>
    ///     Key of unwrapped node after processing.
    ///     For leaf it is empty string value.
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    ///     Data type. It means: int, float (any numeric), bool, string.
    ///     Used for correct writing attribute value in json notation.
    ///     It means either with double commas or not.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    ///     Data value in string format.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    ///     Leaf name. If full qualified path
    ///     is 'root.BaseCalculation.Tra.BgRotorSheet.Id', than
    ///     LeafName is 'Id'.
    ///     Added as a facilitator for processing 'FullQualifiedPath'
    ///     values into true Node.
    /// </summary>
    public string LeafName { get; set; }

    /// <summary>
    ///     Descendants of unwrapped node.
    /// </summary>
    public IEnumerable<UnwrappedNode> Descendants
    {
        get
        {
            // Don't generate descendants if they are already have been generated.
            if (AreDescendantsGenerated) return Descendants;
            _descendants = GenerateDescendants();
            MarkDescendantsAsGenerated();
            return _descendants;
        }

        set => _descendants = value;
    }

    public object Clone()
    {
        // I was unfortunately unable to use MemberwiseClone as I 
        // have got an exception doing this.
        var clone = new UnwrappedNode
        {
            Value = Value,
            FullQualifiedPath = FullQualifiedPath,
            LeafName = LeafName,
            Key = Key,
            AreDescendantsGenerated = AreDescendantsGenerated,
            Type = Type
        };
        return clone;
    }

    protected void MarkDescendantsAsGenerated()
    {
        AreDescendantsGenerated = true;
    }

    public IEnumerable<UnwrappedNode> GenerateDescendants()
    {
        return Descendants.Select(ProcessNode)
            .GroupBy(pn => pn.Key).Select(g => new UnwrappedNode
            {
                Key = g.Key,

                Descendants = g.Select(d =>
                {
                    var clone = (UnwrappedNode) d.Clone();
                    clone.Key = null;
                    clone.Descendants = null;
                    clone.Parent = null;
                    return clone;
                }).ToList(),
            });
    }

    public UnwrappedNode ProcessNode(UnwrappedNode processingNode)
    {
        // We are not processing leaf unwrapped nodes. Return them as they are.
        if (string.IsNullOrEmpty(processingNode.Key)) return processingNode;

        var processedNode = processingNode.WiseClone();
        if (!processedNode.Key.Contains(Dot))
        {
            processedNode.FullQualifiedPath = processedNode.Key;
            processedNode.LeafName = processedNode.Key;
            processedNode.Key = string.Empty;
            processedNode.Descendants = null;
            processedNode.Parent = this;
            return processedNode;
        }

        processedNode.Key = processedNode.FullQualifiedPath.Substring(0,
            processedNode.FullQualifiedPath.IndexOf(Dot, StringComparison.InvariantCultureIgnoreCase));
        processedNode.FullQualifiedPath =
            processedNode.FullQualifiedPath[
                (processedNode.FullQualifiedPath.IndexOf(Dot, StringComparison.OrdinalIgnoreCase) + 1)..];
        return processedNode;
    }

    public UnwrappedNode WiseClone()
    {
        var clone = (UnwrappedNode) Clone();
        var shadowParent = (UnwrappedNode) Parent.Clone();
        var shadowDescendants = Descendants.Select(descendant =>
        {
            var child = (UnwrappedNode) descendant.Clone();
            child.Parent = shadowParent;
            return child;
        }).ToList();
        clone.Parent = Parent;
        clone.Descendants = shadowDescendants;
        return clone;
    }
}