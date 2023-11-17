public class Pet{
    public string name;
    public int health;

    public List<string>sicknesses = new List<string>{};

    public int urination = 0;
    public int defecation = 0;

    public int hunger = 0;
    public int thirst = 0;

    public bool alive = true;

    private int counter = 0;
    private int counter2 = 0;

    public bool isUpdated = false;

    public Pet(string n){
        name = n;
        health = 100;
    }

    public void Update(){
        counter++;
        isUpdated = false;
        if (counter == 60){
            isUpdated = true;
            counter = 0;
            counter2++;
            hunger++;
            thirst += 2;

            if (counter2 == 5) urination++;
            else if (counter2 == 10){
                counter2 = 0;
                urination++;
                defecation++;
            }
        }
    }
}