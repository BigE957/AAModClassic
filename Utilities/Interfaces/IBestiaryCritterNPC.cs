namespace AAModClassic.Utilities.Interfaces
{
    public interface IBestiaryCritterNPC
    {
        public virtual int CountAsType => -1;

        public virtual bool UnlockWhenNearby => true;
    }
}
