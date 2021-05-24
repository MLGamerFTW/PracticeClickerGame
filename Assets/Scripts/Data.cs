using System.Collections;
using System.Collections.Generic;
using BreakInfinity;

public class Data
{
    public BigDouble clicks;

    public List<BigDouble> clickUpgradeLevel;

    public Data()
    {
        clicks = 0;

        clickUpgradeLevel = Methods.CreateList<BigDouble>(3);
    }
}
