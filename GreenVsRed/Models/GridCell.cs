namespace GreenVsRed.Models
{
    internal sealed class GridCell
    {
        public int Value;
        public int BeenGreenCount;
        public bool SwitchesNextGeneration;

        public GridCell(int value)
        {
            this.Value = value;

            BeenGreenCount = 0;
            SwitchesNextGeneration = false;
        }

        public void IncrementBeenGreenCount()
        {
            BeenGreenCount++;
        }

        public void SwitchState()
        {
            if (this.Value == 0)
            {
                this.Value = 1;
            }
            else if (this.Value == 1) this.Value = 0;

            SwitchesNextGeneration = false;
        }
    }
}
