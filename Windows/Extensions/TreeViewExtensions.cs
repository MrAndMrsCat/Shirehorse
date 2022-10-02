namespace Shirehorse.Core.Extensions
{
    public static class TreeViewExtensions
    {
        public static void InsertAlphabetically(this TreeNodeCollection collection, TreeNode node, int startAt = 0)
        {
            foreach (TreeNode n in collection)
            {
                if (n.Text[startAt..].CompareTo(node.Text[startAt..]) == 1)
                {
                    collection.Insert(n.Index, node);
                    return;
                }
            }
            collection.Add(node);
        }
    }
}
