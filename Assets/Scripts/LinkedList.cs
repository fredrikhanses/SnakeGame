public class LinkedList<data>
{
    public class Node
    {
        public data data;
        public Node next;

        public Node(data i)
        {
            data = i;
            next = null;
        }

        public void AddLast(data data)
        {
            if (next == null)
            {
                next = new Node(data);
            }
            else
            {
                next.AddLast(data);
            }
        }

        public void AddFirst(data data)
        {
            if (next == null)
            {
                next = new Node(data);
            }
            else
            {
                Node temp = new Node(data)
                {
                    next = next
                };
                next = temp;
            }
        }

        public void RemoveLast()
        {
            if (next == null)
            {
                return;
            }
            else if (next.next == null)
            {
                next = null;
            }
            else
            {
                next.RemoveLast();
            }
        }

        public int Count(int count)
        {
            if (next == null)
            {
                return count;
            }
            else
            {
                count++;
                return next.Count(count);
            }
        }

        public void RemoveAtIndex(int count, int index)
        {
            if (count == index)
            {
                next = next.next;
            }
            else if(next.next != null)
            {
                count++;
                next.RemoveAtIndex(count, index);
            }
        }

        public data GetDataAtIndex(int count, int index)
        {
            if (count == index)
            {
                return next.data;
            }
            else if(next.next != null)
            {
                count++;
                return next.GetDataAtIndex(count, index);
            }
            else
            {
                return next.data;
            }
        }

        internal bool Contains(data data)
        {
            if (next.data.Equals(data))
            {
                return true;
            }
            else if (next.next != null)
            {
                return next.Contains(data);
            }
            else
            {
                return false;
            }
        }
    }

    public class SingleLinkedList
    {
        public Node headNode;

        public SingleLinkedList()
        {
            headNode = null;
        }

        public void AddLast(data data)
        {
            if (headNode == null)
            {
                headNode = new Node(data);
            }
            else
            {
                headNode.AddLast(data);
            }
        }

        public void AddFirst(data data)
        {
            if (headNode == null)
            {
                headNode = new Node(data);
            }
            else
            {
                Node temp = new Node(data)
                {
                    next = headNode
                };
                headNode = temp;
            }
        }

        public void RemoveLast()
        {
            if (headNode == null)
            {
                return;
            }
            else if (headNode.next == null)
            {
                RemoveFirst();
            }
            else
            {
                headNode.RemoveLast();
            }
        }

        public void RemoveFirst()
        {
            if (headNode == null)
            {
                return;
            }
            else if (headNode.next == null)
            {
                headNode = null;
            }
            else
            {
                headNode = headNode.next;
            }
        }

        public int Count()
        {
            int count = 1;
            if (headNode == null)
            {
                return 0;
            }
            else
            {
                return headNode.Count(count);
            }
        }

        public bool Contains(data data)
        {
            if (headNode == null)
            {
                return false;
            }
            else if (headNode.data.Equals(data))
            {
                return true;
            }
            else if (headNode.next != null)
            {
                return headNode.Contains(data);
            }
            else
            {
                return false;
            }
        }

        public void RemoveAtIndex(int index)
        {
            if (headNode == null)
            {
                return;
            }
            else if (index == 0)
            {
                RemoveFirst();
            }
            else if(headNode.next != null)
            {
                int count = 1;
                headNode.RemoveAtIndex(count, index);
            }
            else
            {
                return;
            }
        }

        public data GetDataAtIndex(int index)
        {
            if (headNode == null)
            {
                return headNode.data;
            }
            else if (index == 0)
            {
                return headNode.data;
            }
            else if(headNode.next != null)
            {
                int count = 1;
                return headNode.GetDataAtIndex(count, index);
            }
            else
            {
                return headNode.data;
            }
        }
    }
}