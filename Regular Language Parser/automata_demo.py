
# We define the transition function
trans_func = {(1, '0'): 1,
              (1, '1'): 2,
              (2, '0'): 3,
              (2, '1'): 2,
              (3, '0'): 2,
              (3, '1'): 2
             }
s = 1 # define the current state, i.e. the initial state
inp = input('Enter a string: ')
for i in range(0, len(inp)):
    c = inp[i]
    inp_pair = (s, c)
    if inp_pair not in trans_func.keys():
        s = -1
    else:
        s = trans_func[inp_pair]

if s not in [2]:
    print('The input string {} is not accepted by the machine'.format(inp))
else:
    print('The input string {} is accepted by the machine'.format(inp))
        
