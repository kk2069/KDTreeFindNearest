using System;
using System.Collections.Generic;
using UnityEngine;

public class KDTree<T>
{
    private class Node
    {
        public float[] Position;
        public T Data;
        public Node Left, Right;
        public int Depth;

        public Node(float[] position, T data, int depth)
        {
            Position = position;
            Data = data;
            Depth = depth;
        }
    }

    private Node root;
    private int dimensions;

    public KDTree(int dimensions)
    {
        this.dimensions = dimensions;
    }

    // 插入节点
    public void Insert(float[] position, T data)
    {
        root = InsertRec(root, position, data, 0);
    }

    private Node InsertRec(Node node, float[] position, T data, int depth)
    {
        if (node == null) return new Node(position, data, depth);

        int axis = depth % dimensions;
        if (position[axis] < node.Position[axis])
            node.Left = InsertRec(node.Left, position, data, depth + 1);
        else
            node.Right = InsertRec(node.Right, position, data, depth + 1);

        return node;
    }

    // 删除节点
    public void Remove(float[] position)
    {
        root = RemoveRec(root, position, 0);
    }

    private Node RemoveRec(Node node, float[] position, int depth)
    {
        if (node == null) return null;

        int axis = depth % dimensions;

        if (ArePositionsEqual(node.Position, position))
        {
            // 替换当前节点为右子树的最小值
            if (node.Right != null)
            {
                Node minNode = FindMin(node.Right, axis, depth + 1);
                node.Position = minNode.Position;
                node.Data = minNode.Data;
                node.Right = RemoveRec(node.Right, minNode.Position, depth + 1);
            }
            else if (node.Left != null)
            {
                Node minNode = FindMin(node.Left, axis, depth + 1);
                node.Position = minNode.Position;
                node.Data = minNode.Data;
                node.Right = RemoveRec(node.Left, minNode.Position, depth + 1);
                node.Left = null;
            }
            else
            {
                return null;
            }
        }
        else if (position[axis] < node.Position[axis])
        {
            node.Left = RemoveRec(node.Left, position, depth + 1);
        }
        else
        {
            node.Right = RemoveRec(node.Right, position, depth + 1);
        }

        return node;
    }

    private Node FindMin(Node node, int axis, int depth)
    {
        if (node == null) return null;

        int currentAxis = depth % dimensions;
        if (currentAxis == axis)
        {
            if (node.Left == null) return node;
            return FindMin(node.Left, axis, depth + 1);
        }

        Node leftMin = FindMin(node.Left, axis, depth + 1);
        Node rightMin = FindMin(node.Right, axis, depth + 1);

        return MinNode(node, leftMin, rightMin, axis);
    }

    private Node MinNode(Node a, Node b, Node c, int axis)
    {
        Node min = a;
        if (b != null && b.Position[axis] < min.Position[axis]) min = b;
        if (c != null && c.Position[axis] < min.Position[axis]) min = c;
        return min;
    }

    private bool ArePositionsEqual(float[] a, float[] b)
    {
        for (int i = 0; i < a.Length; i++)
            if (a[i] != b[i]) return false;
        return true;
    }

    // 最近邻查询
    public T FindNearest(float[] targetPosition)
    {
        return FindNearestRec(root, targetPosition, root, 0).Data;
    }

    private Node FindNearestRec(Node current, float[] targetPosition, Node best, int depth)
    {
        if (current == null) return best;

        if (DistanceSquared(current.Position, targetPosition) < DistanceSquared(best.Position, targetPosition))
        {
            best = current;
        }

        int axis = depth % dimensions;
        Node nextBranch = targetPosition[axis] < current.Position[axis] ? current.Left : current.Right;
        Node oppositeBranch = targetPosition[axis] < current.Position[axis] ? current.Right : current.Left;

        best = FindNearestRec(nextBranch, targetPosition, best, depth + 1);

        if (Math.Pow(targetPosition[axis] - current.Position[axis], 2) < DistanceSquared(best.Position, targetPosition))
        {
            best = FindNearestRec(oppositeBranch, targetPosition, best, depth + 1);
        }

        return best;
    }

    private float DistanceSquared(float[] a, float[] b)
    {
        float distance = 0;
        for (int i = 0; i < a.Length; i++)
        {
            float diff = a[i] - b[i];
            distance += diff * diff;
        }
        return distance;
    }
}
