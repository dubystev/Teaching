#Author: Stephen A. Adubi
#Date: 02-04-2018 (DD-MM-YYYY) @ 12:03 am (start time)
#Finalised on 07-04-2018 (DD-MM-YYYY) @ 11:25 pm
#Simulation of RR scheduling algorithm
#This is an event-driven simulation, read more on this topic from the book at the end of the lecture note!

from random import *

#simulation parameters
global N #number of processes to simulate
global delta #time quantum of x units
global QList #queue of processes: although implemented using list
global arrTimes
global count
global TimeSeries, arrivals, turnTime, finishTime, burstTimes, waitingTime

low = 65
high = 131
delta = 20
QList = []
arrivals = {}
turnTime = {}
finishTime = {}
burstTimes = {}
waitingTime = {}
N = 20
arrTimes = [0]
TimeSeries = []
max_IAT = 15 #maximum inter-arrival time 

def generate_arrival_event(clock):
    global arrTimes, count
    #print('arrTimes before now: ', arrTimes)
    #print('Number of processes that already arrived: ', count)
    handle_arrival(clock) #generate the next arrival
    if(arrTimes):
        del arrTimes[0] #remove the current arrival time after handling
    if(count < N): #generate next arrival if max. number of packets NOT reached!
        nextArrv = clock + randrange(1, max_IAT + 1)
        print('Next arrival: ', nextArrv)
        arrTimes.append(nextArrv) #record the time for the next arrival
    print('queued arrival times: ', arrTimes)
    
def dequeue_process():
    global QList
    lst = QList[0]
    del QList[0] #remove the item in front of the 'queue'
    print('Process',lst[0],'deleted')

def dequeue_and_enqueue_process(ID, burstTime):
    global QList
    lst = QList[0]
    del QList[0] #removes the process in front of the queue
    LIST = [ID, burstTime]
    QList.append(LIST) #enqueue the process again until its turn is reached
    print('Process',lst[0],'deleted and put back again')

def handle_arrival(clock):
    global QList, count, arrivals, turnTime, finishTime, burstTimes, waitingTime
    global TimeSeries, tProcess,   lstEntry
    burstTime = randrange(low, high)
    if(not bool(arrivals)):
        arrivals = {count: clock} #dictionary of process' arrival times
    else:
        arrivals[count] = clock #append

    if(not bool(finishTime)):
        finishTime = {count: 0} #create a dictionary of finishTime
    else:
        finishTime[count] = 0 #append to the dictionary

    if(not bool(turnTime)):
        turnTime = {count: 0} #create a dictionary of turnaround time's'
    else:
        turnTime[count] = 0 #append to the dictionary

    if(not bool(burstTimes)):
        burstTimes = {count: burstTime} #create a dictionary of burstTime's'
    else:
        burstTimes[count] = burstTime #append to the dictionary

    #debugging
    #print('gets here, value of count = ', count)
    tProcess = [count, burstTime] #list containing process ID
    #print('Process profile: ', tProcess) #debugging
    lstEntry = [clock] #record the time of entry of process i in a list
    TimeSeries.append(lstEntry) #append the new list entry of process i
    #print('TimeSeries: ', TimeSeries) #debugging
    QList.append(tProcess) #add the new process with its profile
    count = count + 1

def main():
    global clock, count, QList, arrTimes, burstTimes, turnTime
    global arrivals, finishTime, N
    count = 0
    clock = 0
    generate_arrival_event(clock) #generate the next(first) arrival
    print('queue: ', QList)
    while(QList):
        counter = 0
        processDone = 0 #to detect if a process was completed within delta
        lst = QList[0] #get the list on top of the queue
        PID = lst[0] #get the next process ID
        
        #drop the current clock value into the timeseries list of lists
        lstEntry = TimeSeries[PID] #get the list of the current process
        lstEntry.append(clock) #append the current value of clock to the list
        TimeSeries[PID] = lstEntry #put the updated list back in TimeSeries list
        #print('TimeSeries:',TimeSeries)

        #if time quantum of current process elapses, preempt it (the process)
        while(counter < delta):
            #print('counter = ', counter) #debug
            clock = clock + 1
            #print('arrival times = ', arrTimes)
            if(arrTimes): #check if the list is NOT empty
                if(clock==arrTimes[0]): generate_arrival_event(clock)
            #print('CLOCK:',clock) #uncomment this to see the clock progression
            lst[1] = lst[1] - 1 #reduce the process' burst time
            if(lst[1] == 0):
                finishTime[PID] = clock
                turnTime[PID] = clock - arrivals[PID] #turnaround time
                #drop the current clock value into the timeseries list of lists
                lstEntry = TimeSeries[PID] #get the list of the current process
                lstEntry.append(clock) #append the current value of clock
                TimeSeries[PID] = lstEntry #put the updated list back
                processDone = 1
                break
            counter = counter + 1 #until counter 'meets' the time quantum

        if(processDone):
            dequeue_process() #remove the process from the queue
        else:
            #drop the current clock value into the timeseries list of lists
            lstEntry = TimeSeries[PID] #get the list of the current process
            lstEntry.append(clock) #append the current value of clock to d list
            TimeSeries[PID] = lstEntry #put the updated list back
            dequeue_and_enqueue_process(PID, lst[1]) #re-schedule the process

    #checking, debugging
    print('Arrival Time\'s\'',arrivals)
    #print('Finish Time\'s\'',finishTime)
    print('Turnaround Time\'s\'',turnTime)
    total = 0
    total2 = 0
    print('Burst times',burstTimes)
    for val in turnTime.values():
        total = total + val
    for val in burstTimes.values():
        total2 = total2 + val

    #calculating waiting time for each process and average waiting time; awt
    total_waiting_time = 0
    for i in range(N):
        lst = TimeSeries[i] #retrieve the time series of process i
        j = 1
        summ = 0
        listLength = len(lst)
        while(j < listLength):
            summ = summ + (lst[j] - lst[j-1])
            j = j + 2
        total_waiting_time = total_waiting_time + summ
        print('Total Waiting time for Process',i,'=',summ)
        #end while
    #end for

    print('Average Waiting Time=',total_waiting_time/N)
    print('Total turnaround time:',total,' Number of processes =', N)
    print('Average Turnaround time =',total/N)
    print('CLOCK at the end:',clock,' Total burst times:',total2)

if __name__ == '__main__':
    main()
