class PRQNode:
    def __init__(self, state, path="", priority=0):
        self.state = state
        self.path = path
        self.priority = priority
        self.next = None


class PriorityQueue:
    def __init__(self):
        self.head = None

    def push(self, new):
        if self.isEmpty():
            self.head = new
        elif self.head.priority >= new.priority:
            new.next = self.head
            self.head = new
        else:
            temp = self.head
            while temp.next is not None and temp.next.priority < new.priority:
                temp = temp.next
            new.next = temp.next
            temp.next = new

    def pop(self):
        if self.isEmpty():
            return
        temp = self.head
        self.head = self.head.next
        return temp

    def isEmpty(self):
        return self.head is None
