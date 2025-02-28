import java.util.*;

/**
 * CSP: Constraints Satisfaction Problem</br>
 * ++++++++++++++++++++++++++++++++++++++++++++++++</br>
 * An implementation of a CSP-based solution approach to colouring graphs
 * The specific algorithm implemented is the Intelligent Backtracking algorithm.</br></br>
 * In this code, the <b>degree <u>heuristic</u></b> is employed to select one variable among unassigned
 * variables while the <b>least constraining value (LCV) <u>heuristic</u></b> is employed to choose a suitable
 * value for the selected variable.</br></br>
 * There are three main java three classes split into two java source files needed to execute
 * the program, they include:</br>
 * <b>Class 1</b>: <b>Main</b> in Main.java; entry point for execution.</br>
 * <b>Class 2</b>: <b>csp_graph</b> in csp_model.java; implements the <b>constraints graph</b>, an
 * important data structure for modelling the constraints in the CSP.</br>
 * <b>Class 3</b>: <b>csp_model</b> in csp_model.java; implements the entire logic of the CSP
 * algorithms described in paragraphs 1 & 2.</br>
 * +++++++++++++++++++++++++++++++++++++++++++++++++
 */

class csp_graph
{
    private HashMap<String, Integer> items;
    private SortedSet<String> items_set;
    private int[][] adjacency_matrix;

    // construct the constraints graph from the set of binary constraints provided
    public csp_graph(String[] constraints)
    {
        items = new HashMap<>();
        items_set = new TreeSet<>();
        for(String s : constraints){
            String[] itemsArray = s.split(",");
            items_set.add(itemsArray[0]);
            items_set.add(itemsArray[1]);
        }

        int nodes_count = items_set.size();
        adjacency_matrix = new int[nodes_count][nodes_count];
        for(String s : constraints){
            String[] itemsArray = s.split(",");
            if(!items.containsKey(itemsArray[0]))
                items.put(itemsArray[0], items_set.headSet(itemsArray[0]).size());
            if(!items.containsKey(itemsArray[1]))
                items.put(itemsArray[1], items_set.headSet(itemsArray[1]).size());
            adjacency_matrix[items.get(itemsArray[0])][items.get(itemsArray[1])] = 1;
            adjacency_matrix[items.get(itemsArray[1])][items.get(itemsArray[0])] = 1;
        }
   }

   // returns the number of variables of the CSP model
    public int getCardinality() { return items.keySet().size(); }
    public HashMap<String, Integer> getItems() { return items; }
    public int[][] getMatrix() { return adjacency_matrix; }
}

public class csp_model {
    private String[] values;
    int[] degree_values;
    private csp_graph graph;
    private ArrayList<HashMap<String, ArrayList<String>>> inferences_list;
    private boolean latest_inf_result;
    private Random rnd;

    public csp_model(String[] constraints, String[] values)
    {
        this.values = values;
        graph = new csp_graph(constraints);
        inferences_list = new ArrayList<>();
        rnd = new Random();
        HashMap<String, ArrayList<String>> inference = INFERENCE(null, null);
        inferences_list.add(inference);
        System.out.println();
    }

    private HashMap<String, ArrayList<String>> INFERENCE(String recent_var, HashMap<String, String> assignments)
    {
        HashMap<String, ArrayList<String>> temp;
        if(recent_var == null)
        {
            ArrayList<String> values_list = new ArrayList<>(Arrays.asList(values));
            temp = new HashMap<>();
            for (String item : graph.getItems().keySet())
                temp.put(item, new ArrayList<>(values_list));
            latest_inf_result = true;
        }
        else
        {
            int lastI = inferences_list.size() - 1; // index of the last item in the array list
            HashMap<String, ArrayList<String>> initialiser = inferences_list.get(lastI);
            temp = new HashMap<>();

            // perform a deep copy of the initialiser into temp (the latest inference)
            for(String keys_in_init : initialiser.keySet())
            {
                ArrayList<String> possible_values_list = new ArrayList<>(initialiser.get(keys_in_init));
                temp.put(keys_in_init, possible_values_list);
            }
            //deep copy ends...

            String value_assigned_to_recent_var = assignments.get(recent_var);
            temp.get(recent_var).clear();
            temp.get(recent_var).add(value_assigned_to_recent_var.toUpperCase()); // indicates already made assignment
            int index1 = graph.getItems().get(recent_var);
            Set<String> unassigned_vars = new LinkedHashSet<>(graph.getItems().keySet());
            unassigned_vars.removeAll(assignments.keySet());
            for(String otherVars : unassigned_vars)
            {
                int index2 = graph.getItems().get(otherVars);
                if(graph.getMatrix()[index1][index2] == 1)
                {
                    temp.get(otherVars).remove(value_assigned_to_recent_var);
                    if(temp.get(otherVars).size() == 0)
                        latest_inf_result = false;
                }
            }
        }

        return temp;
    }

    public HashMap<String, String> backtrack_search()
    {
        HashMap<String, String> assignments = new HashMap<>(); // a hash-map of assignments
        return backtrack(assignments);
    }

