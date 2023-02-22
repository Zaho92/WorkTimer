namespace WorkTimer.Controller.SpecialDataControllers
{
    public interface IDataController
    {
        public abstract void LoadData();

        public abstract bool SaveData();
    }
}