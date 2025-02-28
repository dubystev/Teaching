import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/**
 * Link to the GCP instance below, the image can be downloaded via the link:<br/>
 * <a href = "GCP instance">https://drive.google.com/file/d/1zRTPJ7FdMeH83heL0G0dCsPF8bWU6Pjv/view?usp=sharing</a>
 * <p>To solve for another graph instance (i.e. any problem that can be represented with a constraints graph):
 * <ol>Edit the details of the vertices in the {@code getStrings()} function, including the entries in the constraints
 * list.</ol>
 * <ol>Modify the details of the expression:<br/>{@code String[] values = {"red", "green", "blue"};}<br/> to change
 * the domain of variables in your own CSP instance.</ol></p>
 * <p>After the modification, everything works except if you want to add more functionalities to the source code.</p>
 */

public class Main {
    public static void main(String[] args) {
        System.out.println("Graph Colouring with Intelligent Back-tracking");
        List<String> constraints_list = getStrings();
        String[] constraints = constraints_list.toArray(new String[0]);
        String[] values = {"red", "green", "blue"};
        csp_model csp = new csp_model(constraints, values);
        HashMap<String, String> solution = csp.backtrack_search();
        if(solution == null)
            System.out.println("No solution");
        else
        {
            System.out.println("Variable assignments printed below...");
            for(String p : solution.keySet())
            {
                System.out.printf("%s: %s\n", p, solution.get(p));
            }
        }
    }

    private static List<String> getStrings() {
        String vertex1 = "v1";
        String vertex2 = "v2";
        String vertex3 = "v3";
        String vertex4 = "v4";
        String vertex5 = "v5"; // 5
        String vertex6 = "v6"; // 6
        String vertex7 = "v7"; // G g
        List<String> constraints_list = new ArrayList<>();
        constraints_list.add(vertex1 + "," + vertex2);
        constraints_list.add(vertex1 + "," + vertex3);
        constraints_list.add(vertex1 + "," + vertex5);
        constraints_list.add(vertex2 + "," + vertex3);
        constraints_list.add(vertex2 + "," + vertex4);
        constraints_list.add(vertex3 + "," + vertex4);
        constraints_list.add(vertex3 + "," + vertex6);
        constraints_list.add(vertex4 + "," + vertex7);
        constraints_list.add(vertex5 + "," + vertex6);
        constraints_list.add(vertex5 + "," + vertex7);
        constraints_list.add(vertex6 + "," + vertex7);
        return constraints_list;
    }
}