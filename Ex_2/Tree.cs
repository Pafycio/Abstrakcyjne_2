using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_2
    {
    public class Tree<T> : IEnumerable<T>
    {
        private T value;
        public T Value
        {
            private set
            {
                this.value = value;
            }
            get
            {
                return this.value;
            }
        }
        private List<Tree<T>> children;
        public List<Tree<T>> Children
        {
            private set
            {
                children = value;
            }
            get
            {
                return this.children;
            }
        }
        private EnumeratorOrder order;
        public EnumeratorOrder Order
        {
            get { return this.order; }
            set
            {
                if (value == EnumeratorOrder.BreadthFirstSearch ||
                    value == EnumeratorOrder.DepthFirstSearch)
                {
                    this.order = value;
                    foreach (Tree<T> elem in children)
                    {
                        elem.Order = value;
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public Tree(T p, EnumeratorOrder enumeratorOrder)
        {
            this.value = p;
            this.children = new List<Tree<T>>();
            this.Order = enumeratorOrder;
        }

        public void Add(Tree<T> subtree)
        {
            this.children.Add(subtree);
            subtree.Order = this.order;
        }

        public void Add(T number)
        {
            this.children.Add(new Tree<T>(number, this.order));
        }


        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            if (this.order == EnumeratorOrder.BreadthFirstSearch)
            {
                Queue<Tree<T>> queue = new Queue<Tree<T>>();
                queue.Enqueue(this);
                while (queue.Count() > 0)
                {
                    Tree<T> currentelem = queue.Dequeue();
                    yield return currentelem.value;
                    foreach (Tree<T> elem in currentelem.children)
                    {
                        queue.Enqueue(elem);
                    }
                }
            }
            else
            {
                Stack<Tree<T>> stack = new Stack<Tree<T>>();
                stack.Push(this);
                while (stack.Count() > 0)
                {
                    Tree<T> currentelem = stack.Pop();
                    currentelem.children.Reverse();
                    foreach (Tree<T> elem in currentelem.children)
                    {
                        stack.Push(elem);
                    }
                    currentelem.children.Reverse();
                    yield return currentelem.value;
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            return (this as IEnumerable<T>).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this as IEnumerable<T>).GetEnumerator();
        }
    }
}

