using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace EdApp.AutoFill.BL.Model;

/// <summary>
///     To be used as an intermediate step for converting meta data about attributes,
///     placed at the mapping table, into full functioning tree.
/// </summary>
public class UnwrappedNode : ICloneable
{
    private const string Dot = ".";

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
    ///     Key of unwrapped node after processing.
    ///     For leaf it is empty string value.
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Value of the node with type.
    /// </summary>
    public NodeValue Value { get; set; }

    /// <summary>
    /// Shows if unwrapped node is leaf or not.
    /// </summary>
    public bool IsLeaf => !string.IsNullOrEmpty(FullQualifiedPath) && !FullQualifiedPath.Contains(Dot);

    /// <summary>
    ///     Descendants of unwrapped node.
    /// </summary>
    public IEnumerable<UnwrappedNode> Descendants { get; set; }

    public object Clone()
    {
        return MemberwiseClone();
    }

    protected void MarkDescendantsAsGenerated()
    {
        AreDescendantsGenerated = true;
    }

    public void ProcessDescendants()
    {
        if (AreDescendantsGenerated) return;
        var processedDescendants = Descendants.Select(x => ProcessNode(x)).ToList();
        var groupedDescendants = processedDescendants
            .GroupBy(pn => pn.Key).Select(g => new UnwrappedNode { Key = g.Key, Descendants = new List<UnwrappedNode>(g.ToList())});
        Descendants = null;
        MarkDescendantsAsGenerated();
    }

    public UnwrappedNode ProcessNode(UnwrappedNode processingNode)
    {
        var processedNode = processingNode.ClonePlane();
        // If a passed unwrapped node is a leaf, than will return it as it is.
        if (processingNode.IsLeaf)
        {
            processedNode.Key = processingNode.FullQualifiedPath;
            return processedNode;
        }

        // Processes a not leaf case.
        processedNode.Key = processedNode.FullQualifiedPath.Substring(0,
            processedNode.FullQualifiedPath.IndexOf(Dot, StringComparison.InvariantCultureIgnoreCase));
        processedNode.FullQualifiedPath =
            processedNode.FullQualifiedPath[
                (processedNode.FullQualifiedPath.IndexOf(Dot, StringComparison.OrdinalIgnoreCase) + 1)..];
        return processedNode;
    }

    public UnwrappedNode WiseClone()
    {
        // Creates plane copy.
        var wiseClone = ClonePlane();
        
        // Creates plane copy of descendants.
        if (Descendants is null)
        {
            return wiseClone;
        }
        var planeCopyOfDescendants = Descendants.Select(descendant =>
        {
            var planeCopyOfDescendant = descendant.ClonePlane();
            return planeCopyOfDescendant;
        }).ToList();
        
        wiseClone.Descendants = planeCopyOfDescendants;

        return wiseClone;
    }

    protected UnwrappedNode ClonePlane()
    {
        return (UnwrappedNode) Clone();
    }
}