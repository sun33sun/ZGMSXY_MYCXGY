/****************************************************************************
 * Copyright (c) 2017 Thor Brigsted UNDER MIT LICENSE  see licenses.txt 
 * Copyright (c) 2022 liangxiegame UNDER Paid MIT LICENSE  see licenses.txt
 *
 * xNode: https://github.com/Siccity/xNode
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Pro
{
    /// <summary> Base class for all node graphs </summary>
    [Serializable]
    public abstract class IMGUIGraph : ScriptableObject
    {

        public virtual string Name => this.name;
        
        /// <summary> All nodes in the graph. <para/>
        /// See: <see cref="AddNode{T}"/> </summary>
        [SerializeField] public List<IMGUIGraphNode> nodes = new List<IMGUIGraphNode>();

        /// <summary> Add a node to the graph by type (convenience method - will call the System.Type version) </summary>
        public T AddNode<T>() where T : IMGUIGraphNode
        {
            return AddNode(typeof(T)) as T;
        }

        /// <summary> Add a node to the graph by type </summary>
        public virtual IMGUIGraphNode AddNode(Type type)
        {
            IMGUIGraphNode.graphHotfix = this;
            IMGUIGraphNode node = ScriptableObject.CreateInstance(type) as IMGUIGraphNode;
            node.graph = this;
            nodes.Add(node);
            return node;
        }

        /// <summary> Creates a copy of the original node in the graph </summary>
        public virtual IMGUIGraphNode CopyNode(IMGUIGraphNode original)
        {
            IMGUIGraphNode.graphHotfix = this;
            IMGUIGraphNode node = ScriptableObject.Instantiate(original);
            node.graph = this;
            node.ClearConnections();
            nodes.Add(node);
            return node;
        }

        /// <summary> Safely remove a node and all its connections </summary>
        /// <param name="node"> The node to remove </param>
        public virtual void RemoveNode(IMGUIGraphNode node)
        {
            node.ClearConnections();
            nodes.Remove(node);
            if (Application.isPlaying) Destroy(node);
        }

        /// <summary> Remove all nodes and connections from the graph </summary>
        public virtual void Clear()
        {
            if (Application.isPlaying)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    Destroy(nodes[i]);
                }
            }

            nodes.Clear();
        }

        /// <summary> Create a new deep copy of this graph </summary>
        public virtual IMGUIGraph Copy()
        {
            // Instantiate a new nodegraph instance
            IMGUIGraph graph = Instantiate(this);
            // Instantiate all nodes inside the graph
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i] == null) continue;
                IMGUIGraphNode.graphHotfix = graph;
                IMGUIGraphNode node = Instantiate(nodes[i]) as IMGUIGraphNode;
                node.graph = graph;
                graph.nodes[i] = node;
            }

            // Redirect all connections
            for (int i = 0; i < graph.nodes.Count; i++)
            {
                if (graph.nodes[i] == null) continue;
                foreach (IMGUIGraphNodePort port in graph.nodes[i].Ports)
                {
                    port.Redirect(nodes, graph.nodes);
                }
            }

            return graph;
        }

        protected virtual void OnDestroy()
        {
            // Remove all nodes prior to graph destruction
            Clear();
        }

        #region Attributes

        /// <summary> Automatically ensures the existance of a certain node type, and prevents it from being deleted. </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
        public class RequireNodeAttribute : Attribute
        {
            public Type type0;
            public Type type1;
            public Type type2;

            /// <summary> Automatically ensures the existance of a certain node type, and prevents it from being deleted </summary>
            public RequireNodeAttribute(Type type)
            {
                this.type0 = type;
                this.type1 = null;
                this.type2 = null;
            }

            /// <summary> Automatically ensures the existance of a certain node type, and prevents it from being deleted </summary>
            public RequireNodeAttribute(Type type, Type type2)
            {
                this.type0 = type;
                this.type1 = type2;
                this.type2 = null;
            }

            /// <summary> Automatically ensures the existance of a certain node type, and prevents it from being deleted </summary>
            public RequireNodeAttribute(Type type, Type type2, Type type3)
            {
                this.type0 = type;
                this.type1 = type2;
                this.type2 = type3;
            }

            public bool Requires(Type type)
            {
                if (type == null) return false;
                if (type == type0) return true;
                else if (type == type1) return true;
                else if (type == type2) return true;
                return false;
            }
        }

        #endregion
    }
}