/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package binarysearchtree;

/**
 *
 * @author Stephen A. Adubi
 * @since Oct. 1, 2016
 */

class Node //the Node class
{
    public int data; //data of the node
    public Node left; //left child of the node -- A node itself
    public Node right; //right child of the node -- A node itself
}

class BST //class handling BST operations, implements the insert-into-tree operation
{
    Node root;
    public BST() { root = null; } //constructor
    public boolean empty() { return (root==null); } //returns true if tree is empty
    public void display() { display(root); } //publicly accessible version
    void display(Node current) //this method displays the node data in the tree; never try to call this version
    {
        if(current != null)
        {
            display(current.left);
            System.out.print(current.data + " ");
            display(current.right);
        }
    }
    public void insert(int item) //publicly accessible version
    {
        if(empty()) //insert data as the root node if tree is empty
	{
            Node temp = new Node();
            temp.data = item;
            temp.left = null;
            temp.right = null;
            root = temp;
            System.out.println(item + " added to the tree");
	}
        
	else
            insert(root, item); //otherwise traverse down the tree until you find a suitable spot
    }
    void insert(Node curNode, int item) //automatically called via the else part of the above method
    {
        if(item<curNode.data)
        {
            if(curNode.left==null) /* if the current node has no left child then 
                                    insert the item as the left child */
            {
                Node temp = new Node(); //create a new node
		temp.data = item; //set it's data
		temp.left = null; //it has no left child yet
		temp.right = null; //it has no right child yet
		curNode.left = temp; //make it the left child of the current node
		System.out.println(item + " added to the tree"); 
            }
            
            else
                insert(curNode.left, item); /*otherwise try to insert the data into the left 
                                            subtree of the current node*/
        }
        
        else
	{
            if(curNode.right == null)
            {
		Node temp = new Node();
		temp.data = item;
		temp.left = null;
		temp.right = null;
		curNode.right = temp;
		System.out.println(item + " added to the tree");
            }
		
            else
		insert(curNode.right, item);
	}
    }
}

public class BinarySearchTree {

    /**
     * @param args the command line arguments
     */
    
    public static void main(String[] args) {
        /* Note the BST class has no method for deleting nodes
           Try adding that using your class experience*/
        // This main function inserts the sequence: 53, 72, 30, 61, 39, 14, 84, 79, 47, 9
        BST bst = new BST();
        bst.insert(53);
        bst.insert(72);
        bst.insert(30);
        bst.insert(61);
        bst.insert(39);
        bst.insert(14);
        bst.insert(84);
        bst.insert(79);
        bst.insert(47);
        bst.insert(9);
        
        System.out.println();
        bst.display();
        System.out.println();
    }
}