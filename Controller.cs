using System.Windows.Controls;
using System.Windows.Media;

namespace graphs
{
    public class Controller
    {
        public enum State { Inactive, Active, Active_2 }
        public State IsBtnAddTopClick;
        public State IsBtnDeleteTopClick;
        public State IsBtnDeleteConnectionClick;
        public State IsBtnAddConnectionClick;
        public State IsDFSBtnClick;

        public Controller()
        {
            IsBtnAddTopClick = State.Inactive;
            IsBtnDeleteTopClick = State.Inactive;
            IsBtnDeleteConnectionClick = State.Inactive;
            IsBtnAddConnectionClick = State.Inactive;
            IsDFSBtnClick = State.Inactive;
        }

        public void ClearInfo()
        {
            IsBtnAddTopClick = State.Inactive;
            IsBtnDeleteTopClick = State.Inactive;
            IsBtnDeleteConnectionClick = State.Inactive;
            IsBtnAddConnectionClick = State.Inactive;
            IsDFSBtnClick = State.Inactive;

            var win = ((MainWindow)System.Windows.Application.Current.MainWindow);
            win.btn_add_top.BorderBrush = win.btn_add_top.Background;
            win.btn_delete_top.BorderBrush = win.btn_delete_top.Background;
            win.btn_add_connection.BorderBrush = win.btn_add_connection.Background;
            win.btn_delete_connection.BorderBrush = win.btn_delete_connection.Background;
        }

        public void CheckStatus(ref State state, Button button)
        {
            if (state == State.Inactive)
            {
                ClearInfo();
                state = State.Active;
                button.BorderBrush = Brushes.Yellow;
            }
            else ClearInfo();
        }
    }
}