    /**
     * In this method, the least constraining value (LCV) heuristic is implemented.
     * @param var the variable of interest
     * @param assignment the current partial solution
     * @return a string, representing the value to be assigned to the variable (<code>var</code>) of interest
     */
    private String[] order_domain_values(String var, HashMap<String, String> assignment)
    {
        int lastI = inferences_list.size() - 1;
        List<String> valid_values_var = new ArrayList<>(inferences_list.get(lastI).get(var));
        Collections.shuffle(valid_values_var);
        String[] result;
        System.out.print("LCVs: "); //////
        if(valid_values_var.size() == 1)
        {
            result = new String[1];
            result[0] = valid_values_var.get(0);
        }
        else
        {
            HashMap<String, Integer> lcvs = new LinkedHashMap<>(); // key order is based on entry order
            Set<String> unassigned_vars = new LinkedHashSet<>(graph.getItems().keySet());
            unassigned_vars.removeAll(assignment.keySet());
            int[][] conflict_matrix = graph.getMatrix();
            int item_index = graph.getItems().get(var);
            for(String cand_value : valid_values_var)
            {
                int lcv = 0;
                for(String unassigned_var : unassigned_vars)
                {
                    int item2_index = graph.getItems().get(unassigned_var);
                    if(conflict_matrix[item2_index][item_index] == 1) {
                        if (inferences_list.get(lastI).get(unassigned_var).contains(cand_value))
                            lcv += inferences_list.get(lastI).get(unassigned_var).size() - 1;
                        else
                            lcv += inferences_list.get(lastI).get(unassigned_var).size();
                    }
                }

                lcvs.put(cand_value, lcv);
                System.out.printf("{%s = %d} ", cand_value, lcv); //////
            }

            // sort the lcvs and return the sorted list
            List<Map.Entry<String, Integer>> list = new ArrayList<>(lcvs.entrySet()); // Convert to List of Map Entries
            list.sort(Map.Entry.comparingByValue(Comparator.reverseOrder())); // Sort List by Value
            result = list.stream().map(Map.Entry::getKey).toArray(String[]::new); // Convert sorted keys to an array
        }

        return result;
    }

    private boolean isConsistent(String value, HashMap<String, String> assignment, String recent_var)
    {
        Set<String> assigned_vars = new LinkedHashSet<>(assignment.keySet());
        int[][] conflict_matrix = graph.getMatrix();
        int item_index = graph.getItems().get(recent_var);
        for(String assigned_var : assigned_vars)
        {
            int item2_index = graph.getItems().get(assigned_var);
            if(conflict_matrix[item2_index][item_index] == 1 && (Objects.equals(value, assignment.get(assigned_var))))
                return false;
        }
        return true;
    }

    void printDegrees_and_Options(String var, HashMap<String, String> assignment)
    {
        System.out.print("Degree Values of unassigned variables: {");
        HashMap<String, Integer> items = graph.getItems();
        for(String item : items.keySet()) {
            if(assignment.containsKey(item))
                continue;
            int item_index = items.get(item);
            int degree_value = degree_values[item_index];
            System.out.printf("%s = %d, ", item, degree_value);
        }
        System.out.print('}');
        System.out.println();
        System.out.print("Options: ");
        int lastI = inferences_list.size() - 1;
        List<String> valid_values_var = new ArrayList<>(inferences_list.get(lastI).get(var));
        for(String cand_values : valid_values_var)
        {
            System.out.printf("{%s = %s} ", var, cand_values);
        }
        System.out.println();
    }

    void printInferences(HashMap<String, ArrayList<String>> inferences)
    {
        System.out.print("Forward Checking -> ");
        for(String item : inferences.keySet())
        {
            System.out.printf("%s: {", item);
            for(String value_in_item : inferences.get(item))
            {
                System.out.printf("%s, ", value_in_item);
            }
            System.out.print("} ");
        }

        System.out.println();
        System.out.println();
    }

    private HashMap<String, String> backtrack(HashMap<String, String> assignments)
    {
        if(assignments.size() == graph.getCardinality())
            return assignments;
        String var = select_unassigned_variable(assignments);
        printDegrees_and_Options(var, assignments); ////// For debugging, NOT a CORE step
        for(String value : order_domain_values(var, assignments))
        {
            System.out.printf("Chose %s", value); //////
            System.out.println(); //////
            if(isConsistent(value, assignments, var))
            {
                assignments.put(var, value);
                HashMap<String, ArrayList<String>> inferences = INFERENCE(var, assignments);
                printInferences(inferences); //////
                if(latest_inf_result) // if inferences != failure
                {
                    inferences_list.add(inferences);
                    HashMap<String, String> result = backtrack(assignments);
                    if(result != null)
                        return result;
                    inferences_list.remove(inferences_list.size() - 1);
                }
                assignments.remove(var);
            }
        }

        return null;
    }

    /**
     * Based on the degree heuristic, the next unassigned variable is selected
     * @param assignment the partially filled solution set
     * @return the next variable for value assignment
     */
    private String select_unassigned_variable(HashMap<String, String> assignment)
    {
        Set<String> assigned_items = assignment.keySet();
        Set<String> unassigned_items = new LinkedHashSet<>(graph.getItems().keySet());
        unassigned_items.removeAll(assigned_items);
        List<String> unassigned_items_list = new ArrayList<>(unassigned_items);
        Collections.shuffle(unassigned_items_list);
        var items = graph.getItems();
        degree_values = new int[items.size()];
        String selected_var="";
        int max_degree = 0;
        int[][] conflict_matrix = graph.getMatrix();
        int iter = 0;
        for(String item : unassigned_items_list)
        {
            iter++;
            int item_index = items.get(item);
            for(String item2 : unassigned_items_list)
            {
                int item2_index = items.get(item2);
                if(conflict_matrix[item2_index][item_index] == 1)
                    degree_values[item_index]++;
            }

            boolean first_cond = iter == 1;
            boolean second_cond = degree_values[item_index] > max_degree ||
                                 degree_values[item_index] == max_degree && rnd.nextBoolean();
            if(first_cond || second_cond)
            {
                max_degree = degree_values[item_index];
                selected_var = item;
            }
        }

        return selected_var;
    }
}
