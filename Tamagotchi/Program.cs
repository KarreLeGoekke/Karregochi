using System.Numerics;
using System.Diagnostics;
using Raylib_cs;

Console.WriteLine("What's your pet's name?");
string name = Console.ReadLine();
Pet creature = new Pet(name);

Font font = Raylib.GetFontDefault();
Random rnd = new Random();

Raylib.InitWindow(800, 600, "Tomogachi");
Raylib.SetTargetFPS(60);
Texture2D pet = Raylib.LoadTexture(@"resources\pet.png");

float money = 0.00f;
bool inJob = false;

void buyWaterFunc(){
    if (money < 5.00f) return;
    money -= 5.00f;
    creature.thirst -= 50;
    creature.thirst = Math.Clamp(creature.thirst, 0, 100);
}

void buyFoodFunc(){
    if (money < 10.00f) return;
    money -= 10.00f;
    creature.hunger -= 50;
    creature.hunger = Math.Clamp(creature.hunger, 0, 100);
}

void buyToiletFunc(){
    if (money < 50.00f) return;
    money -= 50.00f;
    creature.urination -= 100;
    creature.urination = Math.Clamp(creature.urination, 0, 100);
    creature.defecation -= 100;
    creature.defecation = Math.Clamp(creature.defecation, 0, 100);
}

void buyDeathFunc(){
    if (money < 100.00f) return;
    money -= 100.00f;
    Raylib.CloseWindow();
}

void getJobFunc(){
    inJob = true;
}

Aleph.Button buyWater = new Aleph.Button("Water ($5.00)", new Vector2(150, 350), new Vector2(200, 50), buyWaterFunc);
Aleph.Button buyFood = new Aleph.Button("Food ($10.00)", new Vector2(150, 450), new Vector2(200, 50), buyFoodFunc);
Aleph.Button buyToilet = new Aleph.Button("Toilet ($50.00)", new Vector2(650, 350), new Vector2(200, 50), buyToiletFunc);
Aleph.Button buyDeath = new Aleph.Button("DEATH ($100.00)", new Vector2(650, 450), new Vector2(200, 50), buyDeathFunc);
Aleph.Button getJob = new Aleph.Button("Get a job! (3X income!)", new Vector2(400, 400), new Vector2(200, 50), getJobFunc);

Stopwatch sw = new Stopwatch();
sw.Start();

while (!Raylib.WindowShouldClose()){
    Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.WHITE);
        Raylib.DrawTexture(pet, 400 - 80, 200 - 80, Color.WHITE);

        Aleph.DrawTextCenter("Take care of " + creature.name + "!", new Vector2(400, 32), 48, Color.BLACK);

        // Statistics
        Aleph.DrawStatistic("Hunger", new Vector2(200, 175), creature.hunger);
        Aleph.DrawStatistic("Thirst", new Vector2(600, 175), creature.thirst);
        Aleph.DrawStatistic("Urination", new Vector2(200, 250), creature.urination);
        Aleph.DrawStatistic("Defecation", new Vector2(600, 250), creature.defecation);

        // Buttons
        buyWater.Draw();
        buyFood.Draw();
        buyToilet.Draw();
        buyDeath.Draw();

        // Job
        if (sw.Elapsed.TotalSeconds > 60 && !inJob){
            getJob.Draw();
        }

        Aleph.DrawTextCenter("Money: $" + MathF.Round(money, 2), new Vector2(400, 500), 36, Color.BLACK);
    Raylib.EndDrawing();
    creature.Update();
    if (creature.isUpdated){
        if (inJob) money += rnd.NextSingle() * 3.0f;
        else money += rnd.NextSingle();
    }
}

sw.Stop();
Raylib.CloseWindow();