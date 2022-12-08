class Node:
    def __init__(self, state, path=""):
        self.state = state
        self.path = path
        self.next = None


class FIFOQueue:
    def __init__(self):
        self.head = None

    def push(self, new):
        if self.isEmpty():
            self.head = new
        else:
            temp = self.head
            while temp.next is not None:
                temp = temp.next
            temp.next = new

    def pop(self):
        if self.isEmpty():
            return
        temp = self.head
        self.head = self.head.next
        return temp

    def isEmpty(self):
        return self.head is None


class LIFOQueue:
    def __init__(self):
        self.head = None

    def push(self, new):
        if self.isEmpty():
            self.head = new
        else:
            new.next = self.head  # just make the new entry the head (as per LIFO, i.e. Stack)
            self.head = new

    def pop(self):
        if self.isEmpty():
            return
        temp = self.head
        self.head = self.head.next
        return temp

    def isEmpty(self):
        return self.head is None
