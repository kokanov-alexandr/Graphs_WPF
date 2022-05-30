using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
namespace graphs
{
    public class Controler
    {
        public enum State { First, Second, Third }
        public State IsBtnAddTopClick { get;  set; }
        public State IsBtnDeleteTopClick { get;  set; }
        public State IsBtnDeleteConnectionClick { get;  set; }
        public State IsBtnAddConnectionClick { get;  set; }

        public Controler()
        {
            IsBtnAddTopClick = State.First;
            IsBtnDeleteTopClick = State.First;
            IsBtnDeleteConnectionClick = State.First;
            IsBtnAddConnectionClick = State.First;
        }

        public void ClearInfo()
        {
            IsBtnAddTopClick = State.First;
            IsBtnDeleteTopClick = State.First;
            IsBtnDeleteConnectionClick = State.First;
            IsBtnAddConnectionClick = State.First;

        } 
    }
}
