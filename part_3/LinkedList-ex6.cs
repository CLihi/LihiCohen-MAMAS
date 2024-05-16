using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Transactions;

class Node
{
    public int Value
    {get; set;}
    public Node? Next 
    {get; set;}

    public Node(int value, Node? next = null)
    {
        Value = value;
        Next = next;
    }
}

class LinkedList
{
    public Node? First
    {get; set;}
    public Node? Last
    {get; set;}
    public Node? Max
    {get; set;}
    public Node? Min
    {get; set;}

    public LinkedList(Node first)
    {
        First = first;
        Node cur_node = First;
        Min = first;
        Max = first;
        while (cur_node.Next != null) {
            if (cur_node.Value > Max.Value) {
                Max = cur_node;
            }
            if (cur_node.Value < Min.Value) {
                Min = cur_node;
            }
            cur_node = cur_node.Next;
        }
        Last = cur_node;
    }

    public void Append(int new_value)
    {
        Node new_node = new Node(new_value);
        if (Last is null)
        {
            // Last is null only if poped the last node of a linkedList, then both Last and First are null
            Last = new_node;
            First = new_node;
        }
        Last.Next = new_node;
        Last = new_node;
        // fix Min and Max if needed
        if (Max is null) {
            Max = new_node;
        }
        if (Min is null) {
            Min = new_node;
        }
        if (new_node.Value > Max!.Value) {
            Max = new_node;
        }
        if (new_node.Value < Min!.Value) {
            Min = new_node;
        }
    }

    public void Prepend(int new_value)
    {
        Node new_node = new Node(new_value, First);
        First = new_node;
        // fix Min and Max if needed
        if (Max is null) {
            Max = new_node;
        }
        if (Min is null) {
            Min = new_node;
        }
        if (new_node.Value > Max!.Value) {
            Max = new_node;
        }
        if (new_node.Value < Min!.Value) {
            Min = new_node;
        }        
    }

    public int? Pop()
    {
        if (First is null)
        {
            // The linkedilst is empty
            throw new SystemException("Used the pop method on an empty LinkedList");
        }
        if (First.Next is null) {
            // Happens when First == Last -> a linkedList of one node
            int first_val = First.Value;
            First = null;
            Last = null;
            Min = null;
            Max = null;
            return first_val;
        }
        Node preLast_node = First;
        bool pop_max = Last == Max ? true : false;
        bool pop_min = Last == Min ? true : false;
        if (pop_max){
            Max = First;
        }
        if (pop_min){
            Min = First;
        }
        while (preLast_node.Next!.Next != null) {
            preLast_node = preLast_node.Next;
            if (pop_max){
                if (preLast_node.Value > Max!.Value){
                    Max = preLast_node;
                }
            }
            if (pop_min) {
                if (preLast_node.Value < Min!.Value){
                    Min = preLast_node;
                }
            }
        }
        int pop_val = preLast_node.Next.Value;
        preLast_node.Next = null;
        Last = preLast_node;
        return pop_val;
    }

    public int Unqueue()
    {
        if (First is null)
        {
            // The linkedilst is empty
            throw new SystemException("Used the unqueue method on an empty LinkedList");
        }
        int pop_val = First.Value;
        if (First == Last) {
            // Happens when only one node is in the linkedList
            Last = First.Next; // should be null
            Max = null;
            Min = null;
        }
        bool pop_max = First == Max ? true : false;
        bool pop_min = First == Min ? true : false;
        First = First.Next;
        if (pop_max || pop_min) {
            //  fix Min and Max if needed
            if (pop_max){
                Max = First;
            }
            if (pop_min){
                Min = First;
            }
            Node? cur_node = First!.Next;
            while (cur_node != null) {
                if (pop_max){
                    if (cur_node.Value > Max!.Value) {
                        Max = cur_node;
                    }
                }
                if (pop_min){
                    if (cur_node.Value < Min!.Value) {
                        Min = cur_node;
                    }
                }
                cur_node = cur_node.Next;
            }
        }
        return pop_val;
    }

    public IEnumerable<int> ToList()
    {
        Node? cur_node = First;
        while (cur_node != null)
        {
            yield return(cur_node.Value);
            cur_node = cur_node.Next;
        }
    }

    public bool IsCircular()
    {
        if (First is null || First.Next is null) {
            return false;
        }
        Node? cur_node = First.Next;
        while (cur_node != First) {
            if (cur_node is null) {
                return false;
            }
            cur_node = cur_node.Next;
        }
        return true;
    }

    private Node? MergeSort(Node? first, Node? last)
    {
        if (first is null || first == last)
        {
            return first;
        }
        Node? middle = GetMiddle(first, last);
        Node? next_to_middle = middle!.Next;
        middle.Next = null;

        Node? left_side = MergeSort(first, middle);
        Node? right_side = MergeSort(next_to_middle, last);
        return Merge(left_side, right_side);
    }

    private Node? GetMiddle(Node first, Node? last)
    {
        if(first is null){
            return null;
        }
        Node? slow_node = first;
        Node? fast_node = first;
        while (fast_node != last && fast_node!.Next != last) {
            slow_node = slow_node!.Next;    // called when first!=last, so first.Next!=null
            fast_node = fast_node!.Next!.Next;
        }
        return slow_node;
    }

    private Node? Merge(Node? left, Node? right)
    {
        Node semi = new Node(0); //starts the merged linkedList
        Node cur_node = semi;
        while (left != null && right != null) {
            if (left.Value <= right.Value) {
                cur_node.Next = left;
                left = left.Next;
            }
            else {
                cur_node.Next = right;
                right = right.Next;
            }
            cur_node = cur_node.Next;
        }
        if(right != null) {
            cur_node.Next = right;
        }
        else if (left != null) {
            cur_node.Next = left;
        }
        return semi.Next;
    }
    public void Sort()
    {
        First = MergeSort(First, Last);
        Node? cur_node = First;
        while (cur_node!.Next != null) {
            cur_node = cur_node.Next;
        }
        Last = cur_node;
    }

    public Node? GetMaxNode()
    {
        return Max;
    }
    
    public Node? GetMinNode()
    {
        return Min;
    }
}