public class BoutonPresser : Pause // heritage
{
    public void apuyable()
    {
        autorisation = true;
    }
    public void inapuyable()
    {
        autorisation = false;
    }
}
