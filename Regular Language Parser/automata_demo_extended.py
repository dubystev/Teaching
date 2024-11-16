
num_stars_read = 0  # determines what to process next
trans_func_end_flag = False  # signifies the end of transition function definition in the file
file_path = 'model2.txt'

trans_func = {}  # We define an empty dictionary to store data about the transition function
final_states = []  # We define a list of final states, empty at first.

# Open the file in read mode ('r')
try:
    with open(file_path, 'r') as file:
        for line in file:
            line.strip()  # Use strip() to remove trailing newline characters
            if '*' in line:
                num_stars_read += 1
                trans_func_end_flag = True
                continue
            words = line.split()
            if not trans_func_end_flag:
                # Process the tokenized words
                cur_state = int(words[0])
                symbol = words[1]
                next_state = int(words[2])
                dict_key = (cur_state, symbol)
                trans_func[dict_key] = next_state
            else:
                if num_stars_read == 1:
                    s = int(words[0])  # read the current state from the file
                else:
                    for word in words:
                        final_states.append(int(word))
                    break
except FileNotFoundError:
    print(f"File '{file_path}' not found.")
except Exception as e:
    print(f"An error occurred: {e}")

inp = input('Enter a string: ')
for i in range(0, len(inp)):
    c = inp[i]
    inp_pair = (s, c)
    if inp_pair not in trans_func.keys():
        s = -1
    else:
        s = trans_func[inp_pair]

if s not in final_states:
    print(f"ACCEPTANCE: NO, The input string {inp} is not accepted by the machine")
else:
    print(f"ACCEPTANCE: YES, The input string {inp} is accepted by the machine")
