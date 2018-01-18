using System;
using System.Collections.Generic;
using System.Linq;
using ExpressionParser.Model.Nodes;

namespace ExpressionParser.Model
{
	internal class NodeStack : Stack<Node>
	{
		internal Node LastAdded;

		internal void Add(Node node)
		{
			if (!this.Any())
				Push(node);
			else switch (node) {
				case BinaryNode binaryNode when binaryNode.IsClosed:
					AttachNodeToRoot(node);
					break;
				case BinaryNode binaryNode when Peek() is BinaryNode root && root.Precedence <= binaryNode.Precedence:
					AttachRootToNodeLeft(binaryNode);
					break;
				case BinaryNode binaryNode when Peek() is BinaryNode root:
					MoveRootRightToNodeLeft(root, binaryNode);
					AttachNodeToRootRight(root, binaryNode);
					break;
				case BinaryNode binaryNode:
					AttachRootToNodeLeft(binaryNode);
					break;
				default:
					AttachNodeToRoot(node);
					break;
			}
			LastAdded = node;
		}

		private void AttachNodeToRoot(Node node)
		{
			if (!Peek().TryAddNode(node))
				throw new InvalidOperationException($"Error adding '{node.GetType().Name}' to '{Peek().GetType().Name}'.");
		}

		private static void MoveRootRightToNodeLeft(BinaryNode root, BinaryNode node)
		{
			node.Left = node.Left ?? root.Right;
		}

		private static void AttachNodeToRootRight(BinaryNode root, Node node)
		{
			root.Right = node;
		}

		private void AttachRootToNodeLeft(BinaryNode node)
		{
			node.Left = Pop();
			Push(node);
		}
	}
}