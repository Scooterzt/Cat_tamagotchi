using System.ComponentModel.DataAnnotations;
namespace DojoDachi.Models{
    public class Dachi{
        public int Happiness{get;set;}
        public int Fullness{get;set;}
        public int Energy{get;set;}
        public int Meals{get;set;}

    public Dachi(){
        this.Happiness = 20;
        this.Fullness = 20;
        this.Energy = 50;
        this.Meals = 3;
        }
    public Dachi(int happines, int fullness, int energy, int meals){
        this.Happiness = happines;
        this.Fullness = fullness;
        this.Energy = energy;
        this.Meals = meals;
        }
    }
}